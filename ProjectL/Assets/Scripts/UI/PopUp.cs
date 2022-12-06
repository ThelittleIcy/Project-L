using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class PopUp : MonoBehaviour
{
    public string Message { get => m_message; set => m_message = value; }
    public PopUpManager Manager { get => m_manager; set => m_manager = value; }

    [SerializeField]
    private string m_message;
    [SerializeField]
    private int m_visibleSeconds = 5;
    [SerializeField]
    private TextMeshProUGUI m_text;

    [SerializeField]
    private PopUpManager m_manager;

    public void SetUpMessage(string _message)
    {
        Message = _message;
    }

    public void StartPopUp()
    {
        Visible();

        StartCoroutine(Wait());
    }
    private IEnumerator Wait()
    {
        int time = m_visibleSeconds;
        while (time >= 0)
        {
            time -= 1;
            yield return new WaitForSecondsRealtime(1);
        }
        m_manager.DeletePopUp(this);
    }

    private void Visible()
    {
        m_text.text = Message;
    }
    private void Invisible()
    {
        m_message = "";
        m_text.text = "";
    }
}
