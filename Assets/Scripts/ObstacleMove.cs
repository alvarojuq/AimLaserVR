using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    Vector3 endPos;
    public float duration = 4;
    //public Vector3 positionLeft;
    void Start()
    {
        if (transform.localPosition.z > 0)
        {
            endPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 7f);
        }
        else
        {
            endPos = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 7f);
        }
        StartCoroutine(LerpPosition(endPos, duration));
        Debug.Log("Moving!");
    }
 
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        while (true) { 
        float time = 0;
        
        Vector3 startPosition = transform.localPosition;
            Vector3 temp = startPosition;
        while (time < duration)
        {
            transform.localPosition = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
            startPosition = targetPosition;
            targetPosition = temp;
    }
    }
}
