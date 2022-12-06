using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmObject : MonoBehaviour, IFarmable
{
    public Tools NeededTool { get => m_neededTool; set => m_neededTool = value; }
    [SerializeField]
    private List<int> m_amountFarmable;
    [SerializeField]
    private Tools m_neededTool = Tools.NONE;
    [SerializeField]
    private int m_maxUses = 1;
    [SerializeField]
    private int m_currentUses;
    [SerializeField]
    private float m_waitSecondsForFilling = 60;

    // Data which Item
    [SerializeField]
    private List<ScriptableRessource> m_lootRessource;

    private void Start()
    {
        m_currentUses = m_maxUses;
        StartCoroutine(Fill());
    }
    public void Farm()
    {
        if (m_currentUses > 0)
        {
            // Add To Inventory Amount
            for (int i = 0; i < m_lootRessource.Count; i++)
            {
                PlayerInventory.Instance.Add(m_lootRessource[i], m_amountFarmable[i]);
            }
            m_currentUses -= 1;
        }
    }

    public IEnumerator Fill()
    {
        while (true)
        {
            if(m_currentUses < m_maxUses)
            {
                m_currentUses += 1;
                yield return new WaitForSeconds(m_waitSecondsForFilling);
            }
            else
            {
                yield return null;
            }
        }
    }
}
