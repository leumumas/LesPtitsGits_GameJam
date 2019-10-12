using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EarthSpinScript : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float speedZoom = 10f;
    [SerializeField]
    private float CloseValueCamera = -1.5f;
    [SerializeField]
    private float FarValueCamera = -4f;

    private Camera MainCamera;

    private float StartTime;
    private float journeyLength = 0f;
    private Vector3 StartPosition;
    private Vector3 EndPosition;

    private Vector3 FarPosition;
    private Vector3 ClosePosition;

    private void Start()
    {
        /*MainCamera = Camera.main;
        FarPosition = new Vector3(0, 0, FarValueCamera);
        ClosePosition = new Vector3(0, 0, CloseValueCamera);
        StartPosition = FarPosition;
        EndPosition = FarPosition;
        StartTime = Time.time;
        MainCamera.transform.position = EndPosition;*/
    }

    void Update() 
    {
        /*if (Input.GetButtonDown("ZoomIn"))
        {
            StartTime = Time.time;
            journeyLength = Math.Abs(MainCamera.transform.position.z - CloseValueCamera);
            StartPosition = MainCamera.transform.position;
            EndPosition = ClosePosition;
        }

        if (Input.GetButtonDown("ZoomOut"))
        {
            StartTime = Time.time;
            journeyLength = Math.Abs(MainCamera.transform.position.z - FarValueCamera);
            StartPosition = MainCamera.transform.position;
            EndPosition = FarPosition;
        }
        
        float distCovered = (Time.time - StartTime) * speedZoom;
        
        float fractionOfJourney = distCovered / journeyLength;

        if (fractionOfJourney < 1f)
        {
            MainCamera.transform.position = Vector3.Lerp(StartPosition, EndPosition, fractionOfJourney);
        }

        float translation = -Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * speed;

        transform.Rotate(Vector3.up, rotation * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, translation * Time.deltaTime, Space.World);*/
    }
}