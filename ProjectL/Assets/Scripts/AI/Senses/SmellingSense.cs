using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmellingSense : ASense
{
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
    private bool IsSmelling()
    {
        return false;
    }
}
