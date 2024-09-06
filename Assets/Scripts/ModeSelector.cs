using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelector : MonoBehaviour
{
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

        ObjectHolder.Instance.mixedRealityObjects.SetActive(!boolMR);

        if(controlType == 3 && boolMR)
        {
            ObjectHolder.Instance.surgeonTable.SetActive(false);
        }

        ObjectHolder.Instance.simulationObjects.SetActive(!boolTB);
        ObjectHolder.Instance.targetBoardObjects.SetActive(boolTB);

        ObjectHolder.Instance.fetoscope.CmodeSelect(controlType);

        Destroy(gameObject);
        Destroy(ObjectHolder.Instance.gameObject);
    }
}
