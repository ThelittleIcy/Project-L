using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<ScriptableRessource> AllRessources { get => m_allRessources; set => m_allRessources = value; }
    public static GameManager Instance { get => m_instance; set => m_instance = value; }

    [SerializeField]
    private List<ScriptableRessource> m_allRessources;

    private static GameManager m_instance;

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
}
