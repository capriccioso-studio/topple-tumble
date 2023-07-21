using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public SeedScriptableObject seed;
    public PlatformScriptableObject platform;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeSeed(){
        Global.seedtype = seed;
        Debug.Log(Global.seedtype);
        Debug.Log(Global.seedtype.soloSprite);
    }

    public void changePlatform(){
        Global.platformtype = platform;
        Debug.Log(Global.platformtype);
        Debug.Log(Global.platformtype.platformSprite);
    }
}
