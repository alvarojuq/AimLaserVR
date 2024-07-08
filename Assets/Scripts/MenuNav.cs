using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuNav : MonoBehaviour
{
    public int selectedIndex = 0;
    public GameObject[] buttons;

    private void OnEnable()
    {
        selectedIndex = 0;
        EventSystem.current.SetSelectedGameObject(buttons[selectedIndex]);
    }
    public void OnInputUp()
    {
        if(selectedIndex != 0)
        {
            selectedIndex -= 1;
        }
        else
        {
            selectedIndex = buttons.Length - 1;
        }
        EventSystem.current.SetSelectedGameObject(buttons[selectedIndex]);
    }
    public void OnInputDown()
    {
        if (selectedIndex < buttons.Length - 1)
        {
            selectedIndex += 1;
        }
        else
        {
            selectedIndex = 0;
        }
        EventSystem.current.SetSelectedGameObject(buttons[selectedIndex]);
    }
}
