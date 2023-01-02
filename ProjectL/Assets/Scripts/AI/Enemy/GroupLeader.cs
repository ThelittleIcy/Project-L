using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GroupLeader : MonoBehaviour
{
    public List<AStats> GroupMembers { get => m_groupMembers; set => m_groupMembers = value; }
    [SerializeField]
    private List<AStats> m_groupMembers;
}
