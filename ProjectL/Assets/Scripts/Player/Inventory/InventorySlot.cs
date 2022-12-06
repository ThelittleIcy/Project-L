using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlot : MonoBehaviour
{
    [SerializeField]
    private ScriptableRessource m_ressource;
    [SerializeField]
    private int m_count;
    [SerializeField]
    private Color m_transparentColor;

    [SerializeField]
    private TextMeshProUGUI m_countText;
    [SerializeField]
    private TextMeshProUGUI m_nameText;
    [SerializeField]
    private Image m_iconImage;

    private void Awake()
    {
        m_nameText.text = m_ressource.Name;
        m_iconImage.sprite = m_ressource.Icon;
        if(Count == 0)
        {
            SetTransparent();
        }
    }

    private void SetTransparent()
    {
        m_iconImage.color = m_transparentColor;
    }
    private void SetVisible()
    {
        m_iconImage.color = Color.white;
    }

    public int Count
    {
        get => m_count;
        set
        {
            if(m_count < 0)
            {
                Debug.LogError("You have negative Inventory.... This does not work this way");
                return;
            }
            m_count = value;
            CountText.text = m_count.ToString();
            if(m_count == 0)
            {
                SetTransparent();
                return;
            }
            SetVisible();
        }
    }
    public ScriptableRessource Ressource { get => m_ressource; set => m_ressource = value; }
    public TextMeshProUGUI CountText { get => m_countText; set => m_countText = value; }
}
