using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Collider2D))]
public class HearingSense : ASense
{
    public Collider2D HearingArea { get => m_hearingArea; set => m_hearingArea = value; }
    [SerializeField]
    private Collider2D m_hearingArea;

    [SerializeField]
    private LayerMask m_hearingMask;
    public override void SetUp()
    {
        m_hearingArea.isTrigger = true;
        Results = new Dictionary<string, bool>();
        Results.Add("IsHearing", false);
    }
    public override void GatherIntel()
    {
        Results["IsHearing"] = IsHearing();
    }

    public override Dictionary<string, bool> ReturnIntel()
    {
        return Results;
    }
    private bool IsHearing()
    {
        if (m_hearingArea.IsTouchingLayers(m_hearingMask))
        {
            return true;
        }
        return false;
    }
}
