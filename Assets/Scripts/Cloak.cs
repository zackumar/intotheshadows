using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cloak", menuName = "Into The Shadows/Items/Cloak")]
public class Cloak : Item
{
    public override bool CanUse(Vector3 target)
    {
        return true;
    }

    public override bool Use(Vector3 target)
    {
        Debug.Log("Cloak");
        AudioManager.instance.PlaySound(UseSound[0], target);
        return true;
    }

    public override bool Use(PlayerController player)
    {
        return false;
    }
}
