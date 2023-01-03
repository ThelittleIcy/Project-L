using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSense : ASense
{
    [SerializeField]
    private EnemyStats m_stats;

    [SerializeField]
    private float m_radius = 5f;
    [SerializeField]
    [Range(1, 360)]
    private float m_angle = 45f;

    [SerializeField]
    private LayerMask m_targetLayer;
    [SerializeField]
    private LayerMask m_obstructionLayer;
    /// <summary>
    /// Sets Up the Seeing Dictionary - In Vision, Blind etc.
    /// </summary>
    public override void SetUp()
    {
        Results = new Dictionary<string, bool>();
        Results.Add("InVision", false);
        Results.Add("Blind", false);
    }
    /// <summary>
    /// Sets the Values of the different Keys of the Dictionary.
    /// </summary>
    public override void GatherIntel()
    {
        Results["InVision"] = IsInVision();
        Results["Blind"] = IsBlind();
    }
    public override Dictionary<string, bool> ReturnIntel()
    {
        GatherIntel();
        return Results;
    }
    //private void Update()
    //{
    //    Debug.Log(IsInVision());
    //}
    private bool IsInVision()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, m_radius, m_targetLayer);

        if (rangeCheck.Length > 0)
        {
            for (int i = 0; i < rangeCheck.Length; i++)
            {
                Transform target = rangeCheck[i].transform;
                Vector2 directionToTarget = (target.position - transform.position).normalized;

                if (Vector2.Angle(transform.up, directionToTarget) < m_angle * 0.5f)
                {
                    float distanceToTarget = Vector2.Distance(transform.position, target.position);

                    if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, m_obstructionLayer))
                    {
                        Target = target.gameObject;
                        return true;
                    }
                }
            }
        }
        Target = null;
        return false;
    }
    private bool IsBlind()
    {
        if (m_stats.IsBlind == true)
        {
            return true;
        }
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        UnityEditor.Handles.color = Color.blue;
        UnityEditor.Handles.DrawWireDisc(transform.position, Vector3.forward, m_radius);

        Vector3 angle1 = DirectionFromAngles(-transform.eulerAngles.z, -m_angle * 0.5f);
        Vector3 angle2 = DirectionFromAngles(-transform.eulerAngles.z, m_angle * 0.5f);

        Gizmos.DrawLine(transform.position, transform.position + angle1 * m_radius);
        Gizmos.DrawLine(transform.position, transform.position + angle2 * m_radius);
    }
    private Vector2 DirectionFromAngles(float _eulerY, float _angleDegrees)
    {
        _angleDegrees += _eulerY;
        return new Vector2(Mathf.Sin(_angleDegrees * Mathf.Deg2Rad), Mathf.Cos(_angleDegrees * Mathf.Deg2Rad));
    }
}
