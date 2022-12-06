using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class RightClick_Button : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent MiddleClick;
    public UnityEvent RightClick;

    [SerializeField]
    private Button m_button;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Middle)
        {
            MiddleClick?.Invoke();
            //m_button.image.color = m_button.colors.pressedColor;
            //StartCoroutine(Wait());
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            RightClick?.Invoke();
            //m_button.image.color = m_button.colors.pressedColor;
            //StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        float count = 2.5f;
        while(count <= 0)
        {
            count--;
            yield return new WaitForSecondsRealtime(1);
        }
        m_button.image.color = m_button.colors.normalColor;
    }

    private void OnValidate()
    {
        m_button = GetComponent<Button>();
    }
}
