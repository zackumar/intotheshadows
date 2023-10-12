using UnityEngine;

public abstract class Item : ScriptableObject
{
    public string Key => UniqueID;

    [Tooltip("Name used in the database for that Item, used by save system")]
    public string UniqueID = "DefaultID";

    public string DisplayName;
    public Sprite ItemSprite;
    public int MaxStackSize = 10;
    public bool Consumable = true;
    public int BuyPrice = -1;

    [Tooltip("Prefab that will be instantiated in the player hand when this is equipped")]
    public GameObject VisualPrefab;
    public string PlayerAnimatorTriggerUse = "GenericToolSwing";

    [Tooltip("Sound triggered when using the item")]
    public Sound[] UseSound;

    public abstract bool CanUse(Vector3 target);
    public abstract bool Use(Vector3 target);
    public abstract bool Use(PlayerController player);

    //override this for item that does not need a target (like Product, they can be eaten anytime)
    public virtual bool NeedTarget()
    {
        return true;
    }
}