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
    private void Update()
    {
        GetSenseResults();
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
            if(sense.ReturnTarget() != null)
            {
                if (!PossibleTargets.Contains(sense.ReturnTarget()))
                {
                    PossibleTargets.Add(sense.ReturnTarget());
                }
            }
        }
    }
    [ContextMenu("Test")]
    public override void GetSenseResults()
    {
        Informations.Clear();
        // Vision -> Get Target
        foreach (ASense sense in Senses)
        {
            Dictionary<string, bool> result = sense.ReturnIntel();
            foreach (KeyValuePair<string, bool> key in result)
            {
                Informations.Add(key.Key, key.Value);
            }
        }
        GetPossibleTargets();
        MakeDecision();
    }

    public override void MakeDecision()
    {
        if (Informations["InVision"] && !Informations["Blind"])
        {
            m_myStats.IsAttacking = true;
            m_myStats.IsAlerted = true;
            Debug.Log("In Vision True, blind False");
        }
        if (Informations["IsHearing"])
        {
            m_myStats.IsAlerted = true;
            Debug.Log("Is Hearing true");
        }
        if (Informations["IsSmelling"])
        {
            m_myStats.IsAlerted = true;
            Debug.Log("Is Smelling true");
        }
        if (Informations["IsFeeling"])
        {
            m_myStats.IsAttacking = true;
            m_myStats.IsAlerted = true;
        }
        if (Informations["IsAlerted"])
        {
            m_myStats.IsAlerted = true;
        }
        if (Informations["Blind"])
        {
            m_myStats.IsAlerted = true;
            m_myStats.IsAttacking = false;
        }
        if(!Informations["InVision"] && !Informations["IsHearing"] && !Informations["IsSmelling"] 
            && !Informations["IsFeeling"] && !Informations["IsAlerted"] && !Informations["Blind"])
        {
            m_myStats.IsAttacking = false;
            m_myStats.IsAlerted = false;
            Debug.Log("Nothing");
        }
        //if(!Informations["InVision"] && !Informations["Blind"] && !Informations["IsFeeling"])
        //{
        //    m_myStats.IsAttacking = false;
        //}
        ExecuteDecision();
    }
    public override void ExecuteDecision()
    {
        if (m_myStats.IsAttacking)
        {
            GameObject target = DetermineClosestEnemy();
            //transform.position = Vector3.MoveTowards(transform.position, target.transform.position, 5f);
            transform.position = Vector2.Lerp(transform.position, target.transform.position, 0.001f);
            // Move To Enemy & Attack
        }
        if (m_myStats.IsAlerted)
        {
            GameObject target = DetermineClosestEnemy();
            // Turn To Enemy
            Extensions.LookAt2D(transform, new Vector2(target.transform.position.x, target.transform.position.y));
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

public static class Extensions
{
    public static void LookAt2D(Transform _origin, Transform _target)
    {
        Vector2 current = _origin.position;
        Vector2 direction = new Vector2(_target.position.x, _target.position.y) - current;
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //_origin.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _origin.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
    public static void LookAt2D(Transform _origin, Vector2 _target)
    {
        Vector2 current = _origin.position;
        Vector2 direction = _target - current;
        direction = direction.normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //_origin.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        _origin.rotation = Quaternion.Euler(0f, 0f, angle - 90);
    }
}