using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : AItem
{
    [SerializeField]
    private ScriptableRessource m_data;
    public override void PickUp()
    {
        base.PickUp();
        Destroy(this.gameObject);
    }
    public override void AddToInventory()
    {
        base.AddToInventory();
        PlayerInventory.Instance.Add(m_data, 1);
    }

    public override void RemoveFromInventory()
    {
        base.RemoveFromInventory();
        PlayerInventory.Instance.Remove(m_data, 1);
    }
}
