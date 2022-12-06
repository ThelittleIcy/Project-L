using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Tool", menuName ="Data/Items/Tool")]
public class ScriptableTool : ScriptableRessource
{
    public int Uses { get => m_uses; set => m_uses = value; }
    [SerializeField]
    private int m_uses = 1;

}
