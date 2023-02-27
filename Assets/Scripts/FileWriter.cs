using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Text;
using System.IO;


public class FileWriter : MonoBehaviour
{
    public GameObject position;
    //public GameObject rotation;
    public GameObject rollCam;
    private GameObject laser;
    private bool handLaser;
    private GameObject eyetracker;
    private GameObject heartrate;
    StreamWriter writer = null;

    private void OnEnable()
    {
        position = GameObject.Find("Main Camera");
        //rotation = GameObject.Find("Main Camera");
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
        laser = GameObject.Find("Main Camera");
        VRLaser laserScript = laser.GetComponent<VRLaser>();
        bool finalCheck = (bool)laserScript.isOn && handLaser;

        eyetracker = GameObject.Find("Eyes");
        heartrate = GameObject.Find("HRText");

            //This is writing the line of the type, name, damage... etc... (I set these)
        writer.WriteLine("PlaceHolder_Timestamp");
            //This loops through everything in the inventory and sets the file to these.
            writer.WriteLine(position.transform.position.x.ToString() +
                "," + position.transform.position.y.ToString() +
                "," + position.transform.position.z.ToString() +
                "," + position.transform.rotation.y.ToString() +
                "," + position.transform.rotation.z.ToString() +
                "," + rollCam.transform.rotation.z.ToString() +
                "," + finalCheck);
       //        "," + laserScript.isOn.ToString());

            writer.Flush();


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
