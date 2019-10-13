using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Region", menuName = "Region")]
public class RegionData : ScriptableObject
{
    public string m_NameRegion;
    public long m_Population;

    public bool m_Tornado;
    public bool m_Volcano;
    public bool m_EarthQuake;
}
