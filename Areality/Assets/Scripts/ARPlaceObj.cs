using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]


public class ARPlaceObj : MonoBehaviour
{
    public GameObject gameObjectToInstantiate;

    private GameObject spawnObj;
    private ARRaycastManager arCastManager;
    private Vector2 touchPos;
    private Vector2 touchPos2;

    float distance_current;
    float distance_previous;
    bool first_pinch = true;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Awake()
    {
        arCastManager = GetComponent<ARRaycastManager>();
    }

    bool TryGetTouchPos(out Vector2 touchPos)
    {
        if(Input.touchCount > 0)
        {
            touchPos = Input.GetTouch(0).position;
            return true;
        }

        touchPos = default;
        return false;
    }

    void Update()
    {
        if(!TryGetTouchPos(out Vector2 touchPos))
        {
            return;

            if(arCastManager.Raycast(touchPos, hits, TrackableType.PlaneWithinPolygon))
            {
                var hitPos = hits[0].pose;

                if( spawnObj == null)
                {
                    spawnObj = Instantiate(gameObjectToInstantiate, hitPos.position, hitPos.rotation);
                }
                else
                {
                    spawnObj.transform.position = hitPos.position;
                }
            }
        }
        if(Input.touchCount >1 && spawnObj)
        {
            touchPos = Input.GetTouch(0).position;
            touchPos2 = Input.GetTouch(1).position;
            distance_current = touchPos2.magnitude-touchPos.magnitude;

            if(first_pinch)
            {
                distance_previous = distance_current;
                first_pinch = false;
            }
            if(distance_current!=distance_previous)
            {
                Vector3 scale_value = spawnObj.transform.localScale * (distance_current / distance_previous);
                spawnObj.transform.localScale = scale_value;
                distance_previous = distance_current;
            }

        }
        
    }
}
