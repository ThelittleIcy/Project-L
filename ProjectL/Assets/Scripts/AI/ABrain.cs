using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ABrain : MonoBehaviour
{
    /// <summary>
    /// Gathers Intel from all Senses.
    /// Makes a Decision based on this Intel.
    /// </summary>
    public List<ASense> Senses { get => m_senses; set => m_senses = value; }
    [SerializeField]
    private List<ASense> m_senses;

    public abstract void SetUpAllSenses();
    public abstract void GetSenseResults();
    public abstract void MakeDecision();
    public void OnValidate()
    {
        m_senses.Clear();
        m_senses.AddRange(GetComponents<ASense>());
    }
}
