using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardMove : MonoBehaviour
{
    // Start is called before the first frame update
    float mainSpeed = 1.0f; //regular speed
    //float shiftAdd = 250.0f; //multiplied by how long shift is held.  Basically running
    //float maxShift = 1000.0f; //Maximum speed when holdin gshift
    //float camSens = 0.25f; //How sensitive it with mouse
    //private Vector3 lastMouse = new Vector3(255, 255, 255); //kind of in the middle of the screen, rather than at the top (play)
    private float totalRun = 1.0f;

    void Update()
    {
        //lastMouse = Input.mousePosition - lastMouse;
        //lastMouse = new Vector3(-lastMouse.y * camSens, lastMouse.x * camSens, 0);
        //lastMouse = new Vector3(transform.eulerAngles.x + lastMouse.x, transform.eulerAngles.y + lastMouse.y, 0);
        //transform.eulerAngles = lastMouse;
        //lastMouse = Input.mousePosition;
        //Mouse  camera angle done.  

        //Keyboard commands
        float f = 0.0f;
        Vector3 p = GetBaseInput();
        if (p.sqrMagnitude > 0)
        { // only move while a direction key is pressed
            if (Input.GetKey(KeyCode.LeftShift))
            {
                totalRun += Time.deltaTime;
            }
            else
            {
                totalRun = Mathf.Clamp(totalRun * 0.5f, 1f, 1000f);
                p = p * mainSpeed;
            }

            p = p * Time.deltaTime;
            Vector3 newPosition = transform.position;
            if (Input.GetKey(KeyCode.Space))
            { //If player wants to move on X and Z axis only
                transform.Translate(p);
                newPosition.x = transform.position.x;
                newPosition.z = transform.position.z;
                transform.position = newPosition;
            }
            else
            {
                transform.Translate(p);
            }
        }

        //ExecuteEvents.Execute(buttonToFire.gameObject, new BaseEventData(EventSystem.current), ExecuteEvents.submitHandler);
    }

    private Vector3 GetBaseInput()
    { //returns the basic values, if it's 0 than it's not active.
        Vector3 p_Velocity = new Vector3();
        /*if (Input.GetAxis("Vertical")) ;
        {
            p_Velocity += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            p_Velocity += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            p_Velocity += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            p_Velocity += new Vector3(1, 0, 0);
        }*/

        // Query the value of the Horizontal Axis
        float horizontalInput = Input.GetAxis("Horizontal");
        // Similarly with the Vertical Axis
        float verticalInput = Input.GetAxis("Vertical");
        // Apply the inputs to a new Vector2
        p_Velocity = new Vector3(horizontalInput, 0, verticalInput);

        return p_Velocity;
    }
}
