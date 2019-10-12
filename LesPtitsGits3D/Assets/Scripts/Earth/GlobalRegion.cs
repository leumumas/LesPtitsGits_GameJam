﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRegion : MonoBehaviour
{
    [SerializeField]
    private RegionData m_CurrentRegion;
    [SerializeField]
    private bool m_Ocean = false;

    public long Population => m_CurrentRegion.m_Population;
    public string NameRegion => m_CurrentRegion.m_NameRegion;
    public bool Ocean => m_Ocean;


}
