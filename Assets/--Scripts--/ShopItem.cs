using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour
{
    public SeedScriptableObject seed;
    public PlatformScriptableObject platform;

    public void EquipSeed(){
        Global.seedtype = seed;
    }

    public void EquipPlatform(){
        Global.platformtype = platform;
    }
}
