using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDownTool : AItem
{
    public Vector2 UseDirection { get => m_useDirection; set => m_useDirection = value; }
    public Transform OriginPlayer { get => m_originPlayer; set => m_originPlayer = value; }

    [SerializeField]
    private Vector2 m_useDirection;
    [SerializeField]
    private float m_range = 2f;
    [SerializeField]
    private Transform m_originPlayer;
    [SerializeField]
    private LayerMask m_useLayerMask;
    [SerializeField]
    private Tools m_type;

    public override void AddToInventory()
    {
        base.AddToInventory();
        PlayerTools.Instance.Add(this, m_type);
    }
    public override void RemoveFromInventory()
    {
        base.RemoveFromInventory();
        PlayerTools.Instance.Remove(this, m_type);
    }

    public override void Use()
    {
        base.Use();
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(OriginPlayer.position.x, OriginPlayer.position.y), UseDirection, m_range, m_useLayerMask);
        if(hit == false)
        {
            return;
        }
        if (hit.collider.gameObject.GetComponent(typeof(IFarmable)) != null)
        {
            if (hit.collider.gameObject.GetComponent<FarmRessource>().NeededTool == m_type)
            {
                hit.collider.gameObject.GetComponent<FarmRessource>().Farm();
            }
        }
        Debug.Log("Used the Tool");
    }
    [ContextMenu("Test")]
    public override void Select()
    {
        base.Select();

    }
}
