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
    public bool simuation = false;

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
            rend.material = material2;
            this.enabled = false;

            if (!simuation)
            {
                foreach (Transform child in transform)
                {
                    // Destroy each child GameObject
                    Destroy(child.gameObject);
                }
            }
        }
    }
}
