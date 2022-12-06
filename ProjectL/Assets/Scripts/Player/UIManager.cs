using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_inventory;

    [SerializeField]
    private KeyCode m_openInventoryKey = KeyCode.I;

    private void Update()
    {
        if (Input.GetKey(m_openInventoryKey))
        {
            PlayerInventory.Instance.Convert();
            m_inventory.SetActive(true);
            PlayerTools.Instance.enabled = false;
            PlayerMovement.Instance.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerTools.Instance.enabled = true;
            PlayerMovement.Instance.enabled = true;
            m_inventory.SetActive(false);
        }
    }
}
