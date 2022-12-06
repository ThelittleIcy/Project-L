using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTools : MonoBehaviour
{
    public Tool Weapon { get => m_weapon; set => m_weapon = value; }
    public Tool Pickaxe { get => m_pickaxe; set => m_pickaxe = value; }
    public Tool Axe { get => m_axe; set => m_axe = value; }
    public static PlayerTools Instance { get => m_instance; set => m_instance = value; }

    [SerializeField]
    private Tool m_weapon;
    [SerializeField]
    private Tool m_axe;
    [SerializeField]
    private Tool m_pickaxe;
    [SerializeField]
    private PlayerMovement m_player;
    [SerializeField]
    private KeyCode m_useKeyCode = KeyCode.Mouse0;

    [SerializeField]
    private Image m_weaponUI;
    [SerializeField]
    private Image m_axeUI;
    [SerializeField]
    private Image m_pickaxeUI;
    [SerializeField]
    private Color m_highlighColorSelected;

    private Tool m_currentlySelected;

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
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (m_currentlySelected != null)
            {
                m_currentlySelected.DeSelect();
                m_currentlySelected = null;
                m_weaponUI.color = Color.white;
                m_axeUI.color = Color.white;
                m_pickaxeUI.color = Color.white;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (m_weapon != null)
            {
                if (m_pickaxe != null)
                {
                    m_pickaxe.DeSelect();
                }
                if (m_axe != null)
                {
                    m_axe.DeSelect();
                }
                m_weapon.Select();
                m_currentlySelected = m_weapon;
                m_weaponUI.color = m_highlighColorSelected;

                m_axeUI.color = Color.white;
                m_pickaxeUI.color = Color.white;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (m_axe != null)
            {
                if (m_pickaxe != null)
                {
                    m_pickaxe.DeSelect();
                }
                if (m_weapon != null)
                {
                    m_weapon.DeSelect();
                }

                m_axe.Select();
                m_currentlySelected = m_axe;
                m_axeUI.color = m_highlighColorSelected;

                m_weaponUI.color = Color.white;
                m_pickaxeUI.color = Color.white;
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (m_pickaxe != null)
            {
                if (m_weapon != null)
                {
                    m_weapon.DeSelect();
                }
                if (m_axe != null)
                {
                    m_axe.DeSelect();
                }
                m_pickaxe.Select();
                m_currentlySelected = m_pickaxe;
                m_pickaxeUI.color = m_highlighColorSelected;

                m_weaponUI.color = Color.white;
                m_axeUI.color = Color.white;
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
            else if (m_currentlySelected == m_weapon)
            {
                m_weapon.UseDirection = m_player.FaceDirection;
            }
            m_currentlySelected.Use();
        }
    }
    public void Add(Tool _new, Tools _type)
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
                m_axeUI.sprite = _new.Data.Icon;
                m_axeUI.enabled = true;
                break;
            case Tools.PICKAXE:
                if (m_pickaxe != null)
                {
                    m_pickaxe.Drop();
                }
                m_pickaxe = _new;
                m_pickaxeUI.sprite = _new.Data.Icon;
                m_pickaxeUI.enabled = true;
                break;
            case Tools.WEAPON:
                if (m_weapon != null)
                {
                    m_weapon.Drop();
                }
                m_weapon = _new;
                m_weaponUI.sprite = _new.Data.Icon;
                m_weaponUI.enabled = true;
                break;
            default:
                break;
        }
    }

    public void Remove(Tool _toRemove, Tools _type)
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
                        m_axeUI.color = Color.white;
                        m_axeUI.enabled = false;
                    }
                    break;
                case Tools.PICKAXE:
                    if (m_pickaxe == _toRemove)
                    {
                        m_pickaxe = null;
                        m_currentlySelected = null;
                        m_pickaxeUI.color = Color.white;
                        m_pickaxeUI.enabled = false;
                    }
                    break;
                case Tools.WEAPON:
                    if (m_weapon == _toRemove)
                    {
                        m_weapon = null;
                        m_currentlySelected = null;
                        m_weaponUI.color = Color.white;
                        m_weaponUI.enabled = false;
                    }
                    break;
                default:
                    break;
            }
        
    }
}
