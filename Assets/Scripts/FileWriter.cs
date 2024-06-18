using System.Collections;
using System.Collections.Generic;
using HP.Omnicept.Messaging.Messages;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;
using HP.Glia.Examples.Display;

public class FileWriter : UGUIBaseDisplay
{
    public GameObject position;
    public GameObject rotation;
    public GameObject rollCam;
    private GameObject laser;
    private bool handLaser;
    private Vector2 rightGazeTarget;
    private Vector2 leftGazeTarget;
    private string heartRead;
    StreamWriter writer = null;

    private void OnEnable()
    {
        gliaBehaviour.OnEyeTracking.AddListener(OnEyeTracking);
        gliaBehaviour.OnHeartRate.AddListener(OnHeartRate);
        //gliaBehaviour.OnHeartRateVariabiility.AddListener(OnHeartRate);
        position = GameObject.Find("Main Camera");
        rotation = GameObject.Find("Main Camera");
        //rollCam = GameObject.Find("AR_Camera");      

        string filePath = getPath();
        if (!File.Exists(filePath))
        {
            File.Create(filePath).Close();
        }
        writer = new StreamWriter(filePath);
    }
    private void OnDisable()
    {
        writer.Close();
    }
    void Update()
    {
        SaveInput();
    }

    void SaveInput()
    {
        /*
        position = GameObject.Find("Fetoscope_MoveController");
        FetoscopeMovement posScript = position.GetComponent<FetoscopeMovement>();
        rotation = GameObject.Find("Fetoscope_Pivot");
        FetoscopeRotation rotScript = rotation.GetComponent<FetoscopeRotation>();
        */
        //laser = GameObject.Find("Main Camera");
        //VRLaser laserScript = laser.GetComponent<VRLaser>();
        //bool finalCheck = (bool)laserScript.isOn && handLaser;

        //eyetracker = GameObject.Find("Eyes");
        //heartrate = GameObject.Find("HRText");

            //This is writing the line of the type, name, damage... etc... (I set these)
        //writer.WriteLine("PlaceHolder_Timestamp");
            //This loops through everything in the inventory and sets the file to these.
            writer.WriteLine(
                Time.realtimeSinceStartup.ToString() +
                "," + position.transform.position.x.ToString() +
                "," + position.transform.position.y.ToString() +
                "," + position.transform.position.z.ToString() +
                "," + position.transform.rotation.x.ToString() +
                "," + position.transform.rotation.y.ToString() +
                "," + position.transform.rotation.z.ToString() +
                "," + heartRead +
                "," + rightGazeTarget.x.ToString() +
                "," + rightGazeTarget.y.ToString() +
                "," + leftGazeTarget.x.ToString() +
                "," + leftGazeTarget.y.ToString() //+
                //"," + finalCheck
                );
       //        "," + laserScript.isOn.ToString());

            writer.Flush();


    }
    private void OnEyeTracking(EyeTracking eyeTracking)
    {
        if (eyeTracking != null)
        {
            rightGazeTarget = new Vector2(eyeTracking.CombinedGaze.X, -eyeTracking.CombinedGaze.Y);
            leftGazeTarget = new Vector2(eyeTracking.CombinedGaze.X, -eyeTracking.CombinedGaze.Y);
            //rightPupilSizeTarget = eyeTracking.RightEye.PupilDilation / 10f;
            //leftPupilSizeTarget = eyeTracking.LeftEye.PupilDilation / 10f;
        }
    }
    private void OnHeartRate(HeartRate hr)
    {
        if (hr != null)
        {
            heartRead = hr.Rate.ToString();
        }
    }

    public IEnumerator TrigCheck()
    {
        handLaser = true;
        yield return new WaitForSeconds(0.2f);
        handLaser = false;
    }

    private string getPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/CSV/" + "Input_Data.csv";
#elif UNITY_ANDROID
        return Application.persistentDataPath+"Saved_Inventory.csv";
#elif UNITY_IPHONE
        return Application.persistentDataPath+"/"+"Saved_Inventory.csv";
#else
        return Application.dataPath +"/"+"Saved_Inventory.csv";
#endif
    }

}
