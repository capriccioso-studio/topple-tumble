using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject SeedItemTemplate, PlatformItemTemplate;
    GameObject seedItem, platformItem;

    [SerializeField] Transform SeedShopScrollView;
    [SerializeField] Transform PlatformShopScrollView;
    
    //Very messy code, needs cleaning up

    public void Awake()
    {
        int sID=1, pID=1;
        SeedItemTemplate = SeedShopScrollView.GetChild(0).gameObject;
        PlatformItemTemplate = PlatformShopScrollView.GetChild(0).gameObject;

        /*Loops through the list of seed items*/
        foreach(SeedShopItemsScriptableObjects seed in GameDatabase.instance.seedShopItems)
        {
            if(!seed.defaultSeed)
            {
                seedItem = Instantiate(SeedItemTemplate, SeedShopScrollView);
                seed.itemRef = seedItem;
                seedItem.name = sID.ToString();
                sID++;
                
                /*Applies the information to the seed item template*/
                foreach(Transform child in seedItem.transform)
                {
                    if(child.gameObject.name == "money icon"){
                        child.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = seed.cost + "";
                    }else if(child.gameObject.name == "seed"){
                        child.gameObject.GetComponent<Image>().sprite = seed.image;
                    }
                }

                /*Turns button off if player bought item already*/
                if(PlayerPrefs.GetInt(seed.name) == 1){
                    Button button = seed.itemRef.transform.GetChild(2).gameObject.GetComponent<Button>();
                    button.interactable = false;
                    button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
                }
            }
            
        }

        /*Loops through the list of platform items*/
        foreach(PlatformShopItemsScriptableObjects platform in GameDatabase.instance.platformShopItems)
        {
            if(!platform.defaultPlatform)
            {
                platformItem = Instantiate(PlatformItemTemplate, PlatformShopScrollView);
                platform.itemRef = platformItem;
                platformItem.name = pID.ToString();
                pID++;
                
                /*Applies the information to the seed item template*/
                foreach(Transform child in platformItem.transform)
                {
                    if(child.gameObject.name == "money icon"){
                        child.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = platform.cost + "";
                    }else if(child.gameObject.name == "platform"){
                        child.gameObject.GetComponent<Image>().sprite = platform.image;
                    }
                }

                /*Turns button off if player bought item already*/
                if(PlayerPrefs.GetInt(platform.name) == 2){
                    Button button = platform.itemRef.transform.GetChild(2).gameObject.GetComponent<Button>();
                    button.interactable = false;
                    button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
                }
            }
        }
        
        Destroy  (SeedItemTemplate);
        Destroy  (PlatformItemTemplate);
    }

    public void BuySeed(GameObject seedItem)
    {
        SeedShopItemsScriptableObjects seed = GameDatabase.instance.seedShopItems[int.Parse(seedItem.name)];
        Button button = seedItem.transform.GetChild(2).gameObject.GetComponent<Button>();
        if(Global.orb >= seed.cost){
            Global.orb -= seed.cost;
            button.interactable = false;
            PlayerPrefs.SetInt(seed.name, button.interactable? 0 : 1);
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }

    public void BuyPlatform(GameObject platformItem)
    {
        PlatformShopItemsScriptableObjects platform = GameDatabase.instance.platformShopItems[int.Parse(platformItem.name)];
        Button button = platformItem.transform.GetChild(2).gameObject.GetComponent<Button>();
        if(Global.orb >= platform.cost){
            Global.orb -= platform.cost;
            button.interactable = false;
            PlayerPrefs.SetInt(platform.name, button.interactable? 0 : 2);
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }
}
