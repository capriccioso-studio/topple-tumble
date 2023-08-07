using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Item/Platform", order = 2)]
public class PlatformShopItemsScriptableObjects : ScriptableObject
{
    [Header("Platform Item Demographics")]
    
    // public ITEMTYPE type;
    public string itemName;
    public int cost;
    [TextArea(5,10)] public string description; 
    public string platformEquipped;
    public bool defaultPlatform;
    public PlatformScriptableObject platform;
    [HideInInspector] public GameObject itemRef;

    [Header("Platform Sprites")]
    public Sprite image;
}
