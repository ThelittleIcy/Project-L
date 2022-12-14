using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroupSense : ASense
{
    [SerializeField]
    private GroupLeader m_leader;
    public override void SetUp()
    {
        Results = new Dictionary<string, bool>();
        Results.Add("IsAlerted", false);

        if (m_leader == null)
        {
            Debug.LogError("You did not Assign a Leader on " + this.gameObject);
            return;
        }
        m_leader.GroupMembers.Add(this.gameObject.GetComponent<AStats>());
    }
    public override void GatherIntel()
    {
        Results["IsAlerted"] = IsAlerted();
    }
    public override Dictionary<string, bool> ReturnIntel()
    {
        GatherIntel();
        return Results;
    }

    private bool IsAlerted()
    {
        // Dich selbst rausnehmen
        for (int i = 0; i < m_leader.GroupMembers.Count; i++)
        {
            if (m_leader.GroupMembers[i].gameObject != gameObject)
            {
                if (m_leader.GroupMembers[i].IsAlerted || m_leader.GroupMembers[i].IsAttacking)
                {
                    return true;
                }
            }

        }
        return false;
    }
}
