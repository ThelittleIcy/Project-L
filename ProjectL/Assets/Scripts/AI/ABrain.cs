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

    public List<GameObject> PossibleTargets { get => m_possibleTargets; set => m_possibleTargets = value; }
    [SerializeField]
    private List<GameObject> m_possibleTargets;

    public abstract void SetUpAllSenses();
    public abstract void GetSenseResults();
    public abstract void GetPossibleTargets();
    public abstract void MakeDecision();
    public abstract void ExecuteDecision();
    public void OnValidate()
    {
        m_senses.Clear();
        m_senses.AddRange(GetComponents<ASense>());
    }
}
