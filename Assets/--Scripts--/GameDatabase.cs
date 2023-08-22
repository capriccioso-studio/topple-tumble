using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDatabase : MonoBehaviour
{
    public SeedScriptableObject[] seedTypes;
    public PlatformScriptableObject[] platformTypes;
    public EnvironmentScriptableObject[] environmentTypes;
    public SeedShopItemsScriptableObjects[] seedShopItems;
    public PlatformShopItemsScriptableObjects[] platformShopItems;

    public static GameDatabase instance;
    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;
    }
}
