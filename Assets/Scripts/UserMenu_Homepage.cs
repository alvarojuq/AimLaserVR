using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //nedded to access the UI elements
using UnityEngine.SceneManagement; //needed to switch between scenes

public class UserMenu_Homepage : MonoBehaviour
{
    //public variable to set if the simulator is paused or not
    public static bool MenuIsOpen = false;

    //Gameobject for the Panel_Menus UI object, which holds all of the menus and submenus, as well as Gameobjects for those submenus
    public GameObject menuPanel;
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject aboutMenu;

    //Gameobject for the esc icon
    public GameObject escDefault;
    public GameObject escActive;


    // Start is called before the first frame update
    void Start()
    {
        //we set this bool to be false when we start in order to ensure the scene starts correctly upon reloading
        MenuIsOpen = false;
        //this leaves the cursor as is, just like normal
        Cursor.lockState = CursorLockMode.None;
    }


    // Update is called once per frame
    void Update()
    {
        //a statment to make the esc key pause and resume the game 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuIsOpen)
            {
                CloseMenu();
            }
            else
            {
                OpenMenu();
            }
        }
    }


    //a method to pause the simulator
    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        aboutMenu.SetActive(false);
        escDefault.SetActive(true);
        escActive.SetActive(false);
        MenuIsOpen = false;
    }


    //a method to resume the simulator 
    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        escDefault.SetActive(false);
        escActive.SetActive(true);
        MenuIsOpen = true;
    }
}
