using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public bool IsBlind { get => m_isBlind; set => m_isBlind = value; }
    [SerializeField]
    private bool m_isBlind;

}
