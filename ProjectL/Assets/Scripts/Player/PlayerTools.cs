using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTools : MonoBehaviour
{
    public Shovel Shovel { get => m_shovel; set => m_shovel = value; }
    public Pickaxe Pickaxe { get => m_pickaxe; set => m_pickaxe = value; }
    public Axe Axe { get => m_axe; set => m_axe = value; }
    public static PlayerTools Instance { get => m_instance; set => m_instance = value; }

    [SerializeField]
    private Axe m_axe;
    [SerializeField]
    private Pickaxe m_pickaxe;
    [SerializeField]
    private Shovel m_shovel;
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
    public void Add(Axe _new)
    {
        if (m_axe != null)
        {
            m_axe.Drop();
        }
        m_axe = _new;
    }
    public void Add(Pickaxe _new)
    {
        if (m_pickaxe != null)
        {
            m_pickaxe.Drop();
        }
        m_pickaxe = _new;
    }
    public void Add(Shovel _new)
    {
        if (m_shovel != null)
        {
            m_shovel.Drop();
        }
        m_shovel = _new;
    }
    public void Remove(Axe _toRemove)
    {
        if (m_axe == _toRemove)
        {
            m_axe = null;
            m_currentlySelected = null;
        }
    }
    public void Remove(Pickaxe _toRemove)
    {
        if (m_pickaxe == _toRemove)
        {
            m_pickaxe = null;
            m_currentlySelected = null;
        }
    }
    public void Remove(Shovel _toRemove)
    {
        if (m_shovel == _toRemove)
        {
            m_shovel = null;
            m_currentlySelected = null;
        }
    }
}
