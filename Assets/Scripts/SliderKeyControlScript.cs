using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI; //nedded to access the UI elements
using TMPro; //needed to access TextMesh Pro

public class SliderKeyControlScript : MonoBehaviour
{
    //public variables have been set for each of the things we need to access (the sliders and the lights) as well as the variables we'll be controlling
    //the brightness is the initial value we want to set light intensity too, which will be used in the start function
    //the variation is by how much the slider will chance with a key press
    //the final TextMeshProGUI elmement in each sets refers to the TextMeshPro objects that change their value based on slider position

    public Light FetoscopeLight;
    public Slider FetoscopeBrightnessSlider;
    public float FetoscopeBrightness;
    public float BrightnessVariation;
    //the values below correspond to changing the text values displayed above the slider, which is separated from the actual slider values
    public TextMeshProUGUI BrightnessValue;
    public float BrightnessText;
    public float BrightnessTextVariation;
    //the game objects below correspond to the keyboard icons on either side of the slider
    public GameObject O_Default;
    public GameObject O_Active;
    public GameObject P_Default;
    public GameObject P_Active;


    //this slider will  access the HeNe light intensity
    public Light HeNeLaser;
    public Slider HeNeSlider;
    public float HeNeBrightness;
    public float HeNeVariation;
    //the values below correspond to changing the text values displayed above the slider, which is separated from the actual slider values
    public TextMeshProUGUI HeNeValue;
    public float HeNeText;
    public float HeNeTextVariation;
    //the game objects below correspond to the keyboard icons on either side of the slider
    public GameObject K_Default;
    public GameObject K_Active;
    public GameObject L_Default;
    public GameObject L_Active;

    //We will be piping the Laser Power value here into our Fetoscope Laser Script in order to control the Ray Cast range
    public Slider LaserPowerSlider;
    public float LaserPower;
    public float LaserVariation;
    //the values below correspond to changing the text values displayed above the slider, which is separated from the actual slider values
    public TextMeshProUGUI PowerValue;
    public float LaserPowerText;
    public float LaserPowerTextVariation = 5f;

    //the game objects below correspond to the keyboard icons on either side of the slider
    public GameObject N_Default;
    public GameObject N_Active;
    public GameObject M_Default;
    public GameObject M_Active;

    //this value determines how long the key UI icons flash when pressed 
    public float KeyPressFlash = 0.2f;

    private PlayerControls inputActions;

    // Start is called before the first frame update
    void Start()
    {
        //at the start of the scene we need to access the slider and the lights
        FetoscopeBrightnessSlider.GetComponent<Slider>();
        HeNeSlider.GetComponent<Slider>();
        LaserPowerSlider.GetComponent<Slider>();

        FetoscopeLight.GetComponent<Light>();
        HeNeLaser.GetComponent<Light>();

        //at the start of the scene, we also want to set the light intensity to our original brightness
        //if we don't do this, the intensity won't be consistent at the start of the scene with when we use the slider 
        FetoscopeLight.intensity = FetoscopeBrightness;
        HeNeLaser.intensity = HeNeBrightness;

        
        //this sets the correct value in above the slider at the start of the scene
        BrightnessValue.text = BrightnessText + "";
        HeNeValue.text = HeNeText + "";
        PowerValue.text = LaserPowerText + "";
    }

    private void Awake()
    {
        inputActions = new PlayerControls();

        inputActions.Brightness.Decrease.performed += ctx => DecreaseBrightness();
        inputActions.Brightness.Increase.performed += ctx => IncreaseBrightness();
        inputActions.HeNe.Decrease.performed += ctx => DecreaseHeNe();
        inputActions.HeNe.Increase.performed += ctx => IncreaseHeNe();
        inputActions.LaserPower.Decrease.performed += ctx => DecreaseLaserPower();
        inputActions.LaserPower.Increase.performed += ctx => IncreaseLaserPower();

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
        //this if statement ensures that slider values can't be updated while the sim is paused
        if (UserMenu_Simulation.SimIsPaused.Equals(false))
        {
            //calling this during frame updates allows us to update the light intensities on key presses 
            FetoscopeLight.intensity = FetoscopeBrightness;
            HeNeLaser.intensity = HeNeBrightness;

            //this updates the values displayed above the sliders based on key presses
            BrightnessValue.text = BrightnessText + "";
            HeNeValue.text = HeNeText + "";
            PowerValue.text = LaserPowerText + "";

        }
    }

    //this assigns the slider movement to specific keys
    //GetKeyDown indicates that each "slide" happens with one key press, and holding the key won't do anything
    //the && operator creates a range that stop the user from increasing/decreasing the displayed brightness outside of the slider range
    //the coroutine being called flashes the UI element corresponding to the key being pressed
    private void DecreaseBrightness()
    {
        if (BrightnessText > 0f)
        {
            FetoscopeBrightnessSlider.value -= BrightnessVariation;
            BrightnessText -= BrightnessTextVariation;
            StartCoroutine(O_isPressed());
        }
    }

    private void IncreaseBrightness()
    {
        if (BrightnessText < 100f)
        {
            FetoscopeBrightnessSlider.value += BrightnessVariation;
            BrightnessText += BrightnessTextVariation;
            StartCoroutine(P_isPressed());
        }
    }

    private void DecreaseHeNe()
    {
        if (HeNeText > 1f)
        {
            HeNeSlider.value -= HeNeVariation;
            HeNeText -= HeNeTextVariation;
            StartCoroutine(K_isPressed());
        }
    }

    private void IncreaseHeNe()
    {
        if (HeNeText < 5f)
        {
            HeNeSlider.value += HeNeVariation;
            HeNeText += HeNeTextVariation;
            StartCoroutine(L_isPressed());
        }
    }

    private void DecreaseLaserPower()
    {
        if (LaserPowerText > 10f)
        {
            LaserPowerSlider.value -= LaserVariation;
            LaserPowerText -= LaserPowerTextVariation;
            StartCoroutine(N_isPressed());
        }
    }

    private void IncreaseLaserPower()
    {
        if (LaserPowerText < 60f)
        {
            LaserPowerSlider.value += LaserVariation;
            LaserPowerText += LaserPowerTextVariation;
            StartCoroutine(M_isPressed());
        }
    }


    //a public method to change the fetoscope brightness as well as the value in the text above it
    public void AdjustFetoscopeBrightness(float newBrightness)
    {
        FetoscopeBrightness = newBrightness;
    }


    //a public method to change the HeNe brightness as well as the value in the text above it
    public void AdjustHeNeBrightness(float newHeNeBrightness)
    {
        HeNeBrightness = newHeNeBrightness;
    }


    //a public method to change the laser power as well as the value in the text above it
    public void AdjustPower(float newLaserPower)
    {
        LaserPower = newLaserPower;
    }


    //the coroutines below are used to flash the corresponding UI icon when a key is pressed to change a slider value
    private IEnumerator O_isPressed()
    {
        O_Default.SetActive(false);
        O_Active.SetActive(true);
        yield return new WaitForSeconds(KeyPressFlash);
        O_Default.SetActive(true);
        O_Active.SetActive(false);
    }


    private IEnumerator P_isPressed()
    {
        P_Default.SetActive(false);
        P_Active.SetActive(true);
        yield return new WaitForSeconds(KeyPressFlash);
        P_Default.SetActive(true);
        P_Active.SetActive(false);
    }


    private IEnumerator K_isPressed()
    {
        K_Default.SetActive(false);
        K_Active.SetActive(true);
        yield return new WaitForSeconds(KeyPressFlash);
        K_Default.SetActive(true);
        K_Active.SetActive(false);
    }


    private IEnumerator L_isPressed()
    {
        L_Default.SetActive(false);
        L_Active.SetActive(true);
        yield return new WaitForSeconds(KeyPressFlash);
        L_Default.SetActive(true);
        L_Active.SetActive(false);
    }


    private IEnumerator N_isPressed()
    {
        N_Default.SetActive(false);
        N_Active.SetActive(true);
        yield return new WaitForSeconds(KeyPressFlash);
        N_Default.SetActive(true);
        N_Active.SetActive(false);
    }


    private IEnumerator M_isPressed()
    {
        M_Default.SetActive(false);
        M_Active.SetActive(true);
        yield return new WaitForSeconds(KeyPressFlash);
        M_Default.SetActive(true);
        M_Active.SetActive(false);
    }

}
