using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; //allows access to UI elements 

public class FetoscopeRotation : MonoBehaviour
{
    private GameObject fetoCam;
    public GameObject serial;
    public GameObject joystick;
    public float cmode = 0;
    //set the starting values for the yaw (side to side) and pitch (up and down) and     
    float yaw = 65f;
    float pitch = 50f;
    float roll = 0.0f;

    public InputAction switchModeAction; 
    //a public variable for the mouse sensitivity
    public float mouseSensitivity;
    //a public variable for the controller sensitivity
    public float controllerSensitivity;
    public float fetoscopeTurnSpeed;
    private Vector3 moveDirection = Vector3.zero;
    //a public reference to the Control Toggle in the settings menu
    public Toggle ControlToggle;
    private void Start()
    {
        fetoCam = GameObject.Find("Fetoscope_Camera");
        
        cmode = 0;
    }
    private void OnEnable()
    {
        // Enable the InputAction
        switchModeAction.Enable();
        switchModeAction.performed += OnSwitchModePerformed;
    }

    private void OnDisable()
    {
        // Disable the InputAction
        switchModeAction.Disable();
        switchModeAction.performed -= OnSwitchModePerformed;
    }
    private void OnSwitchModePerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Control Mode Switched");
        cmode++;
        if (cmode > 3)
        {
            cmode = 0;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            Debug.Log("Control Mode Switched");
            cmode++;
            if (cmode > 3)
            {
                cmode = 0;
            }
        }

        if (UserMenu_Simulation.SimIsPaused)
        {
            //this leaves the cursor as is, just like normal
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            //this locks the cursor to the center of the game window, and hides the cursor on the game screen
            Cursor.lockState = CursorLockMode.Locked;
            //press esc to show the cursor again

            RotatePivot();
        }
        
    }


    //the method to rotate the pivot point 
    void RotatePivot()
    {

        if (cmode == 0) // keyboard
        {
            yaw -= Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

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
            float angle_horiz = 65 + (70 * -Input.GetAxis("PS3 RStick X"));
            float angle_vert = 50 + (40 * -Input.GetAxis("PS3 RStick Y"));

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
        else if (cmode == 3) // fetoscop controller
        {
            serial = GameObject.Find("SerialController");
            SerialController serialScript = serial.GetComponent<SerialController>();

            yaw = serialScript.rotX;
            pitch = serialScript.rotY;

            //this clamps the amount the fetoscope can rotate
            yaw = Mathf.Clamp(yaw, -35, 85);
            pitch = Mathf.Clamp(pitch, 0, 120);

            transform.eulerAngles = new Vector3(0.0f, yaw, pitch);
            roll = serialScript.rotZ;

            if (roll >= 360)
            {
                roll = roll % 360;
            }
            fetoCam.transform.eulerAngles = new Vector3(fetoCam.transform.eulerAngles.x, fetoCam.transform.eulerAngles.y, roll);
        }

    }

}
