using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AItem : AInteractable, IPickable, IDropable
{
    [SerializeField]
    private KeyCode m_dropKey = KeyCode.F;
    [SerializeField]
    private bool m_pickedUp = false;

    public KeyCode DropKey { get => m_dropKey; set => m_dropKey = value; }

    private Coroutine m_pickUpCoroutine = null;
    private Coroutine m_dropCoroutine = null;

    public Coroutine PickUpCoroutine { get => m_pickUpCoroutine; set => m_pickUpCoroutine = value; }
    public Coroutine DropCoroutine { get => m_dropCoroutine; set => m_dropCoroutine = value; }

    public override void Interact()
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

    public override IEnumerator CanInteract()
    {
        while (!m_pickedUp)
        {
            if (Input.GetKeyDown(InteractKey))
            {
                Interact();
            }
            yield return null;
        }
    }

    public IEnumerator CanDrop()
    {
        while (m_pickedUp)
        {
            if (Input.GetKeyDown(DropKey))
            {
                Drop();
            }
            yield return null;
        }
    }

}
