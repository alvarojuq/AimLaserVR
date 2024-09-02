using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.UI;

public class FetoscopeLaser : MonoBehaviour
{
    public GameObject serial;
    //referencing the fetoscope camera, from which the laser raycasts will be fired
    public Camera fetoscopeCamera;

    //this creates a link to the decal prefab used to project the ablation onto the placenta
    public GameObject ablationPrefab;

    //public variables that will correspond to the laser power, in Watts, the range of the laser, which will scale with power, and the fire rate 
    //fire rate is controlled to avoid the accumulation of a massive number of decals, even though they are resource unintensive 
    //public float laserRange = 7f;
    public float laserRate = 10f;

    //a private variable that works with the laserRate to determine when it can be fired again, and thus when the ablation decal is placed again
    private float nextTimeToFire = 0f;

    //a public reference to the Audio Source used for the Laser Tone when it's fired
    public AudioSource LaserTone;

    //a public reference to the Damage Flash UI element that shows when an incorrect location is ablated
    public GameObject damageFlash;

    // Checks if serial footpedal has been pressed
    public bool isOn = false;
    SerialController serialScript;
    //Input Actions
    [Header("Input Actions")]
    public InputActionReference fireInput;
    public InputAction xrFireInput;

    private void Start()
    {
        //serial = GameObject.Find("SerialController");
       // serialScript = serial.GetComponent<SerialController>();
    }
    private void OnEnable()
    {
        fireInput.action.Enable();
        xrFireInput.Enable();
    }

    private void OnDisable()
    {
        fireInput.action.Disable();
        xrFireInput.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        //an if statement to check if we are firing the fetoscope laser. Fire1 is a default Unity inout that corresponds to the left mouse button
        //The && UserMenu_Simulation.SimIsPause(false) ensures that the fire method isn't accidentally called when the sim is paused

        if (!UserMenu_Simulation.SimIsPaused && (fireInput.action.IsPressed() || xrFireInput.IsPressed() || isOn))
        {
            //Debug.Log("test");
            nextTimeToFire = Time.time + 1f / laserRate;
            LaserFire();

            if (!LaserTone.isPlaying)
                LaserTone.Play();
        }
        else
        {
            LaserTone.Stop();
        }

        /*if (serialScript.connected == true)
        {
            if ((serialScript.laserOn == true) && UserMenu_Simulation.SimIsPaused.Equals(false))
            {
                isOn = true;
            }

            if ((serialScript.laserOn == false) && UserMenu_Simulation.SimIsPaused.Equals(false))
            {
                isOn = false;
            }
        }*/

    }

    void LaserFire()
    {
        //this calls the Laser Power value from the Slider script, and applies it as the range of the ray cast
        float laserRange = 20; //GameObject.FindGameObjectWithTag("UI").GetComponent<SliderKeyControlScript>().LaserPower;

        RaycastHit hit;
        //the code below says that if our laser hits something, something then happens
        //we need to access the transform of the camera to shot from it, and ensure it is the forward direction
        //the laser range at the end ensure that we only get the effect if it's within our specified range
        if (Physics.Raycast(fetoscopeCamera.transform.position, fetoscopeCamera.transform.forward, out hit, laserRange))
        {
            //Debug.Log(hit.transform.name);
            //Debug.DrawRay(fetoscopeCamera.transform.position, fetoscopeCamera.transform.forward * laserRange, Color.green,1.0f, true);

            //this sets any object hit as a GameObject that can be refered to 
            GameObject theObjectHit = hit.collider.gameObject;

            //this instaniates the ablation prefab, projection it onto the surface of the object that's hit as a temporary GameObject
            //This temp partis important in order to apply it to the parent of the object it's hitting, which allows it to follow colliders placed onto objects
            // the vector3.up, and hit.normal allow the projection to occur in the direction opposite of the objects normals
            //Instantiate(ablationPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, hit.normal));
            Vector3 newnorm = new Vector3(hit.normal.x - 90, hit.normal.y, hit.normal.z);
            //GameObject temp = Instantiate(ablationPrefab, hit.point, Quaternion.FromToRotation(Vector3.up, newnorm));
            GameObject temp = Instantiate(ablationPrefab, hit.point, Quaternion.LookRotation(hit.normal));

            float distance = Vector3.Distance(fetoscopeCamera.transform.position, hit.point);
            distance = Mathf.Clamp(distance, 0f, 20);
            float ablationSize = 1f - (distance / 20);
           // temp.GetComponent<DecalProjector>().size = new Vector3(ablationSize, ablationSize, ablationSize);
            temp.GetComponent<DecalProjector>().fadeFactor = ablationSize;
            

            //this allows us to instantiate the decal onto the parent of the object hit, ensuring the decals move with any colliders and animations 
            temp.transform.parent = theObjectHit.transform;
            if (theObjectHit.GetComponent<CheckHit>() && theObjectHit.GetComponent<CheckHit>().enabled)
            {
                CheckHit hitUp = theObjectHit.GetComponent<CheckHit>();
                hitUp.progress++;

                if (Gamification.instance)
                    Gamification.instance.Hit();
                if (PlacentaGamification.instance)
                    PlacentaGamification.instance.Hit();
            }
            //Debug.Log("Shooting!");

            //if the laser hits anything but the placental surface, the damage flash animation appears

            if(theObjectHit.gameObject.tag == "Target")
            {
                //Debug.Log("Target hit: " + theObjectHit.gameObject.name);
            }
            else
            {
                if (Gamification.instance)
                {
                    Gamification.instance.Miss();
                    StartCoroutine(DamageFlash());
                }
                if (PlacentaGamification.instance)
                {
                    PlacentaGamification.instance.Miss();
                    StartCoroutine(DamageFlash());
                }
            }
        }
    }

    //this is an enumterator that calls up the Damage Flash image and animation
    private IEnumerator DamageFlash()
    {
        damageFlash.SetActive(true);
        yield return new WaitForSeconds(2f);
        damageFlash.SetActive(false);
    }

}
