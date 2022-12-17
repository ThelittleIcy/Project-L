using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ASense : MonoBehaviour
{
    /// <summary>
    /// Baseclass for all Senses.
    /// </summary>
    public Dictionary<string, bool> Results { get => m_results; set => m_results = value; }
    private Dictionary<string, bool> m_results;
    /// <summary>
    /// Sets Up this Sense: Dictionary, etc.
    /// </summary>
    public abstract void SetUp();
    /// <summary>
    /// Updates the different Aspects of the Dictionary.
    /// </summary>
    public abstract void GatherIntel();
    /// <summary>
    /// Returns the Dictionary which contains the result.
    /// </summary>
    /// <returns>The results</returns>
    public abstract Dictionary<string, bool> ReturnIntel();
}
