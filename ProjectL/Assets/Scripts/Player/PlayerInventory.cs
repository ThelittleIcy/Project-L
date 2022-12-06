using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get => m_instance; set => m_instance = value; }
    private static PlayerInventory m_instance;

    [SerializeField]
    private List<ScriptableRessource> m_inventory;

    [SerializeField]
    private InventorySlotsManager m_slotManager;
    private void Awake()
    {
        if (m_instance != null && m_instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_instance = this;
        }
    }

    public void Add(ScriptableRessource _new, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            m_inventory.Add(_new);
            Convert();
        }
    }

    public void Remove(ScriptableRessource _toRemove, int _amount)
    {
        for (int i = 0; i < _amount; i++)
        {
            if (m_inventory.Contains(_toRemove))
            {
                m_inventory.Remove(_toRemove);
                Convert();
            }
            else
            {
                Debug.LogError("You tried to Remove something from Inventory, which is not in there");
            }
        }
    }

    public void Convert()
    {
        string[,] items = new string[GameManager.Instance.AllRessources.Count, 2];

        for (int i = 0; i < GameManager.Instance.AllRessources.Count; i++)
        {
            items[i, 0] = GameManager.Instance.AllRessources[i].Name;
        }

        foreach (ScriptableRessource item in m_inventory)
        {
            for (int i = 0; i < (items.Length/2) ; i++)
            {
                if(items[i, 0] == item.Name)
                {
                    int count;
                    int.TryParse(items[i, 1], out count);
                    count++;
                    items[i, 1] = count.ToString();
                }
            }
        }

        for (int i = 0; i < (items.Length/2); i++)
        {
            int count;
            int.TryParse(items[i, 1], out count);
            m_slotManager.SetCount(items[i, 0], count);
        }
    }
}
