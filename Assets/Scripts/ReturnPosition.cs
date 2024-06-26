using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPosition : MonoBehaviour
{
    public float vert = 0;
    public float horiz = 0;
    public float baseVert = 0;
    public float baseHoriz = 0;

    // Start is called before the first frame update
    void Start()
    {
        baseVert = this.gameObject.transform.position.z;
        baseHoriz = this.gameObject.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        vert = (this.gameObject.transform.position.z - baseVert) * 10;
        horiz = (this.gameObject.transform.position.x - baseHoriz) * 10;
    }
}
