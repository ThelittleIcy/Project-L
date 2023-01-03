using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AStats : MonoBehaviour
{
    public bool IsAlerted { get => m_isAlerted; set => m_isAlerted = value; }
    [SerializeField]
    private bool m_isAlerted = false;
    public bool IsAttacking { get => m_isAttacking; set => m_isAttacking = value; }
    [SerializeField]
    private bool m_isAttacking = false;
    public bool IsDirty { get => m_isDirty; set => m_isDirty = value; }
    [SerializeField]
    private bool m_isDirty = false;
}
