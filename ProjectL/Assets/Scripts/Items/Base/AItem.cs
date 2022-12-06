using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public Coroutine PickUpCoroutine { get => m_pickUpCoroutine; set => m_pickUpCoroutine = value; }
    public Coroutine DropCoroutine { get => m_dropCoroutine; set => m_dropCoroutine = value; }

    public virtual void PickUp()
    {
        PopUpManager.Instance.GeneratePopUp("Picked up " + name);
        m_pickedUp = true;
        // Add To Inventory
        AddToInventory();
    }
    public virtual void Drop()
    {
        PopUpManager.Instance.GeneratePopUp("Dropped " + name);
        m_pickedUp = false;
        // Remove From Inventory
        RemoveFromInventory();
    }
    public virtual void Use()
    {

    }
    public virtual void AddToInventory()
    {

    }

    public virtual void RemoveFromInventory()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUpCoroutine = StartCoroutine(CanPickUp());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(PickUpCoroutine);
    }

    public IEnumerator CanPickUp()
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

    public IEnumerator CanDrop()
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
