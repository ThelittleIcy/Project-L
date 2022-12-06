using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Ressource", menuName ="Data/Items/Ressource")]
public class ScriptableRessource : ScriptableObject
{
    public string Name { get => m_name; set => m_name = value; }
    public Sprite Icon { get => m_icon; set => m_icon = value; }
    [SerializeField]
    private string m_name;
    [SerializeField]
    private Sprite m_icon;

}
