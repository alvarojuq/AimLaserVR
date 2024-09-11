using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PlacentaTargetManager : MonoBehaviour
{
    public GameObject decal;
    public ProgressTrack finCheck;

    public UserMenu_Simulation sim;
    public SceneLoader sceneLoader;

    [Header("Gameplay Report")]
    public GameObject levelReport;
    public TextMeshProUGUI accuracyTxt, timeTxt;
    public TextMeshProUGUI scoreTxt, gradeText;
    public FetoscopeLaser laser;

    public bool nextLevel = false;
    public bool endScene = false;
    public bool visualTargets = false;

    public NearFarInteractor leftHand, rightHand;

    // Update is called once per frame
    void Update()
    {
        if (finCheck.isFinish == true)
        {
            StartCoroutine(SwapBoard());
        }

        if (levelReport.activeInHierarchy)
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
        finCheck.isFinish = false;
        PlacentaGamification.instance.SetTimer(false);

        levelReport.SetActive(true);
        accuracyTxt.text = "Accuracy: " + PlacentaGamification.instance.HitPercentage().ToString("0") + "%";
        timeTxt.text = "Time: " + PlacentaGamification.instance.TimeSpent().ToString("0.0");
        PlacentaGamification.instance.NextBoard();
        scoreTxt.text = "Total Score: " + PlacentaGamification.instance.score;

        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        leftHand.enableFarCasting = true;
        rightHand.enableFarCasting = true;

        gradeText.text = "Grade: " + PlacentaGamification.instance.Grade();

        GameObject[] allObjects = GameObject.FindGameObjectsWithTag("Decal");
        foreach (GameObject obj in allObjects)
        {
            Destroy(obj);
        }

        yield return new WaitUntil(() => nextLevel);

        if (!endScene)
        {
            sceneLoader.HomePage();
            endScene = true;
        }
    }

    public void WriteFile()
    {
        string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
        string path = Application.persistentDataPath + "/SimulationReportCard_" + dateTime + ".txt";


        // Check if the file already exists. If yes, delete it.
        if (File.Exists(path))
        {
            File.Delete(path);
        }

        // Create a file to write to.
        using (StreamWriter sw = File.CreateText(path))
        {
            sw.WriteLine("Report Card - Placenta Simulation");
            sw.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            sw.WriteLine("Used visual targets: " + visualTargets);
            sw.WriteLine("");
            sw.WriteLine(accuracyTxt.text);
            sw.WriteLine(timeTxt.text);
            sw.WriteLine("");
            sw.WriteLine("Total Score: " + PlacentaGamification.instance.score);
            sw.WriteLine("Grade: " + PlacentaGamification.instance.Grade());

        }

        Debug.Log("File created and written to: " + path);
    }
}
