using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelector : MonoBehaviour
{
    private GameObject mixedRealityObjects, targetBoardObjects, simulationObjects, surgeonTable;
    private FetoscopeRotation fetoscope;
    private int controlType;

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
            mixedRealityObjects = GameObject.Find("InteriorObjects");
            simulationObjects = GameObject.Find("PlacentaTargets");
            targetBoardObjects = GameObject.Find("BoardManager");
            surgeonTable = GameObject.Find("surgica_table");
            fetoscope = GameObject.Find("Fetoscope_Pivot").GetComponent<FetoscopeRotation>();

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

    public void DropDownSelect(int value)
    {
        Debug.Log("Control Mode Switched To " + value);
        controlType = value;
    }
    public void UpdateObjects()
    {
        mixedRealityObjects.SetActive(!boolMR); 
        if(fetoscope.cmode == 3 && boolMR)
        {
            surgeonTable.SetActive(false);
        }

        simulationObjects.SetActive(!boolTB); 
        targetBoardObjects.SetActive(boolTB);

        fetoscope.CmodeSelect(controlType);

        Destroy(gameObject);
    }
}
