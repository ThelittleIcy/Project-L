using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private int m_amount;
    [SerializeField]
    private InventorySlot m_slot;
    public void Use()
    {
        if (m_slot.Count >= 1)
        {
            PlayerHealth.Instance.IncreaseHunger(m_amount);
            PlayerInventory.Instance.Remove(m_slot.Ressource, 1);
            Debug.Log("Eaten");
        }
    }
}
