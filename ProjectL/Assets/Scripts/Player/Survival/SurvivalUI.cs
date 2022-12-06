using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SurvivalUI : MonoBehaviour
{
    [SerializeField]
    private int m_value;

    [SerializeField]
    private TextMeshProUGUI m_text;

    public void SetValue(int _new)
    {
        m_text.text = _new.ToString();
    }
}
