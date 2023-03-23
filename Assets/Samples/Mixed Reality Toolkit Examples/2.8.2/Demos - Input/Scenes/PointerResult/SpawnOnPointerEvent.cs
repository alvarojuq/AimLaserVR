using Microsoft.MixedReality.Toolkit.Input;
using UnityEngine;

namespace Microsoft.MixedReality.Toolkit.Examples.Demos
{
    
    // Example script that spawns a prefab at the pointer hit location.
    [AddComponentMenu("Scripts/MRTK/Examples/SpawnOnPointerEvent")]
    public class SpawnOnPointerEvent : MonoBehaviour
    {
        //public GameObject CursorHighlight;
        public GameObject PrefabToSpawn;
        private bool activeLaser;
        public GameObject tracker;

        
        private void OnEnable()
        {
            //cur.Pointer.IsTargetPositionLockedOnFocusLock = false;
            FileWriter trackScript = tracker.GetComponent<FileWriter>();
        }

        public void Spawn(MixedRealityPointerEventData eventData)
        {
                if (PrefabToSpawn != null)
                {
                    var result = eventData.Pointer.Result;                 
                    Instantiate(PrefabToSpawn, result.Details.Point, Quaternion.LookRotation(result.Details.Normal));
                }
        
        }
        public void SpawnHit(BaseInputEventData eventData)
        {
            FileWriter trackScript = tracker.GetComponent<FileWriter>();
            foreach (var ptr in eventData.InputSource.Pointers)
            {
                // An input source has several pointers associated with it, if you handle OnInputDown all you get is the input source
                // If you want the pointer as a field of eventData, implement IMixedRealityPointerHandler
                if (ptr.Result != null && ptr.Result.CurrentPointerTarget.transform.IsChildOf(transform))
                {   
                    ptr.IsTargetPositionLockedOnFocusLock = false;
                    //Debug.Log($"InputDown and Pointer {ptr.PointerName} is focusing this object or a descendant");
                    if (PrefabToSpawn != null)
                    {
                        //activeLaser = true;
                   
                        var result = ptr.Result;
                        Instantiate(PrefabToSpawn, result.Details.Point, Quaternion.LookRotation(result.Details.Normal));
                        StartCoroutine(trackScript.TrigCheck());
                        CheckHit hitUp = ptr.Result.CurrentPointerTarget.GetComponent<CheckHit>();
                        hitUp.progress++;
                    }
                }
                //Debug.Log($"InputDown fired, pointer {ptr.PointerName} is attached to input source that fired InputDown");
            }
        }

        public void Spawn2 (BaseInputEventData eventData)
        {
            //activeLaser = false;
        }
    }
    
}