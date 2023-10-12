using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySpawner : MonoBehaviour
{

    public Item item;
    // Start is called before the first frame update
    public bool spawnInfinite = false;
    private bool spanwed = false;

    public void Add()
    {
        if (spanwed) return;
        if (!spawnInfinite) spanwed = true;
        Inventory.Instance.AddItem(item);
    }
}
