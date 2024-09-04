using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class FetoscopeMovement : MonoBehaviour
{
    public GameObject refObj;
    public GameObject serial;
    //variable to attach the fetoscope character controller 
    public CharacterController fetoscopeCC;
    Rigidbody m_Rigidbody;

    //variable to set the speed of the fetoscope moving in and out using the direction keys
    public float fetoscopeDirectionSpeed;
    //variable to set the speed of the fetoscope moving in and out using the scroll wheel
    public float fetoscopeScrollSpeed;
    //variable to set the speed of the fetoscope moving in and out using the mouse and PS3 controller
    public float fetoscopePS3Speed;

    //private variables for the direction the fetoscope moves based on key or scroll input, set to 0 to start
    private Vector3 moveDirection = Vector3.zero;
    private Vector3 scrollDirection = Vector3.zero;
    private Vector3 VRScrollDirection = Vector3.zero;
    private Vector3 PS3moveDirection = Vector3.zero;

    bool noVerticalInput;
    bool noScrollInput;

    //a public reference to the Control Toggle in the settings menu
    public Toggle ControlToggle;

    GameObject link;
    SerialController serialScript;

    // Input actions
    private PlayerControls inputActions;
    private InputAction vrInputActions;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharacterController>();
        m_Rigidbody = GetComponent<Rigidbody>();
        //fetoscopeCC.transform.localPosition.y = -15;

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
    // Update is called once per frame
    void Update()
    {
        //calls the methods to move the fetoscope
        MoveFetoscope();
    }
    void MoveFetoscope()
    {

        link = GameObject.Find("Fetoscope_Pivot");
        FetoscopeRotation fRot = link.GetComponent<FetoscopeRotation>();
        //this if statement restrict backwards movement of the fetoscope, and avoid a bug that causes the user to get stuck
        //the user gets stuck when the fetoscope CC gets too close to the collider of the uterine wall
        //additionally, this big can cause the user to exit the uterus 
        if (fetoscopeCC.transform.localPosition.y > 1)
        {
            //Debug.Log("Out of bounds");
            Vector3 newPosition = new Vector3(0, 1.0f, 0);
            fetoscopeCC.transform.localPosition = newPosition;
        }
        else
        {

            if (fRot.cmode == 0) // mouse and keyboard
            {
                // Read vertical movement input
                float verticalInput = inputActions.Default.VerticalMovement.ReadValue<float>();
                Vector3 moveDirection = transform.forward * verticalInput * fetoscopeDirectionSpeed;

                 // Read scroll wheel input
                  float scrollInput = inputActions.Default.ScrollWheel.ReadValue<float>();
                  Vector3 scrollDirection = transform.forward * scrollInput * fetoscopeScrollSpeed * 0.01f;

                // Determine if either input type is being used
                bool noVerticalInput = Mathf.Approximately(verticalInput, 0f);
               // bool noScrollInput = Mathf.Approximately(scrollInput, 0f);

                // Apply movement based on input
                if (!noVerticalInput)
                {
                    // If vertical input is present, move using keyboard input
                    fetoscopeCC.Move(moveDirection * Time.deltaTime);
                }
                else if (!noScrollInput)
                {
                    // If scroll input is present, move using scroll wheel input
                    fetoscopeCC.Move(scrollDirection * Time.deltaTime);
                }
            }
            else if (fRot.cmode == 1) // gamepad
            { 
                PS3moveDirection = (transform.forward * inputActions.Joystick.LStickY.ReadValue<float>() * fetoscopePS3Speed);
                //PS3moveDirection = (transform.forward * Input.GetAxis("Mouse Y") * fetoscopePS3Speed);

                if(Mathf.Abs(inputActions.Joystick.LStickY.ReadValue<float>()) > 0.05f)
                    fetoscopeCC.Move(PS3moveDirection * Time.deltaTime);
            }
            else if (fRot.cmode == 2) // vr controllers
            {
                PS3moveDirection = (transform.forward * inputActions.Joystick.LStickY.ReadValue<float>() * fetoscopePS3Speed);
                //PS3moveDirection = (transform.forward * Input.GetAxis("Mouse Y") * fetoscopePS3Speed);

                if (Mathf.Abs(inputActions.Joystick.LStickY.ReadValue<float>()) > 0.05f)
                    fetoscopeCC.Move(PS3moveDirection * Time.deltaTime);
            }
            else if (fRot.cmode == 3) // fetoscope
            {

                if (fetoscopeCC.transform.localPosition.y > serialScript.localPos)
                {
                    moveDirection = (transform.forward);
                    // while (fetoscopeCC.transform.localPosition.y > (serialScript.localPos+0.25))
                    fetoscopeCC.Move(moveDirection * Time.deltaTime * fetoscopeDirectionSpeed);
                }
                if (fetoscopeCC.transform.localPosition.y < serialScript.localPos)
                {
                    moveDirection = (-transform.forward);
                    // while (fetoscopeCC.transform.localPosition.y < (serialScript.localPos-0.25))
                    fetoscopeCC.Move(moveDirection * Time.deltaTime * fetoscopeDirectionSpeed);
                }
            }
            
            
        }
    }

}
