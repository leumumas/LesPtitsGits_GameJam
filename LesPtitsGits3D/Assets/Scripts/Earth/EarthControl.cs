using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EarthControl : MonoBehaviour
{
    [SerializeField]
    private float speed = 10f;
    [SerializeField]
    private float speedZoom = 10f;
    [SerializeField]
    private float CloseValueCamera = -300f;
    [SerializeField]
    private float FarValueCamera = -800f;
    [SerializeField]
    private GameObject m_Tornado;

    private Camera MainCamera;

    private float StartTime;
    private float journeyLength = 0f;
    private Vector3 StartPosition;
    private Vector3 EndPosition;

    private Vector3 FarPosition;
    private Vector3 ClosePosition;
    
    private RaycastHit raycastHit;
    private Ray ray;

    private GlobalRegion m_LastRegion;

    private bool m_IsZoomOut = true;
    
    private void Start()
    {
        MainCamera = Camera.main;
        FarPosition = new Vector3(0, 0, FarValueCamera);
        ClosePosition = new Vector3(0, 0, CloseValueCamera);
        StartPosition = FarPosition;
        EndPosition = FarPosition;
        StartTime = Time.time;
        MainCamera.transform.position = EndPosition;
    }

    void Update() 
    {
        bool Change = false;
        if (Input.GetButtonDown("ZoomIn"))
        {
            StartTime = Time.time;
            journeyLength = Math.Abs(MainCamera.transform.position.z - CloseValueCamera);
            StartPosition = MainCamera.transform.position;
            EndPosition = ClosePosition;
            m_IsZoomOut = false;
            Change = true;
        }

        if (Input.GetButtonDown("ZoomOut"))
        {
            StartTime = Time.time;
            journeyLength = Math.Abs(MainCamera.transform.position.z - FarValueCamera);
            StartPosition = MainCamera.transform.position;
            EndPosition = FarPosition;
            m_IsZoomOut = true;
            Change = true;
        }

        float distCovered = (Time.time - StartTime) * speedZoom;
        
        float fractionOfJourney = distCovered / journeyLength;

        if (fractionOfJourney < 1f)
        {
            MainCamera.transform.position = Vector3.Lerp(StartPosition, EndPosition, fractionOfJourney);
        }

        float translation = -Input.GetAxis("Vertical") * (m_IsZoomOut ? speed : speed / 2);
        float rotation = Input.GetAxis("Horizontal") * (m_IsZoomOut ? speed : speed / 2);

        transform.Rotate(Vector3.up, rotation * Time.deltaTime, Space.World);
        transform.Rotate(Vector3.right, translation * Time.deltaTime, Space.World);

        ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
        if (Physics.Raycast(ray, out raycastHit))
        {
            GlobalRegion globalRegion = raycastHit.collider.GetComponent<GlobalRegion>();
            if (globalRegion != null && (globalRegion != m_LastRegion || Change))
            {
                if (m_LastRegion != null)
                {
                    RegionHandler.Instance.RegionOver(m_LastRegion, true);
                }
                m_LastRegion = globalRegion;
                if (m_IsZoomOut)
                {
                    RegionHandler.Instance.RegionOver(m_LastRegion, false);
                }
                RegionHandler.Instance.CurrentGlobalRegion = m_LastRegion;
            }
        }
    }
}