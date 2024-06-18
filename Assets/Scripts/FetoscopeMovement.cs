using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //allows access to UI elements 

public class FetoscopeMovement : MonoBehaviour, IMixedRealityInputHandler<Vector2>
{
    public GameObject refObj;
    public GameObject serial;
    //variable to attach the fetoscope character controller 
    public CharacterController fetoscopeCC;
    Rigidbody m_Rigidbody;

    public MixedRealityInputAction moveAction;
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

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CharacterController>();
        m_Rigidbody = GetComponent<Rigidbody>();
        //fetoscopeCC.transform.localPosition.y = -15;
    }

   
    // Update is called once per frame
    void Update()
    {
        //calls the methods to move the fetoscope
        MoveFetoscope();
    }

    public void OnInputChanged(InputEventData<Vector2> eventData)
    {
        if (eventData.MixedRealityInputAction == moveAction)
        {
            Vector3 localDelta = (Vector3)eventData.InputData;
            VRScrollDirection = (transform.forward * (float) localDelta.y * fetoscopePS3Speed);
            fetoscopeCC.Move(VRScrollDirection * Time.deltaTime);
        }
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

            if (fRot.cmode == 0)
            {
                //with the vector3 variable moveDirection, we are setting it equal to the forward direction of the fetoscope controller, based on the arrow key input and multipled by the fetoscope direction speed
                moveDirection = (transform.forward * Input.GetAxis("Vertical") * fetoscopeDirectionSpeed);
                //with the vector3 variable scrollDirection, we are setting it equal to the forward direction of the fetoscope controller, based on the mouse scroll whell input and multiplied by the fetoscope scroll speed
                scrollDirection = (transform.forward * Input.GetAxis("Mouse ScrollWheel") * fetoscopeScrollSpeed);


                //the bools below check to see if either the keys or scroll wheel is being used
                //Mathf.Approximately returns true if the two values it's comparing are similar
                //this means the bools below will only return true when either the keys are pressed or the wheel isn't scrolled
                noVerticalInput = Mathf.Approximately(Input.GetAxis("Vertical"), 0f);
                noScrollInput = Mathf.Approximately(Input.GetAxis("Mouse ScrollWheel"), 0f);
                if (Input.GetKey(KeyCode.K))
                {
                    fetoscopeCC.Move(moveDirection * Time.deltaTime * fetoscopeDirectionSpeed);
                }
                if (Input.GetKey(KeyCode.I))
                {
                    fetoscopeCC.Move(-moveDirection * Time.deltaTime * fetoscopeDirectionSpeed);
                }

                //the bools from above are then used here to ensure the two control types don't have an additional effect on the speed of movement
                //using the bools and if statements means that the keyboard and mouse scroll wheel work independtly, rather than together 
                if (noVerticalInput == true)
                {
                    fetoscopeCC.Move(scrollDirection * Time.deltaTime);
                }
                else if (noScrollInput == true)
                {
                    fetoscopeCC.Move(moveDirection * Time.deltaTime);
                }
            }
            else if (fRot.cmode == 1)
            {
                PS3moveDirection = (transform.forward * Input.GetAxis("Mouse Y") * fetoscopePS3Speed);

                fetoscopeCC.Move(PS3moveDirection * Time.deltaTime);
            }
            else if (fRot.cmode == 2)
            {
                //PS3moveDirection = (transform.forward * Input.GetAxis("Mouse Y") * fetoscopePS3Speed);
                //scrollDirection = (transform.forward * Input.GetAxis("PS3 RStick Y") * fetoscopePS3Speed);
                fetoscopeCC.Move(VRScrollDirection * Time.deltaTime);
            }
            else if (fRot.cmode == 3)
            {
                serial = GameObject.Find("SerialController");
                SerialController serialScript = serial.GetComponent<SerialController>();

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
