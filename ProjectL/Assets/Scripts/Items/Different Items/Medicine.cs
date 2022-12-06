using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    [SerializeField]
    private bool m_overTime;
    [SerializeField]
    private int m_amount;
    [SerializeField]
    private InventorySlot m_slot;
    public void Use()
    {
        if (m_slot.Count >= 1)
        {
            if (m_overTime)
            {
                StartCoroutine(PlayerHealth.Instance.IncreaseHealthOverTime(m_amount));
            }
            else
            {
                PlayerHealth.Instance.IncreaseHealth(m_amount);
            }
            PlayerInventory.Instance.Remove(m_slot.Ressource, 1);
            Debug.Log("Healed");
        }
    }
}
