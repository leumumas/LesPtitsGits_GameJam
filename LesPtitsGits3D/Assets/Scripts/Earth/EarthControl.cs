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
    private float CloseValueCamera = -1.5f;
    [SerializeField]
    private float FarValueCamera = -4f;
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

    private bool m_CanSpawnTornado = true;
    private bool m_CanSpawnEarthQuake = true;
    private bool m_CanSpawnVolcano = true;

    private GameObject m_CurrentTornado;
    private Vector3 m_TornadoScale= new Vector3(0.02f, 0.02f, 0.02f);
    private Vector3 m_TornadoRotation = new Vector3(0f, 0f, 0f);

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
        if (Input.GetButtonDown("ZoomIn"))
        {
            StartTime = Time.time;
            journeyLength = Math.Abs(MainCamera.transform.position.z - CloseValueCamera);
            StartPosition = MainCamera.transform.position;
            EndPosition = ClosePosition;
            m_IsZoomOut = false;
            RegionHandler.Instance.RegionOver(m_LastRegion, true);
        }

        if (Input.GetButtonDown("ZoomOut"))
        {
            StartTime = Time.time;
            journeyLength = Math.Abs(MainCamera.transform.position.z - FarValueCamera);
            StartPosition = MainCamera.transform.position;
            EndPosition = FarPosition;
            m_IsZoomOut = true;
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
        transform.Rotate(Vector3.right, translation * Time.deltaTime, Space.World);

        ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
        if (Physics.Raycast(ray, out raycastHit))
        {
            GlobalRegion globalRegion = raycastHit.collider.GetComponent<GlobalRegion>();
            if (m_IsZoomOut && globalRegion != null && globalRegion != m_LastRegion)
            {
                if (m_LastRegion != null)
                {
                    RegionHandler.Instance.RegionOver(m_LastRegion, true);
                }
                m_LastRegion = globalRegion;
                RegionHandler.Instance.RegionOver(m_LastRegion, false);
                m_CanSpawnTornado = m_LastRegion.Tornado;
                m_CanSpawnVolcano = m_LastRegion.Volcano;
                m_CanSpawnEarthQuake = m_LastRegion.EarthQuake;
            }

            if (Input.GetButtonDown("Submit") && m_CanSpawnTornado)
            {
                SpawnTornado(raycastHit.point, raycastHit.normal);
            }
        }
    }

    private void SpawnTornado(Vector3 i_HitPosition, Vector3 i_HitNormal)
    {
        if (m_CurrentTornado != null)
        {
            Destroy(m_CurrentTornado);
        }

        m_CurrentTornado = Instantiate(m_Tornado, i_HitPosition, Quaternion.FromToRotation(Vector3.up, i_HitNormal), this.transform);
        m_CurrentTornado.transform.localScale = m_TornadoScale;
    }
}