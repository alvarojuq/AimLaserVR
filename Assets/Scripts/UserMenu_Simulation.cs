using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //nedded to access the UI elements
using UnityEngine.SceneManagement; //needed to switch between scenes
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class UserMenu_Simulation : MonoBehaviour
{
    //public variable to set if the simulator is paused or not
    public static bool SimIsPaused = false;

    //Gameobject for the Panel_Menus UI object, which holds all of the menus and submenus, as well as Gameobjects for those submenus
    public GameObject menuPanel;
    public GameObject mainMenu;
    public GameObject settingsMenu;

    public GameObject gameReport;
    private bool showReport = false;

    //Gameobject for the esc icon
    public GameObject escDefault;
    public GameObject escActive;

    public NearFarInteractor leftHand, rightHand;

    // Start is called before the first frame update
    void Start()
    {
        //we set this bool to be false when we start in order to ensure the scene starts correctly upon reloading
        SimIsPaused = false;
        //this ensures time is moving correctly when we restart the simulation
        Time.timeScale = 1f;
    }

    public void OnPause()
    {
        //a statment to make the pause action key pause and resume the game 
        if (SimIsPaused)
        {
            ResumeSim();
            leftHand.enableFarCasting = false;
            rightHand.enableFarCasting = false;
        }
        else
        {
            PauseSim();
            leftHand.enableFarCasting = true;
            rightHand.enableFarCasting = true;
        }
    }

    //a method to pause the simulator
    public void ResumeSim()
    {
        //this locks the cursor to the center of the game window, and hides the cursor on the game screen
        Cursor.lockState = CursorLockMode.Locked;

        if(showReport)
        {
            gameReport.SetActive(true);
        }
        showReport = false;

        menuPanel.SetActive(false);
        Time.timeScale = 1f;
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        escDefault.SetActive(true);
        escActive.SetActive(false);
        SimIsPaused = false;
    }


    //a method to resume the simulator 
    public void PauseSim()
    {
        //this leaves the cursor as is, just like normal
        Cursor.lockState = CursorLockMode.None;

        if(gameReport.activeInHierarchy)
        {
            gameReport.SetActive(false);
            showReport = true;
        }

        menuPanel.SetActive(true);
        Time.timeScale = 0f;
        escDefault.SetActive(false);
        escActive.SetActive(true);
        SimIsPaused = true;
    }
}
