using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHit : MonoBehaviour
{

    // Update is called once per frame
    public Material material1;
    public Material material2;
    Renderer rend;
    public int progress = 0;
    public bool done = false;

    void Start()
    {
        rend = GetComponent<Renderer>();

        // At start, use the first material
        rend.material = material1;
        done = false;
    }


    void Update()
    {
        progress = Mathf.Clamp(progress, 0, 100);
        rend.material.Lerp(material1, material2, (progress / 100));
        //Debug.Log(this.name + " " + progress);

        if (progress >= 50)
        {
            done = true;
        }
    }
}
