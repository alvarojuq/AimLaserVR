using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideCheck : MonoBehaviour
{
    private int progress = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnNotify(string flag)
    {
        if (flag == "First" && progress == 0) { 
            Debug.Log("collided with Player, trigger at " + flag);
            progress = 1;
        }
        if (flag == "Second" && progress == 1)
        {
            Debug.Log("collided with Player, trigger at " + flag);
            progress = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
