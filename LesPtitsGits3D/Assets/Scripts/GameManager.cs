using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private List<RegionData> m_GlobalRegions = new List<RegionData>();
    [SerializeField]
    private ScoreController m_ScoreController;
    [SerializeField]
    private int m_PlaneScore;

    private List<GlobalRegionInfo> m_GlobalRegionInfos = new List<GlobalRegionInfo>();

    public int PlaneScore => m_PlaneScore;

    // Start is called before the first frame update
    void Start()
    {
        m_GlobalRegionInfos.Clear();
        foreach (RegionData globalRegion in m_GlobalRegions)
        {
            GlobalRegionInfo globalRegionInfo = new GlobalRegionInfo();
            globalRegionInfo.NameRegion = globalRegion.m_NameRegion;
            globalRegionInfo.Population = globalRegion.m_Population;
            m_GlobalRegionInfos.Add(globalRegionInfo);
        }
    }

    public GlobalRegionInfo GetGlobalRegionInfo(string i_RegionName)
    {
        GlobalRegionInfo globalRegionInfo = null;
        foreach(GlobalRegionInfo globalRegion in m_GlobalRegionInfos)
        {
            if (i_RegionName == globalRegion.NameRegion)
            {
                globalRegionInfo = globalRegion;
            }
        }

        return globalRegionInfo;
    }

    public void RemoveGlobalRegionPopulation(string i_RegionName, int i_PopulationRemoved)
    {
        GlobalRegionInfo globalRegionInfo = GetGlobalRegionInfo(i_RegionName);
        globalRegionInfo.Population -= i_PopulationRemoved;
        if (globalRegionInfo.Population < 0)
        {
            i_PopulationRemoved += (int)globalRegionInfo.Population;
            globalRegionInfo.Population = 0;
        }
        m_ScoreController.AddCasualtiesToScore(i_PopulationRemoved);
    }
}
