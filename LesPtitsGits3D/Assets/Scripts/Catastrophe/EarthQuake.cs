using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthQuake : MonoBehaviour
{
    [SerializeField]
    private float duration;
    [SerializeField]
    private int tickCausalitiesMin;
    [SerializeField]
    private int tickCausalitiesMax;

    private float startTime = -1;
    private GlobalRegion m_GlobalRegion;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (startTime < 0)
        {
            return;
        }
        if ((Time.time - startTime) > duration)
        {
            Destroy(gameObject);
        }
        else
        {
            GameManager.Instance.RemoveGlobalRegionPopulation(m_GlobalRegion.NameRegion, Random.Range(tickCausalitiesMin, tickCausalitiesMax));
        }
    }

    public void SetEarthQuake(GlobalRegion i_GlobalRegion)
    {
        m_GlobalRegion = i_GlobalRegion;
        startTime = Time.time;
    }
}
