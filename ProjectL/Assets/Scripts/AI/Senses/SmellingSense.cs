using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellingSense : ASense
{
    [SerializeField]
    private float m_radius = 1f;

    [SerializeField]
    private LayerMask m_targetLayer;
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
        GatherIntel();
        return Results;
    }
    private bool IsSmelling()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, m_radius, m_targetLayer);
        if(rangeCheck.Length > 0)
        {
            for (int i = 0; i < rangeCheck.Length; i++)
            {
                if(rangeCheck[i].TryGetComponent<AStats>(out AStats stats))
                {
                    if (stats.IsDirty)
                    {
                        Target = rangeCheck[i].gameObject;
                        return true;
                    }
                }
            }
        }
        Target = null;
        return false;
    }

    private void OnDrawGizmos()
    {
        UnityEditor.Handles.color = Color.yellow;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, m_radius);
    }
}
