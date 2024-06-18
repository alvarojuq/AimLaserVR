using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorChange : MonoBehaviour
{

    public Material Mat1;
    public Material Mat2;
    public Material Mat3;
    public Material Mat4;
    Material[] availableMaterials = new Material[4];
    


    GameObject link;
    

    // Start is called before the first frame update
    void Start()
    {
        availableMaterials[0] = Mat1;
        availableMaterials[1] = Mat2;
        availableMaterials[2] = Mat3;
        availableMaterials[3] = Mat4;
    }

    // Update is called once per frame
    void Update()
    {
        Material[] newMaterials = GetComponent<Renderer>().materials;

        link = GameObject.Find("Fetoscope_Pivot");
        FetoscopeRotation fRot = link.GetComponent<FetoscopeRotation>();
        if (fRot.cmode == 0)
        {
            newMaterials[1] = Mat1;
        }
        else if (fRot.cmode == 1)
        {
            newMaterials[1] = Mat2;
        }
        else if (fRot.cmode == 2)
        {
            newMaterials[1] = Mat3;
        }
        else if (fRot.cmode == 3)
        {
            newMaterials[1] = Mat4;
        }

        GetComponent<MeshRenderer>().materials = newMaterials;
    }
}
