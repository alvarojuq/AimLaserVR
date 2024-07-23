using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetManager : MonoBehaviour
{
    public GameObject board1;
    public GameObject board2;
    public GameObject board3;
    public GameObject decal;
    ProgressTrack finCheck;

    [Header("Gameplay Report")]
    public GameObject levelReport;
    public TextMeshProUGUI accuracyTxt, timeTxt, scoreTxt, timeLeftTxt;

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
            StartCoroutine(SwapBoard());
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
            StartCoroutine(SwapBoard());
            finCheck.isFinish = false;
        }
    }

    IEnumerator SwapBoard()
    {
        Gamification.instance.SetTimer(false);

        if (board1.activeSelf == true)
        {
            levelReport.SetActive(true);
            StartCoroutine(TimeToNextLevel("Next Level in ", 5));
            accuracyTxt.text = "Accuracy: " + Gamification.instance.HitPercentage().ToString("0") + "%";
            timeTxt.text = "Time: " + Gamification.instance.TimeSpent().ToString("0.0") + " seconds";
            Gamification.instance.NextBoard();
            scoreTxt.text = "Total Score: " + Gamification.instance.score;

            board1.SetActive(false);
            board3.SetActive(false);
            yield return new WaitForSeconds(5f);
            levelReport.SetActive(false);
            board2.SetActive(true);
        }
        else if (board2.activeSelf == true)
        {
            levelReport.SetActive(true);
            StartCoroutine(TimeToNextLevel("Next Level in ", 5));
            accuracyTxt.text = "Accuracy: " + Gamification.instance.HitPercentage().ToString("0") + "%";
            timeTxt.text = "Time: " + Gamification.instance.TimeSpent().ToString("0.0") + " seconds";
            Gamification.instance.NextBoard();
            scoreTxt.text = "Total Score: " + Gamification.instance.score;

            board1.SetActive(false);
            board2.SetActive(false);
            yield return new WaitForSeconds(5f);
            levelReport.SetActive(false);
            board3.SetActive(true);
        }
        else if (board3.activeSelf == true)
        {
            levelReport.SetActive(true);
            StartCoroutine(TimeToNextLevel("Final report in ", 5));
            accuracyTxt.text = "Accuracy: " + Gamification.instance.HitPercentage().ToString("0") + "%";
            timeTxt.text = "Time: " + Gamification.instance.TimeSpent().ToString("0.0") + " seconds";
            Gamification.instance.NextBoard();
            scoreTxt.text = "Total Score: " + Gamification.instance.score;

            board1.SetActive(false);
            board2.SetActive(false);
            board3.SetActive(false);
            this.enabled = false;
            yield return new WaitForSeconds(5f);

            accuracyTxt.text = "Final Score: " + Gamification.instance.score;
            timeTxt.text = "Grade: " + Gamification.instance.Grade();
            scoreTxt.text = "";
            timeLeftTxt.text = "";
        }
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Decal");
        foreach (GameObject obj in allObjects)
        {
                Destroy(obj);
        }

        Gamification.instance.SetTimer(true);
    }

    IEnumerator TimeToNextLevel(string context, int x)
    {
        for (int i = 0; i < x; i++)
        {
            timeLeftTxt.text = context + (x - i) + "s";
            yield return new WaitForSeconds(1f);
        }
    }
}
