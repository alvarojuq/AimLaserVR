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
        if (transform.position.z > 0)
        {
            endPos = new Vector3(transform.position.x, transform.position.y, -3.5f);
        }
        else
        {
            endPos = new Vector3(transform.position.x, transform.position.y, 3.5f);
        }
        StartCoroutine(LerpPosition(endPos, duration));
    }
 
    IEnumerator LerpPosition(Vector3 targetPosition, float duration)
    {
        while (true) { 
        float time = 0;
        
        Vector3 startPosition = transform.position;
            Vector3 temp = startPosition;
        while (time < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
            startPosition = targetPosition;
            targetPosition = temp;
    }
    }
}
