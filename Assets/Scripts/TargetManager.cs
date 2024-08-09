using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
public class TargetManager : MonoBehaviour
{
    public GameObject board1;
    public GameObject board2;
    public GameObject board3;
    public GameObject decal;
    ProgressTrack finCheck;

    public UserMenu_Simulation sim;

    [Header("Gameplay Report")]
    public GameObject levelReport;
    public TextMeshProUGUI[] accuracyTxt, timeTxt;
    public TextMeshProUGUI scoreTxt, gradeText;
    public FetoscopeLaser laser;

    public bool nextLevel = false;

    public NearFarInteractor leftHand, rightHand;

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
       /* if (Input.GetKeyDown(KeyCode.Keypad7))
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
            StartCoroutine(SwapBoard());
            finCheck.isFinish = false;
        }*/

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
            StartCoroutine(SwapBoard());
            finCheck.isFinish = false;
        }

        if(levelReport.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.Confined;
        }
    }

    public void NextLevel()
    {
        nextLevel = true;
    }
    IEnumerator SwapBoard()
    {
        Gamification.instance.SetTimer(false);

        if (board1.activeSelf == true)
        {
            levelReport.SetActive(true);
            accuracyTxt[0].text = "Accuracy: " + Gamification.instance.HitPercentage().ToString("0") + "%";
            timeTxt[0].text = "Time: " + Gamification.instance.TimeSpent().ToString("0.0");
            Gamification.instance.NextBoard();
            scoreTxt.text = "Total Score: " + Gamification.instance.score;

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            leftHand.enableFarCasting = true;
            rightHand.enableFarCasting = true;

            board1.SetActive(false);
            board3.SetActive(false);

            yield return new WaitUntil(() => nextLevel);

            leftHand.enableFarCasting = false;
            rightHand.enableFarCasting = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            nextLevel = false;
            levelReport.SetActive(false);
            board2.SetActive(true);
        }
        else if (board2.activeSelf == true)
        {
            levelReport.SetActive(true);
            accuracyTxt[1].text = "Accuracy: " + Gamification.instance.HitPercentage().ToString("0") + "%";
            timeTxt[1].text = "Time: " + Gamification.instance.TimeSpent().ToString("0.0");
            Gamification.instance.NextBoard();
            scoreTxt.text = "Total Score: " + Gamification.instance.score;

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            leftHand.enableFarCasting = true;
            rightHand.enableFarCasting = true;

            board1.SetActive(false);
            board2.SetActive(false);

            yield return new WaitUntil(() => nextLevel);

            leftHand.enableFarCasting = false;
            rightHand.enableFarCasting = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1f;
            nextLevel = false;
            levelReport.SetActive(false);
            board3.SetActive(true);
        }
        else if (board3.activeSelf == true)
        {
            levelReport.SetActive(true);
            accuracyTxt[2].text = "Accuracy: " + Gamification.instance.HitPercentage().ToString("0") + "%";
            timeTxt[2].text = "Time: " + Gamification.instance.TimeSpent().ToString("0.0");
            Gamification.instance.NextBoard();
            scoreTxt.text = "Total Score: " + Gamification.instance.score;
            board1.SetActive(false);
            board2.SetActive(false);
            board3.SetActive(false);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.Confined;
            leftHand.enableFarCasting = true;
            rightHand.enableFarCasting = true;

            scoreTxt.text = "Total Score: " + Gamification.instance.score;
            gradeText.text = "Grade: " + Gamification.instance.Grade();
        }
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Decal");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

        Gamification.instance.SetTimer(true);
    }

    void WriteFile()
    {
        string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string path = Application.persistentDataPath + "/ReportCard_" + dateTime + ".txt";


        // Check if the file already exists. If yes, delete it.
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        // Create a file to write to.
        using (StreamWriter sw = File.CreateText(path))
        {
            sw.WriteLine("Report Card");
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sw.WriteLine("");
            sw.WriteLine("Level 1:");
            sw.WriteLine(accuracyTxt[0].text);
            sw.WriteLine(timeTxt[0].text);
            sw.WriteLine("");
            sw.WriteLine("Level 2:");
            sw.WriteLine(accuracyTxt[1].text);
            sw.WriteLine(timeTxt[1].text);
            sw.WriteLine("");
            sw.WriteLine("Level 3:");
            sw.WriteLine(accuracyTxt[1].text);
            sw.WriteLine(timeTxt[1].text);
            sw.WriteLine("");
            sw.WriteLine("Total Score: " + Gamification.instance.score);
            sw.WriteLine("Grade: " + Gamification.instance.Grade());

        }

        Debug.Log("File created and written to: " + path);
    }

}
