using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelector : MonoBehaviour
{
    private GameObject mixedRealityObjects, targetBoardObjects, simulationObjects;

    public Toggle toggleMR, toggleTB;
    public bool boolMR = false, boolTB = false;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (SceneManager.GetActiveScene().name != "TTTS_MainMenu")
        {
            mixedRealityObjects = GameObject.FindGameObjectWithTag("NonMixedRealityObjects");
            simulationObjects = GameObject.FindGameObjectWithTag("SimulationObjects");
            targetBoardObjects = GameObject.FindGameObjectWithTag("TargetBoardObjects");

            UpdateObjects();
        }
    }

    public void UpdateMRToggle()
    {
        if (toggleMR.gameObject != null)
        {
            boolMR = toggleMR.isOn;
        }
    }
    public void UpdateTBToggle()
    {
        if (toggleTB.gameObject != null)
        {
            boolTB = toggleTB.isOn;
        }
    }

    public void UpdateObjects()
    {
        mixedRealityObjects.SetActive(!boolMR); 
        simulationObjects.SetActive(!boolTB); 
        targetBoardObjects.SetActive(boolTB);

        Destroy(gameObject);
    }
}
