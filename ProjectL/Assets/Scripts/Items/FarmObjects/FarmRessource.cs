using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmRessource : MonoBehaviour, IFarmable
{
    public Tools NeededTool { get => m_neededTool; set => m_neededTool = value; }
    [SerializeField]
    private int m_amountFarmable = 0;
    [SerializeField]
    private Tools m_neededTool = Tools.NONE;

    // Data which Ressource
    public void Farm()
    {
        // Add To Inventory Amount
        Debug.Log("Add To Inventory "
            + this.name + m_amountFarmable);
    }
}
