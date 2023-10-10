using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cloak", menuName = "Into The Shadows/Items/Cloak")]
public class Cloak : Item
{
    public override bool CanUse(Vector3Int target)
    {
        return true;
    }

    public override bool Use(Vector3Int target)
    {
        Debug.Log("Cloak");
        return true;
    }

    public override bool Use(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
