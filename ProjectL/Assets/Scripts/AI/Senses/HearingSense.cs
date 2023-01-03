using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class HearingSense : ASense
{
    [SerializeField]
    private float m_radius = 5f;
    [SerializeField]
    private LayerMask m_hearingMask;
    public override void SetUp()
    {
        Results = new Dictionary<string, bool>();
        Results.Add("IsHearing", false);
    }
    public override void GatherIntel()
    {
        Results["IsHearing"] = IsHearing();
    }

    public override Dictionary<string, bool> ReturnIntel()
    {
        GatherIntel();
        return Results;
    }
    private bool IsHearing()
    {
        //if (m_hearingArea.IsTouchingLayers(m_hearingMask))
        //{
        //    return true;
        //}

        // AudioSource?
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, m_radius, m_hearingMask);
        if (rangeCheck.Length > 0)
        {
            for (int i = 0; i < rangeCheck.Length; i++)
            {
                if (rangeCheck[i].gameObject.TryGetComponent<AudioSource>(out AudioSource source))
                {
                    if (source.isPlaying)
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
        UnityEditor.Handles.color = Color.red;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, m_radius);
    }
}
