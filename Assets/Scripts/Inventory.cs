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
        Debug.Log(Global.platformtype);
        Debug.Log(Global.platformtype.platformSprite);
    }

    public void changePlatform(){
        Global.platformtype = platform;
        Debug.Log(Global.platformtype);
        Debug.Log(Global.platformtype.platformSprite);
    }
}
