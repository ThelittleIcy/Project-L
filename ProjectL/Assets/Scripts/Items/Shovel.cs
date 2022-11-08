using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel : AItem
{
    public Vector2 UseDirection { get => m_useDirection; set => m_useDirection = value; }
    public Transform OriginPlayer { get => m_originPlayer; set => m_originPlayer = value; }

    [SerializeField]
    private Vector2 m_useDirection;
    [SerializeField]
    private float m_range = 2f;
    [SerializeField]
    private Transform m_originPlayer;
    public override void AddToInventory()
    {
        base.AddToInventory();
        PlayerTools.Instance.Add(this);
    }
    public override void RemoveFromInventory()
    {
        base.RemoveFromInventory();
        PlayerTools.Instance.Remove(this);
    }

    public override void Use()
    {
        base.Use();
        //RaycastHit2D hit = Physics2D.Raycast(new Vector2(OriginPlayer.position.x, OriginPlayer.position.y), UseDirection, m_range);
        //if (hit.collider.gameObject.GetComponent(typeof(IDegradable)))
        //{

        //}
        Debug.Log("Used the Shovel");
    }
    public override void Select()
    {
        base.Select();

    }

}
