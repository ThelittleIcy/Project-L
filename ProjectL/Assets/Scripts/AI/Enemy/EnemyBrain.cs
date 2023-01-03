using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBrain : ABrain
{
    [SerializeField]
    private EnemyStats m_myStats;

    public Dictionary<string, bool> Informations { get => m_informations; set => m_informations = value; }
    private Dictionary<string, bool> m_informations;

    private void Awake()
    {
        m_informations = new Dictionary<string, bool>();
        SetUpAllSenses();
    }
    public override void SetUpAllSenses()
    {
        foreach (ASense sense in Senses)
        {
            sense.SetUp();     
        }
    }
    public override void GetPossibleTargets()
    {
        foreach (ASense sense in Senses)
        {
            if(sense.ReturnTarget() == null || PossibleTargets.Contains(sense.ReturnTarget()))
            {
                return;
            }
            else
            {
                PossibleTargets.Add(sense.ReturnTarget());
            }
        }
    }
    [ContextMenu("Test")]
    public override void GetSenseResults()
    {
        // Vision -> Get Target
        foreach (ASense sense in Senses)
        {
            Dictionary<string, bool> result = sense.ReturnIntel();
            foreach (KeyValuePair<string, bool> key in result)
            {
                Informations.Add(key.Key, key.Value);
                Debug.Log(key.Key + " " + key.Value);
            }
        }

        GetPossibleTargets();
    }

    public override void MakeDecision()
    {
        if (Informations["InVision"] && !Informations["Blind"])
        {
            m_myStats.IsAttacking = true;
            m_myStats.IsAlerted = true;
            // Move To DetermineClosestEnemy()
            return;
        }
        if (Informations["Blind"])
        {
            m_myStats.IsAlerted = true;
        }
    }

    private GameObject DetermineClosestEnemy()
    {
        if(PossibleTargets.Count == 0)
        {
            return null;
        }

        float minDistance;
        minDistance = Vector3.Distance(transform.position, PossibleTargets[0].transform.position);
        GameObject target = PossibleTargets[0];
        for (int i = 1; i < PossibleTargets.Count; i++)
        {
            float dis = Vector3.Distance(transform.position, PossibleTargets[i].transform.position);
            if(dis < minDistance)
            {
                minDistance = dis;
                target = PossibleTargets[i];
            }
        }
        return target;
    }
}