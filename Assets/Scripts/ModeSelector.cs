using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ModeSelector : MonoBehaviour
{
    private int controlType;

    //Mixed reality, target boards, visible targets
    public Toggle toggleMR, toggleTB, toggleVT;
    public TMP_Dropdown controlDropdown, portDropdown;
    public bool boolMR = false, boolTB = false, boolVT = false;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if(controlDropdown && portDropdown)
        {
            controlDropdown.value = PlayerPrefs.GetInt("ControlDropdown");
            controlType = PlayerPrefs.GetInt("ControlDropdown");

            portDropdown.value = PlayerPrefs.GetInt("PortDropdown");
            string portName = "COM" + (PlayerPrefs.GetInt("PortDropdown") + 1); 
            PlayerPrefs.SetString("COMPort", portName);
        }
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
    public void UpdateVTToggle()
    {
        if (toggleVT.gameObject != null)
        {
            boolVT = toggleVT.isOn;
        }
    }
    public void UpdateTBToggle()
    {
        if (toggleTB.gameObject != null)
        {
            boolTB = toggleTB.isOn;

            if (boolTB == true)
            {
                toggleVT.isOn = false;
                boolVT = false;
            }

            toggleVT.gameObject.SetActive(!toggleTB.isOn);
        }
    }

    public void DropDownSelect(int value)
    {
        Debug.Log("Control Mode Switched To " + value);
        controlType = value;
        PlayerPrefs.SetInt("ControlDropdown", value);
    }
    public void PortSelect(int value)
    {
        string portName = "COM" + (value + 1);
        PlayerPrefs.SetString("COMPort", portName);
        PlayerPrefs.SetInt("PortDropdown", value);
    }
    public void UpdateObjects()
    {
        
        ObjectHolder.Instance.mixedRealityObjects.SetActive(!boolMR);

        if(controlType == 3 && boolMR)
        {
            ObjectHolder.Instance.surgeonTable.SetActive(false);
        }

        ObjectHolder.Instance.simulationObjects.GetComponent<PlacentaTargetManager>().visualTargets = boolVT;

        ObjectHolder.Instance.simulationObjects.SetActive(!boolTB);
        ObjectHolder.Instance.targetBoardObjects.SetActive(boolTB);

        ObjectHolder.Instance.fetoscope.CmodeSelect(controlType);


        Color color = ObjectHolder.Instance.simulationMaterial.color;
        color.a = boolVT ? 0.3f : 0f;
        ObjectHolder.Instance.simulationMaterial.color = color;

        Destroy(gameObject);
        Destroy(ObjectHolder.Instance.gameObject);
    }
}
