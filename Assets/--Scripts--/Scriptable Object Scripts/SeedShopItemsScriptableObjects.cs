using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item/Seed", order = 1)]
public class SeedShopItemsScriptableObjects : ScriptableObject
{
    [Header("Seed Item Demographics")]
    
    // public ITEMTYPE type;
    public string itemName;
    public int ID;
    public int cost;
    [TextArea(5,10)] public string description; 
    public string seedEquipped;
    public bool defaultSeed;
    public SeedScriptableObject seed;

    [Header("Seed Sprites")]
    public Sprite image;
}
