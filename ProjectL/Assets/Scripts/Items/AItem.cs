using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public abstract class AItem : MonoBehaviour, IPickable, IDropable
{
    [SerializeField]
    private KeyCode m_pickUpKey = KeyCode.F;
    [SerializeField]
    private KeyCode m_dropKey = KeyCode.F;
    [SerializeField]
    private bool m_pickedUp = false;

    private Coroutine m_pickUpCoroutine = null;
    private Coroutine m_dropCoroutine = null;
    public void PickUp()
    {
        Debug.Log("Picked Up Item " + name);
        m_pickedUp = true;
        // Add To Inventory
        AddToInventory();
        this.transform.parent = PlayerTools.Instance.transform;
        this.transform.position = PlayerTools.Instance.transform.position;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<CircleCollider2D>().enabled = false;
    }
    public void Drop()
    {
        Debug.Log("Droped Item " + name);
        m_pickedUp = false;
        // Remove From Inventory
        RemoveFromInventory();
        this.transform.parent = null;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;
        gameObject.GetComponent<CircleCollider2D>().enabled = true;
    }
    public virtual void Use()
    {

    }
    public virtual void Select()
    {
        m_dropCoroutine = StartCoroutine(CanDrop());
    }
    public virtual void AddToInventory()
    {

    }

    public virtual void RemoveFromInventory()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_pickUpCoroutine = StartCoroutine(CanPickUp());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(m_pickUpCoroutine);
    }

    private IEnumerator CanPickUp()
    {
        while (!m_pickedUp)
        {
            if (Input.GetKeyDown(m_pickUpKey))
            {
                PickUp();
            }
            yield return null;
        }
    }

    private IEnumerator CanDrop()
    {
        while (m_pickedUp)
        {
            if (Input.GetKeyDown(m_dropKey))
            {
                Drop();
            }
            yield return null;
        }
    }

}
