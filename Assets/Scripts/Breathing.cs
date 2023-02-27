using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //nedded to access the UI elements

public class Breathing : MonoBehaviour
{
    //a public variable to access the transform component of the GameObject holding our camera
    public Transform BreathController;
    //this vector will be used to set and record the origin point of the BreathController GameObject
    private Vector3 BreathControllerOirign;
    //a public variable to control the breathing intensity of the mother, could be increased to simulate a stressed mother, etc. 
    public float BreathingIntensity = 0.15f;
    //this variable is used to represent a point along the sin or cos wave being used to modulate the breathing shake 
    private float breathingCounter;
    //a boolean to determine if the mother is breathing or holding their breath
    private bool isBreathing;
    //a public reference to the hold breath UI element
    public GameObject HoldBreathDefault;
    public GameObject HoldBreathActive;


    // Start is called before the first frame update
    void Start()
    {
        //at the start we want to set the isBreathing boolean to true, as the mother would be breathing 
        isBreathing = true;
        //this simply assigns the origin variable of the controller to where it is when the sim begins
        BreathControllerOirign = BreathController.localPosition;
    }


    // Update is called once per frame
    void Update()
    {
        //if the mother is breathing, activate the breathMovement method, and increase the breathcounting as time passes
        if (isBreathing == true)
        {
            BreathMovement();
            breathingCounter += Time.deltaTime;
            HoldBreathDefault.SetActive(true);
            HoldBreathActive.SetActive(false);
        }
        else if (!isBreathing)
        {
            //this may seem odd, but it means that the breathingCounter will stay constant when isBreathing doesn't equal true, AKA the mothers breath will be held
            breathingCounter += Time.deltaTime - Time.deltaTime;
            HoldBreathDefault.SetActive(false);
            HoldBreathActive.SetActive(true);
        }

        //if "H" is pressed, start the HoldBreath coroutine
        //the && stops the user from being able to spam "H" and continuously have the mother hold their breath
        if (Input.GetKeyDown(KeyCode.H) && isBreathing == true)
        {
            StartCoroutine(HoldBreath());
        }
    }


    //a method to simulate maternal breathing patterns
    void BreathMovement()
    {
        BreathController.localPosition = BreathControllerOirign + new Vector3(Mathf.Cos(breathingCounter * 2) * BreathingIntensity, Mathf.Sin(breathingCounter) * BreathingIntensity, BreathControllerOirign.z);
    }


    //a coroutine that simulates the mother holding her breath
    //isBreathing is set to false, locking the Breathing Controller posiition
    //breath is held for some random value between 5-9 seconds
    //after that time, isBreathing is set to true and motion resumes
    private IEnumerator HoldBreath()
    {
        isBreathing = false;
        float holdBreath = Random.Range(5f, 9f);
        yield return new WaitForSeconds(holdBreath);
        print("The mother held their breath for" + holdBreath + "seconds");
        isBreathing = true;
    }
}
