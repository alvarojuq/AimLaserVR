using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;
using UnityEngine.InputSystem;
using UnityEngine.UI; //allows access to UI elements 

public class FetoscopeRotation : MonoBehaviour
{
    SerialController serialScript;
    private GameObject fetoCam;
    public GameObject serial;
    public GameObject joystick;
    public float cmode = 0;
    //set the starting values for the yaw (side to side) and pitch (up and down) and     
    float yaw = 65f;
    float pitch = 50f;
    float roll = 0.0f;

    //a public variable for the mouse sensitivity
    public float mouseSensitivity;
    //a public variable for the controller sensitivity
    public float controllerSensitivity;
    public float fetoscopeTurnSpeed;
    private Vector3 moveDirection = Vector3.zero;
    public float smoothingFactor = 5.0f;
    private PlayerControls inputActions;
    private void Start()
    {
        //this locks the cursor to the center of the game window, and hides the cursor on the game screen
        Cursor.lockState = CursorLockMode.Locked;

        fetoCam = GameObject.Find("Fetoscope_Camera");

        var xrSettings = XRGeneralSettings.Instance;
        if (xrSettings != null && xrSettings.Manager != null && xrSettings.Manager.activeLoader != null)
        {
            cmode = 2;
        }
        else
        {
            cmode = 0;
        }

        serial = GameObject.Find("SerialController");
        serialScript = serial.GetComponent<SerialController>();
    }
    private void Awake()
    {
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.Enable();
    }

    private void OnDisable()
    {
        inputActions.Disable();
    }
    public void OnSwitchInputMode()
    {
        Debug.Log("Control Mode Switched");
        cmode++;
        if (cmode > 3)
        {
            cmode = 0;
        }
    }
    public void CmodeSelect(int value)
    {
        Debug.Log("Control Mode Switched");
        cmode = value;
    }
    void Update()
    {
        if (!UserMenu_Simulation.SimIsPaused)
        { 
            RotatePivot();
        }
        
    }
    //the method to rotate the pivot point 
    void RotatePivot()
    {

        if (cmode == 0) // keyboard
        {
            float lookInputX = inputActions.Default.AimingX.ReadValue<float>();
            yaw -= lookInputX * mouseSensitivity * Time.deltaTime;

            float lookInputY = inputActions.Default.AimingY.ReadValue<float>();
            pitch -= lookInputY * mouseSensitivity * Time.deltaTime;

            //this clamps the amount the fetoscope can rotate
            yaw = Mathf.Clamp(yaw, -35, 85);
            pitch = Mathf.Clamp(pitch, 0, 120);

            transform.eulerAngles = new Vector3(0.0f, yaw, pitch);
        }
        else if (cmode == 1) // gamepad
        {
            //CONTROLLERCODE
            //Get the current orientation of the game controller
            //Convert the value for each axis to an angle (degrees)
            //In this case the range for each is different
            //the Yaw, or horizontal anlge can go from (-65, 65) degrees
            //the Pitch, or vertical angle can go from (-50, 50) degrees
            float angle_horiz = 65 + (70 * -inputActions.Joystick.RStickX.ReadValue<float>());
            float angle_vert = 50 + (40 * -inputActions.Joystick.RStickY.ReadValue<float>());

            //Calculate the quaternion for the angles
            Quaternion qh = Quaternion.Euler(0, angle_horiz, angle_vert);

            //Rotate/orient the pivot using the quaternion
            transform.localRotation = qh;
            //CONTROLLERCODE ENDS
        }
        else if (cmode == 2) // vr controller
        {
            joystick = GameObject.Find("StickScope");
            ReturnPosition joyPos = joystick.GetComponent<ReturnPosition>();
            yaw = (joyPos.horiz * 75) + 25;
            pitch = (joyPos.vert * 75) + 60;

            //this clamps the amount the fetoscope can rotate
            yaw = Mathf.Clamp(yaw, -35, 85);
            pitch = Mathf.Clamp(pitch, 0, 120);

            transform.eulerAngles = new Vector3(0.0f, yaw, pitch);
        }
        else if (cmode == 3) // fetoscope controller
        {
            /*yaw = Mathf.Lerp(yaw, Mathf.Clamp(serialScript.rotX, -35, 85), smoothingFactor * Time.deltaTime);
            pitch = Mathf.Lerp(pitch, Mathf.Clamp(serialScript.rotY, 0, 120), smoothingFactor * Time.deltaTime);

            transform.eulerAngles = new Vector3(0.0f, yaw, pitch);

            roll = Mathf.Lerp(roll, serialScript.rotZ, smoothingFactor * Time.deltaTime);
            if (roll >= 360)
            {
                roll = roll % 360;
            }
            fetoCam.transform.eulerAngles = new Vector3(fetoCam.transform.eulerAngles.x, fetoCam.transform.eulerAngles.y, roll);*/
        }

    }

}
