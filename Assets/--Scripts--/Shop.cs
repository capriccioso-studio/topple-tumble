using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject SeedItemTemplate, PlatformItemTemplate; //this script duplicates the items to 15 times
    GameObject seedItem, platformItem, test;
    public SeedItems[] seeds;
    public PlatformItems[] platforms;

    [SerializeField] Transform SeedShopScrollView;
    [SerializeField] Transform PlatformShopScrollView;

    void Start()
    {
        SeedItemTemplate = SeedShopScrollView.GetChild (0).gameObject;
        PlatformItemTemplate = PlatformShopScrollView.GetChild (0).gameObject;

        foreach(SeedItems seed in seeds)
        {
            seedItem = Instantiate (SeedItemTemplate, SeedShopScrollView);
            seed.itemRef = seedItem;
            
            foreach(Transform child in seedItem.transform){
                if(child.gameObject.name == "money icon")
                {
                    child.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = seed.cost + "";
                }
                else if(child.gameObject.name == "seed")
                {
                    child.gameObject.GetComponent<Image>().sprite = seed.image;
                }
            }

            seedItem.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => {
                BuySeed(seed);
            });  
        }

        foreach(PlatformItems platform in platforms)
        {
            platformItem = Instantiate (PlatformItemTemplate, PlatformShopScrollView);
            platform.itemRef = platformItem;
            
            foreach(Transform child in platformItem.transform){
                if(child.gameObject.name == "money icon")
                {
                    child.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = platform.cost + "";
                }
                else if(child.gameObject.name == "platform")
                {
                    child.gameObject.GetComponent<Image>().sprite = platform.image;
                }
            }

            platformItem.transform.GetChild(2).gameObject.GetComponent<Button>().onClick.AddListener(() => {
                BuyPlatform(platform);
            });
        }

        Destroy  (SeedItemTemplate);
        Destroy  (PlatformItemTemplate);
    }

    public void BuySeed(SeedItems seed){
        if(Global.orb >= seed.cost){
            Global.orb -= seed.cost;
            Button button = seed.itemRef.transform.GetChild(2).gameObject.GetComponent<Button>();
            button.interactable = false;
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }

    public void BuyPlatform(PlatformItems platform){
        if(Global.orb >= platform.cost){
            Global.orb -= platform.cost;
            Button button = platform.itemRef.transform.GetChild(2).gameObject.GetComponent<Button>();
            button.interactable = false;
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
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
