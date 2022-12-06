using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlotsManager : MonoBehaviour
{
    public List<InventorySlot> Slots { get => m_slots; set => m_slots = value; }
    [SerializeField]
    private List<InventorySlot> m_slots;

    private void Start()
    {
        m_slots.AddRange(GetComponentsInChildren<InventorySlot>());
    }

    public void SetCount(string _res, int _count)
    {
        for (int i = 0; i < m_slots.Count; i++)
        {
            if(m_slots[i].Ressource.Name == _res)
            {
                m_slots[i].Count = _count;
                return;
            }
        }
    }
}
