using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellingSense : ASense
{
    [SerializeField]
    private CircleCollider2D m_area;

    [SerializeField]
    private LayerMask m_targetLayer;

    [SerializeField]
    private GameObject m_target;
    public override void SetUp()
    {
        Results = new Dictionary<string, bool>();
        Results.Add("IsSmelling", false);
    }
    public override void GatherIntel()
    {
        Results["IsSmelling"] = IsSmelling();
    }

    public override Dictionary<string, bool> ReturnIntel()
    {
        return Results;
    }
    public GameObject ReturnTarget()
    {
        if(m_target == null)
        {
            return null;
        }
        return m_target;
    }
    private bool IsSmelling()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, m_area.radius, m_targetLayer);
        if(rangeCheck.Length > 0)
        {
            for (int i = 0; i < rangeCheck.Length; i++)
            {
                if(rangeCheck[i].TryGetComponent<AStats>(out AStats stats))
                {
                    if (stats.IsDirty)
                    {
                        m_target = rangeCheck[i].gameObject;
                        return true;
                    }
                }
            }
        }
        m_target = null;
        return false;
    }
}
