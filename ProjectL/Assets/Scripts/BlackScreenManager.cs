using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BlackScreenManager : MonoBehaviour
{
    public static BlackScreenManager Instance { get => m_instance; set => m_instance = value; }
    private static BlackScreenManager m_instance;

    public UnityEvent OnBlackScreenFinished;

    private Coroutine m_fadeToBlackCoroutine;
    private Coroutine m_fadeAwayCoroutine;
    private Coroutine m_waitCoroutine;

    [SerializeField]
    private Image m_image;
    [SerializeField]
    private float m_waitSeconds = 5;

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
    [ContextMenu("Start")]
    public void StartFadeToBlack()
    {
        m_fadeToBlackCoroutine= StartCoroutine(ToBlack());
    }
    public IEnumerator ToBlack()
    {
        while(m_image.color.a <= 1)
        {
            Color tmp = m_image.color;
            tmp.a += 0.01f;
            m_image.color = tmp;
            yield return new WaitForFixedUpdate();
        }
        m_waitCoroutine = StartCoroutine(Wait(m_waitSeconds));
    }

    public IEnumerator Wait(float _secondsToWait)
    {
        float seconds = _secondsToWait;
        while(seconds > 0)
        {
            seconds -= 1f;
            yield return new WaitForSecondsRealtime(1f);
        }
        m_fadeAwayCoroutine = StartCoroutine(FromBlack());
    }

    public IEnumerator FromBlack()
    {
        while (m_image.color.a >= 0)
        {
            Color tmp = m_image.color;
            tmp.a -= 0.01f;
            m_image.color = tmp;
            yield return new WaitForFixedUpdate();
        }
        OnBlackScreenFinished?.Invoke();
    }
}
