using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInteractable : MonoBehaviour
{
    [SerializeField]
    private KeyCode m_interactKey = KeyCode.F;

    private Coroutine m_startInteractCoroutine = null;

    public Coroutine StartInteractCoroutine { get => m_startInteractCoroutine; set => m_startInteractCoroutine = value; }

    public KeyCode InteractKey { get => m_interactKey; set => m_interactKey = value; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartInteractCoroutine = StartCoroutine(CanInteract());
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        StopCoroutine(StartInteractCoroutine);
    }
    public abstract IEnumerator CanInteract();
    public abstract void Interact();
}
