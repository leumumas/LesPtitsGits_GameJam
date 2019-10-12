using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelRegion : MonoBehaviour
{
    [SerializeField]
    private Animator animatorPanel;
    [SerializeField]
    private TextMeshProUGUI m_NameRegion;
    [SerializeField]
    private TextMeshProUGUI m_Population;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void UpdateRegion(GlobalRegion i_GlobalRegion, bool i_IsOut)
    {
        if (i_GlobalRegion.Ocean)
        {
            return;
        }
        m_NameRegion.text = i_GlobalRegion.NameRegion;
        //m_Population.text = i_GlobalRegion.Population.ToString("# #");
        m_Population.text = string.Format("{0:#,0}", i_GlobalRegion.Population);

        animatorPanel.SetBool("Out", i_IsOut);
    }
}
