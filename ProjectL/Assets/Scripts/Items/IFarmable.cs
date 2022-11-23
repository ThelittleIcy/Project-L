using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFarmable
{
    public void Farm();
}

public enum Tools
{
    NONE,
    AXE,
    PICKAXE,
    SHOVEL
}
