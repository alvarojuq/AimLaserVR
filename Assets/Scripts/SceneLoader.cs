using System.Collections;
using System.Collections.Generic;
using UnityEngine; //required to switch scenes within Unity
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // This script also works as the scene manager
    // For the purposes of loading scenes: 
    // scene 0 =  TTTS Homepage
    // scene 1 = HiFi Simulator
    // scene 2 = LoFi Simulator


    //load TTTS Homepage
    public void HomePage()
    {
        SceneManager.LoadScene(0);
    }


    //Enter HiFi Simulator
    public void HiFiSim()
    {
        SceneManager.LoadScene(1);
    }


    //Enter LoFi Simulator
    public void LoFiSim()
    {
        SceneManager.LoadScene(2);
    }


    //Exit the Application
    public void ExitApp()
    {
        Application.Quit();
    }


    //Restart/reload the current scene
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
