using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public static Inventory Instance;

    public bool[] isFull;
    public GameObject[] slots;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void AddItem(Item item)
    {
        for (int i = 0; i < isFull.Length; i++)
        {
            if (isFull[i] == false)
            {
                slots[i].GetComponent<InventorySlot>().AddItem(item);
                break;
            }
        }
    }
}
