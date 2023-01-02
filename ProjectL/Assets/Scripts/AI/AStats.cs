using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStats : MonoBehaviour
{
    public bool IsAlerted { get => m_isAlerted; set => m_isAlerted = value; }
    [SerializeField]
    private bool m_isAlerted = false;

}
