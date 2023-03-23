using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //allows access to UI elements 

public class FetoscopeRotation : MonoBehaviour
{
    private GameObject fetoCam;
    public GameObject serial;

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
    //a public reference to the Control Toggle in the settings menu
    public Toggle ControlToggle;
    private void Start()
    {
        fetoCam = GameObject.Find("Fetoscope_Camera");
    }

    void Update()
    {
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
        //this states that if the Contol Toggle in the setting submenu is on, meaning the Playstion Controller is on, rotation is controlled by the analog sticks
        if (ControlToggle.isOn)
        {
            /* 
            //CONTROLLERCODE
            //Get the current orientation of the game controller
            //Convert the value for each axis to an angle (degrees)
            //In this case the range for each is different
            //the Yaw, or horizontal anlge can go from (-65, 65) degrees
            //the Pitch, or vertical angle can go from (-50, 50) degrees
            float angle_horiz =  65 + (65 * -Input.GetAxis("PS3 RStick X"));
            float angle_vert = 50 + (40 * -Input.GetAxis("PS3 RStick Y"));

            //Calculate the quaternion for the angles
            Quaternion qh = Quaternion.Euler(0, angle_horiz, angle_vert);

            //Rotate/orient the pivot using the quaternion
            transform.localRotation = qh;
            //CONTROLLERCODE ENDS
            */
            serial = GameObject.Find("SerialController");
            SerialController serialScript = serial.GetComponent<SerialController>();

            yaw = serialScript.rotX;
            pitch = serialScript.rotY;

            //this clamps the amount the fetoscope can rotate
            yaw = Mathf.Clamp(yaw, -35, 85);
            pitch = Mathf.Clamp(pitch, 0, 120);

            transform.eulerAngles = new Vector3(0.0f, yaw, pitch);

            /*if (transform.localRotation.y > yaw)
            {
                transform.Rotate(0.0f, -fetoscopeTurnSpeed, 0.0f);
            }
            if (transform.localRotation.y < yaw)
            { 
                transform.Rotate(0.0f, fetoscopeTurnSpeed, 0.0f);
            }

            if (transform.localRotation.z > pitch)
            {
                transform.Rotate(0.0f, 0.0f, -fetoscopeTurnSpeed);
            }
            if (transform.localRotation.z < pitch)
            {
                transform.Rotate(0.0f, 0.0f, fetoscopeTurnSpeed);
            }*/

            //sets the yaw and the pitch to respond to mouse movement in the X and Y axis, and multiples them by the mouse sensitivty
            //The -= is needed as it creates inverse controls, i.e. if the mouse moves up the fetoscope moves down
            roll = serialScript.rotZ;

            if (roll >= 360)
            {
                roll = roll % 360;
            }

            //rotTwo = new Vector3(roll, 0f, 0f);
            //rotTwo = fetoCam.transform.eulerAngles;
            //rotTwo.y = roll;

            fetoCam.transform.eulerAngles = new Vector3(fetoCam.transform.eulerAngles.x, fetoCam.transform.eulerAngles.y, roll);
            //Debug.Log(yaw + " " + pitch);

        }
        //else, AKA if the Control Toogle is set to Mouse and Keyboard, rotation is controlled by mouse movement
        else
        {
            //sets the yaw and the pitch to respond to mouse movement in the X and Y axis, and multiples them by the mouse sensitivty
            //The -= is needed as it creates inverse controls, i.e. if the mouse moves up the fetoscope moves down
            yaw -= Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity;

            //this clamps the amount the fetoscope can rotate
            yaw = Mathf.Clamp(yaw, -35, 85);
            pitch = Mathf.Clamp(pitch, 0, 120);

            transform.eulerAngles = new Vector3(0.0f, yaw, pitch);
        }
    }

}
