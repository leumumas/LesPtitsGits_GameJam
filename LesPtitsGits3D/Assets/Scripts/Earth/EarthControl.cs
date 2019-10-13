using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class EarthControl : MonoBehaviour
{
    [SerializeField]
    private GameObject m_Tornado;

    private Camera MainCamera;
    
    private RaycastHit raycastHit;
    private Ray ray;

    private GlobalRegion m_LastRegion;

    private bool m_IsZoomOut = true;
    private bool Change = false;

    private bool m_CanSpawnTornado = true;
    private bool m_CanSpawnEarthQuake = true;
    private bool m_CanSpawnVolcano = true;

    private GameObject m_CurrentTornado;
    private Vector3 m_TornadoScale= new Vector3(4f, 4f, 4f);
    private Vector3 m_TornadoRotation = new Vector3(0f, 0f, 0f);
    
    private void Start()
    {
        MainCamera = Camera.main;
    }

    void Update() 
    {
        ray = new Ray(MainCamera.transform.position, MainCamera.transform.forward);
        if (Physics.Raycast(ray, out raycastHit))
        {
            GlobalRegion globalRegion = raycastHit.collider.GetComponent<GlobalRegion>();
            if (globalRegion != null && (globalRegion != m_LastRegion || Change))
            {
                Change = false;
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
                m_CanSpawnTornado = m_LastRegion.Tornado;
                m_CanSpawnVolcano = m_LastRegion.Volcano;
                m_CanSpawnEarthQuake = m_LastRegion.EarthQuake;
        }
    }

    public void SetIsZoomOut(bool newValue)
    {
        Change = true;
        m_IsZoomOut = newValue;
    }
}
