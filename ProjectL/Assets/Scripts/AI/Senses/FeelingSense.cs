using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeelingSense : ASense
{
    [SerializeField]
    private Collider2D m_body;

    [SerializeField]
    private LayerMask m_feelingMask;
    public override void SetUp()
    {
        Results = new Dictionary<string, bool>();
        Results.Add("IsFeeling", false);
    }
    public override void GatherIntel()
    {
        Results["IsFeeling"] = HasTouched();
    }

    //private void Update()
    //{
    //    Debug.Log(HasTouched());
    //}
    public override Dictionary<string, bool> ReturnIntel()
    {
        GatherIntel();
        return Results;
    }

    private bool HasTouched()
    {
        if (m_body.IsTouchingLayers(m_feelingMask))
        {
            return true;
        }
        return false;
    }
}
