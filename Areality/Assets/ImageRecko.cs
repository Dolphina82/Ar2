using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.ARFoundation;

public class ImageRecko : MonoBehaviour
{
    private ARTrackedImageManager _artManager;


    private void Awake()
    {
        _artManager = FindObjectOfType < ARTrackedImageManager>();
    }

    public void OnEnable()
    {
        _artManager.trackedImagesChanged += OnImageChanged;
    }

    public void OnDisable()
    {
        _artManager.trackedImagesChanged -= OnImageChanged;

    }

    public void OnImageChanged(ARTrackedImagesChangedEventArgs arg)
    {
        foreach(var trackedImage in arg.added)
        {
            Debug.Log(trackedImage.name);
        }

    }
}
