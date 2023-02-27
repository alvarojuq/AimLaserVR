using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressTrack : MonoBehaviour
{
    public bool isFinish = false;
    public Component[] targets;
    int temp;

    // Start is called before the first frame update
    void Start()
    {
        isFinish = false;
    }

    // Update is called once per frame
    void Update()
    {
        int tracked = 0;
        targets = GetComponentsInChildren<CheckHit>();

        foreach (CheckHit check in targets)
        {
            
            if (check.done == false)
            {
                //Debug.Log("Checked: " + tracked);
                return;
            }
            tracked++;
            

        }

        isFinish = true;
    }
}
