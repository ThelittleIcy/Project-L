using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    public BreakDownTool Shovel { get => m_shovel; set => m_shovel = value; }
    public BreakDownTool Pickaxe { get => m_pickaxe; set => m_pickaxe = value; }
    public BreakDownTool Axe { get => m_axe; set => m_axe = value; }
    public static PlayerTools Instance { get => m_instance; set => m_instance = value; }

    [SerializeField]
    private BreakDownTool m_axe;
    [SerializeField]
    private BreakDownTool m_pickaxe;
    [SerializeField]
    private BreakDownTool m_shovel;
    [SerializeField]
    private PlayerMovement m_player;
    [SerializeField]
    private KeyCode m_useKeyCode = KeyCode.Mouse0;

    private AItem m_currentlySelected;

    private static PlayerTools m_instance;

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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_axe != null)
            {
                m_axe.Select();
                m_currentlySelected = m_axe;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_pickaxe != null)
            {
                m_pickaxe.Select();
                m_currentlySelected = m_pickaxe;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (m_shovel != null)
            {
                m_shovel.Select();
                m_currentlySelected = m_shovel;
            }
        }

        if (m_currentlySelected != null && Input.GetKeyDown(m_useKeyCode))
        {
            if (m_currentlySelected == m_axe)
            {
                m_axe.UseDirection = m_player.FaceDirection;
            }
            else if (m_currentlySelected == m_pickaxe)
            {
                m_pickaxe.UseDirection = m_player.FaceDirection;
            }
            else if (m_currentlySelected == m_shovel)
            {
                m_shovel.UseDirection = m_player.FaceDirection;
            }
            m_currentlySelected.Use();
        }
    }
    public void Add(BreakDownTool _new, Tools _type)
    {
        switch (_type)
        {
            case Tools.NONE:
                break;
            case Tools.AXE:
                if (m_axe != null)
                {
                    m_axe.Drop();
                }
                m_axe = _new;
                break;
            case Tools.PICKAXE:
                if (m_pickaxe != null)
                {
                    m_pickaxe.Drop();
                }
                m_pickaxe = _new;
                break;
            case Tools.SHOVEL:
                if (m_shovel != null)
                {
                    m_shovel.Drop();
                }
                m_shovel = _new;
                break;
            default:
                break;
        }
    }

    public void Remove(BreakDownTool _toRemove, Tools _type)
    {
        switch (_type)
        {
            case Tools.NONE:
                break;
            case Tools.AXE:
                if (m_axe == _toRemove)
                {
                    m_axe = null;
                    m_currentlySelected = null;
                }
                break;
            case Tools.PICKAXE:
                if (m_pickaxe == _toRemove)
                {
                    m_pickaxe = null;
                    m_currentlySelected = null;
                }
                break;
            case Tools.SHOVEL:
                if (m_shovel == _toRemove)
                {
                    m_shovel = null;
                    m_currentlySelected = null;
                }
                break;
            default:
                break;
        }
    }
}
