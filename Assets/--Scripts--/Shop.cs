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
    
    //Very messy code, needs cleaning up

    void Awake()
    {
        SeedItemTemplate = SeedShopScrollView.GetChild (0).gameObject;
        PlatformItemTemplate = PlatformShopScrollView.GetChild (0).gameObject;

        /*Loops through the list of seed items*/
        foreach(SeedItems seed in seeds)
        {
            seedItem = Instantiate (SeedItemTemplate, SeedShopScrollView);
            seed.itemRef = seedItem;
            
            /*Checks if gameobject exists, then applies the information to the seed item template*/
            foreach(Transform child in seedItem.transform)
            {
                if(child.gameObject.name == "money icon")
                {
                    child.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = seed.cost + "";
                }
                else if(child.gameObject.name == "seed")
                {
                    child.gameObject.GetComponent<Image>().sprite = seed.image;
                }
            }

            /*Checks for playerprefs if player bought object. If so, button interactable is set 
            to false and marked already bought. Otherwise, player can buy item and then playerpref 
            marks item as bought*/
            Button button = seedItem.transform.GetChild(2).gameObject.GetComponent<Button>();
            if(PlayerPrefs.GetInt(seed.name) == 1){
                button.interactable = false;
                button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
            }else{
                button.onClick.AddListener(() => {
                    BuySeed(seed);
                });
            } 
        }

        /*Loops through the list of platform items*/
        foreach(PlatformItems platform in platforms)
        {
            platformItem = Instantiate (PlatformItemTemplate, PlatformShopScrollView);
            platform.itemRef = platformItem;
            
            /*Checks if gameobject exists, then applies the information to the platform item template*/
            foreach(Transform child in platformItem.transform)
            {
                if(child.gameObject.name == "money icon")
                {
                    child.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = platform.cost + "";
                }
                else if(child.gameObject.name == "platform")
                {
                    child.gameObject.GetComponent<Image>().sprite = platform.image;
                }
            }

            /*Checks for playerprefs if player bought object. If so, button interactable is set 
            to false and marked already bought. Otherwise, player can buy item and then playerpref 
            marks item as bought*/
            Button button = seedItem.transform.GetChild(2).gameObject.GetComponent<Button>();
            if(PlayerPrefs.GetInt(platform.name) == 1){
                button.interactable = false;
                button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
            }else{
                button.onClick.AddListener(() => {
                    BuyPlatform(platform);
                });
            } 
        }
        Destroy  (SeedItemTemplate);
        Destroy  (PlatformItemTemplate);
    }

    public void BuySeed(SeedItems seed)
    {
        if(Global.orb >= seed.cost){
            Global.orb -= seed.cost;
            seed.isUnlocked = true;
            Button button = seed.itemRef.transform.GetChild(2).gameObject.GetComponent<Button>();
            button.interactable = false;
            PlayerPrefs.SetInt(seed.name, button.interactable? 0 : 1);
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }

    public void BuyPlatform(PlatformItems platform)
    {
        if(Global.orb >= platform.cost){
            Global.orb -= platform.cost;
            platform.isUnlocked = true;
            Button button = platform.itemRef.transform.GetChild(2).gameObject.GetComponent<Button>();
            button.interactable = false;
            PlayerPrefs.SetInt(platform.name, button.interactable? 0 : 1);
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }
}

[System.Serializable]
public class SeedItems{
    public Sprite image;
    public int cost;
    public string name; 
    public bool isUnlocked;
    public SeedScriptableObject seed;
    [HideInInspector] public GameObject itemRef;
}

[System.Serializable]
public class PlatformItems{
    public Sprite image;
    public int cost;
    public string name;
    public bool isUnlocked;
    public PlatformScriptableObject platform;
    [HideInInspector] public GameObject itemRef;
}
