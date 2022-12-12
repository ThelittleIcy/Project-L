using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterObject : AInteractable
{
    [SerializeField]
    private bool m_fullThirst = false;
    [SerializeField]
    private int m_increaseThirstAmount = 0;

    private void Start()
    {
        if (m_fullThirst)
        {
            m_increaseThirstAmount = PlayerHealth.Instance.MaxThirst;
        }
    }
    public override IEnumerator CanInteract()
    {
        while (true)
        {
            if (Input.GetKeyDown(InteractKey))
            {
                Interact();
            }
            yield return null;
        }
    }

    public override void Interact()
    {
        PlayerHealth.Instance.IncreaseThirst(m_increaseThirstAmount);
        Debug.Log("Filled Thirst");
    }
}
