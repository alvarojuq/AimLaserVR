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

    public UserMenu_Simulation sim;

    [Header("Gameplay Report")]
    public GameObject levelReport;
    public TextMeshProUGUI accuracyTxt, timeTxt, scoreTxt, nextText;
    public FetoscopeLaser laser;

    private bool inLevel = true;
    public bool nextLevel = false;

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

        if (!inLevel && (laser.fireInput.action.IsPressed() || laser.xrFireInput.IsPressed() || laser.isOn))
        {
            nextLevel = true;
        }
    }

    IEnumerator SwapBoard()
    {
        Gamification.instance.SetTimer(false);

        if (board1.activeSelf == true)
        {
            levelReport.SetActive(true);
            accuracyTxt.text = "Accuracy: " + Gamification.instance.HitPercentage().ToString("0") + "%";
            timeTxt.text = "Time: " + Gamification.instance.TimeSpent().ToString("0.0") + " seconds";
            Gamification.instance.NextBoard();
            scoreTxt.text = "Total Score: " + Gamification.instance.score;

            board1.SetActive(false);
            board3.SetActive(false);

            yield return new WaitForSeconds(1f);
            inLevel = false;
            yield return new WaitUntil(() => nextLevel);

            nextLevel = false; inLevel = true;
            levelReport.SetActive(false);
            board2.SetActive(true);
        }
        else if (board2.activeSelf == true)
        {
            levelReport.SetActive(true);
            accuracyTxt.text = "Accuracy: " + Gamification.instance.HitPercentage().ToString("0") + "%";
            timeTxt.text = "Time: " + Gamification.instance.TimeSpent().ToString("0.0") + " seconds";
            Gamification.instance.NextBoard();
            scoreTxt.text = "Total Score: " + Gamification.instance.score;

            board1.SetActive(false);
            board2.SetActive(false);

            yield return new WaitForSeconds(1f);
            inLevel = false;
            yield return new WaitUntil(() => nextLevel);

            nextLevel = false; inLevel = true;
            levelReport.SetActive(false);
            board3.SetActive(true);
        }
        else if (board3.activeSelf == true)
        {
            levelReport.SetActive(true);
            accuracyTxt.text = "Accuracy: " + Gamification.instance.HitPercentage().ToString("0") + "%";
            timeTxt.text = "Time: " + Gamification.instance.TimeSpent().ToString("0.0") + " seconds";
            Gamification.instance.NextBoard();
            nextText.text = "Fire to see results";
            scoreTxt.text = "Total Score: " + Gamification.instance.score;
            board1.SetActive(false);
            board2.SetActive(false);
            board3.SetActive(false);

            yield return new WaitForSeconds(1f);
            inLevel = false;
            yield return new WaitUntil(() => nextLevel);

            accuracyTxt.text = "Final Score: " + Gamification.instance.score;
            timeTxt.text = "Grade: " + Gamification.instance.Grade();
            scoreTxt.text = "";
            nextText.text = "Pause to exit simulation";
        }
        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Decal");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

        Gamification.instance.SetTimer(true);
    }
}
