using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio; //needed to use the Audio Mixer and control volume
using UnityEngine.UI; //nedded to access the UI elements
using TMPro; //needed to access TextMesh Pro

public class UserMenu_Settings : MonoBehaviour
{
    //A reference to the Audio Mixer used in the project
    public AudioMixer audioMixer;

    //These refer to the Sound FX slider in the Settings submenu, and will allow us to control the laser tone sound with the slider
    public Slider soundFXSlider;
    public TextMeshProUGUI soundFXValue;

    //a reference to whatever the current Master Volume from the Audio Mixer is, in case it had been changed
    private float currentVolume;

    //public static bool PlaystationIsOn;


    // Start is called before the first frame update
    void Start()
    {
        //this gets the slider referenced in the inspector window, and sets the Sound FX slider value to the appropriate number when the scene starts
        soundFXSlider.GetComponent<Slider>();

        //this accesses the master volume value at the start of the scene, in case it's changed
        //if it has changed, it ensures that the slider reflects the previously changed value
        audioMixer.GetFloat("masterVolume", out currentVolume);
        soundFXSlider.value = currentVolume;
        soundFXValue.text = (soundFXSlider.value + 80f) + "";
    }


    // Update is called once per frame
    void Update()
    {
        //this updates the SoundFX slider value as we change the FX volume
        soundFXValue.text = (soundFXSlider.value + 80f) + "";
    }


    //This method sets the Sound FX slider to control the Master Volume variable from the Audio Mixer
    public void SetFXvolume (float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }
}
