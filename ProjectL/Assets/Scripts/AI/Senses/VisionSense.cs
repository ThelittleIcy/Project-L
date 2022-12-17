using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSense : ASense
{
    public Transform Player { get => m_player; set => m_player = value; }
    [SerializeField]
    private Transform m_player;
    [SerializeField]
    private EnemyStats m_stats;
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
        return Results;
    }

    private bool IsInVision()
    {
        return false;
    }
    private bool IsBlind()
    {
        if(m_stats.IsBlind == true)
        {
            return true;
        }
        return false;
    }
}
