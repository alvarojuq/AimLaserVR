using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideMessage : MonoBehaviour
{
    public Renderer rend;
    public GameObject observer;
    public string flagName;

    // Start is called before the first frame update
    void Start()
    {
        //rend = GetComponent<Renderer>();
        //rend.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Laser"))
        {
            Debug.Log(other.gameObject.name);
            observer = GameObject.Find("EventController");
            CollideCheck observScript = observer.GetComponent<CollideCheck>();
            observScript.OnNotify(flagName);
            //Debug.Log("collided with Player");
            //rend.enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
