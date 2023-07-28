using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public GameObject SeedItemTemplate, PlatformItemTemplate; //this script duplicates the items to 15 times
    GameObject seedItem, platformItem;
    public SeedItems[] seed;
    public PlatformItems[] platform;

    [SerializeField] Transform SeedShopScrollView;
    [SerializeField] Transform PlatformShopScrollView;

    void Start()
    {
        SeedItemTemplate = SeedShopScrollView.GetChild (0).gameObject;
        PlatformItemTemplate = PlatformShopScrollView.GetChild (0).gameObject;

        foreach(SeedItems seed in seed)
        {
            seedItem = Instantiate (SeedItemTemplate, SeedShopScrollView);
            // seed.itemRef = seedItem;

            seedItem.transform.GetComponent<Button>().onClick.AddListener(() => {
                BuyItem(seed);
            });
            
            // foreach(Transform child in seedItem.transform){
            //     if(child.gameObject.name = "price")
            //     {
            //         child.gameObject.GetComponent<Text>().text = seed.cost.ToString();
            //     }else if(child.gameObject.name = "seed"){
            //         child.gameObject.GetComponent<Image>().sprite = seed.image;
            //     }
            // }
        }

        foreach(PlatformItems platform in platform)
        {
            platformItem = Instantiate (PlatformItemTemplate, PlatformShopScrollView);
        }

        // for(int i = 0; i<15; i++)
        // {
        //     seedItem = Instantiate (SeedItemTemplate, SeedShopScrollView);
        //     platformItem = Instantiate (PlatformItemTemplate, PlatformShopScrollView);
            
        // }

        Destroy  (SeedItemTemplate);
        Destroy  (PlatformItemTemplate);
    }

    public void BuyItem(SeedItems seed){
        Debug.Log("Clicked");
        if(Global.orb >= seed.cost){
            Global.orb -= seed.cost;
        }
    }
}

[System.Serializable]
public class SeedItems{
    public Sprite image;
    public int cost;
    public SeedScriptableObject seed;
    [HideInInspector] public GameObject itemRef;
}

[System.Serializable]
public class PlatformItems{
    public Sprite image;
    public int cost;
    public PlatformScriptableObject platform;
    [HideInInspector] public GameObject itemRef;
}
