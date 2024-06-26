using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LasSound : MonoBehaviour
{
    public GameObject serial;

    //public variables that will correspond to the laser power, in Watts, the range of the laser, which will scale with power, and the fire rate 
    //fire rate is controlled to avoid the accumulation of a massive number of decals, even though they are resource unintensive 
    //public float laserRange = 7f;
    public float laserRate = 10f;

    //a public reference to the Audio Source used for the Laser Tone when it's fired
    public AudioSource LaserTone;

    public bool isOn = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        serial = GameObject.Find("SerialController");
        SerialController serialScript = serial.GetComponent<SerialController>();

        //The code below plays the laser tone sound as long as the fire buttons are held down
        //the GetKeyUp input means that upon the key being release, the sound is cut and stops playing
        if (Input.GetButtonDown("Fire1"))
        {
            if (!LaserTone.isPlaying)
            {
                LaserTone.Play();
            }
            Debug.Log("playing");
            isOn = true;
        }

        if (Input.GetButtonUp("Fire1"))
        {
            LaserTone.Stop();
            isOn = false;
        }

        if (Input.GetButtonDown("Fire2") && UserMenu_Simulation.SimIsPaused.Equals(false))
        {
            LaserTone.Play();
            isOn = true;
        }

        if (Input.GetButtonUp("Fire2") && UserMenu_Simulation.SimIsPaused.Equals(false))
        {
            LaserTone.Stop();
            isOn = false;
        }

        if ((serialScript.laserOn == true))
        {
            LaserTone.Play();
            isOn = true;
        }

        if ((serialScript.laserOn == false))
        {
            LaserTone.Stop();
            isOn = false;
        }

    }
}
