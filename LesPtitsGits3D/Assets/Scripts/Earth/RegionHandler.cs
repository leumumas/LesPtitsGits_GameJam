using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegionHandler : Singleton<RegionHandler>
{
    [SerializeField]
    private PanelRegion panelRegion;
    [SerializeField]
    private float m_RadiusEarth = 1f;

    public float RadiusEarth => m_RadiusEarth;

    public void RegionOver(GlobalRegion i_GlobalRegion, bool i_IsOut)
    {
        panelRegion.UpdateRegion(i_GlobalRegion, i_IsOut);
    }
}
