using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalRegion : MonoBehaviour
{
    [SerializeField]
    private string m_NameRegion;
    [SerializeField]
    private long m_Population;
    [SerializeField]
    private bool m_Ocean = false;

    public long Population => m_Population;
    public string NameRegion => m_NameRegion;
    public bool Ocean => m_Ocean;

    // Start is called before the first frame update
    void Start()
    {
        
    }
}
