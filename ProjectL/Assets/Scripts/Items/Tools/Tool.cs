using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : AItem
{
    public Vector2 UseDirection { get => m_useDirection; set => m_useDirection = value; }
    public Transform OriginPlayer { get => m_originPlayer; set => m_originPlayer = value; }
    public ScriptableTool Data { get => m_data; set => m_data = value; }

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
    [SerializeField]
    private ScriptableTool m_data;

    public override void Interact()
    {
        base.Interact();

        this.transform.parent = PlayerTools.Instance.transform;
        this.transform.position = PlayerTools.Instance.transform.position;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    public override void Drop()
    {
        base.Drop();
        this.transform.parent = null;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
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
            if (hit.collider.gameObject.GetComponent<FarmObject>().NeededTool == m_type)
            {
                hit.collider.gameObject.GetComponent<FarmObject>().Farm();
            }
            //if (hit.collider.gameObject.GetComponent<Enemy>())
            //{

            //}
        }
        Debug.Log("Used the Tool");
    }
    public void Select()
    {
        DropCoroutine = StartCoroutine(CanDrop());
    }
    public void DeSelect()
    {
        if (DropCoroutine != null)
        {
            StopCoroutine(DropCoroutine);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + m_range,transform.position.y,transform.position.z));
    }
}
