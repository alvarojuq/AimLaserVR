using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    public GameObject board1;
    public GameObject board2;
    public GameObject board3;
    public GameObject decal;
    ProgressTrack finCheck;

    // Start is called before the first frame update
    void Start()
    {
        board1.SetActive(true);
        board2.SetActive(false);
        board3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad7))
        {
            print("7 key was pressed");
            board1.SetActive(true);
            board2.SetActive(false);
            board3.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad8))
        {
            print("8 key was pressed");
            board1.SetActive(false);
            board2.SetActive(true);
            board3.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad9))
        {
            print("9 key was pressed");
            board1.SetActive(false);
            board2.SetActive(false);
            board3.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            print("Bar 9 key was pressed");
            SwapBoard();
            finCheck.isFinish = false;
        }

        //ProgressTrack finCheck;
        if (board1.activeSelf == true)
        {
            finCheck = board1.GetComponent<ProgressTrack>();
        }
        if (board2.activeSelf == true)
        {
            finCheck = board2.GetComponent<ProgressTrack>();
        }
        if (board3.activeSelf == true)
        {
            finCheck = board3.GetComponent<ProgressTrack>();
        }

        if (finCheck.isFinish == true)
        {
            SwapBoard();
            finCheck.isFinish = false;
        }
    }

    void SwapBoard()
    {
        if (board1.activeSelf == true)
        {
            board1.SetActive(false);
            board2.SetActive(true);
            board3.SetActive(false);
        }
        else if (board2.activeSelf == true)
        {
            board1.SetActive(false);
            board2.SetActive(false);
            board3.SetActive(true);
        }
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Decal");
        foreach (GameObject obj in allObjects)
        {
                Destroy(obj);
        }
    }

}
