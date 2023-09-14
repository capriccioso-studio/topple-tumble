using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop : MonoBehaviour
{
    public GameObject SeedItemTemplate, PlatformItemTemplate;
    GameObject seedItem, platformItem;

    public GameObject orb;

    [SerializeField] Transform SeedShopScrollView;
    [SerializeField] Transform PlatformShopScrollView;

    public void Awake()
    {
        /***SHOWS ORB TOTAL***/
        UpdateOrb();

        /***DISPLAYS SHOP LIST***/
        SeedItemTemplate = SeedShopScrollView.GetChild(0).gameObject;
        PlatformItemTemplate = PlatformShopScrollView.GetChild(0).gameObject;

        /*Loops through the list of seed items*/
        foreach(SeedShopItemsScriptableObjects seed in GameDatabase.instance.seedShopItems)
        {
            if(!seed.defaultSeed)
            {
                seedItem = Instantiate(SeedItemTemplate, SeedShopScrollView);
                seedItem.name = seed.ID.ToString();

                AddSeedItemDetails(seedItem, seed);
            }
        }

        /*Loops through the list of platform items*/
        foreach(PlatformShopItemsScriptableObjects platform in GameDatabase.instance.platformShopItems)
        {
            if(!platform.defaultPlatform)
            {
                platformItem = Instantiate(PlatformItemTemplate, PlatformShopScrollView);
                platformItem.name = platform.ID.ToString();
                
                AddPlatformItemDetails(platformItem, platform);
            }
        }
        
        Destroy(SeedItemTemplate);
        Destroy(PlatformItemTemplate);
    }


    public void AddSeedItemDetails(GameObject seedItem, SeedShopItemsScriptableObjects seed)
    {
        /*Applies the information to the seed item template*/
        seedItem.transform.GetChild(0).GetComponent<Image>().sprite = seed.image;
        seedItem.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = seed.cost + "";

        /*Turns button off if player bought item already*/
        if(PlayerPrefs.GetInt(seed.name) == 1){
            Button button = seedItem.transform.GetChild(2).GetComponent<Button>();
            button.interactable = false;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }


    public void AddPlatformItemDetails(GameObject platformItem, PlatformShopItemsScriptableObjects platform)
    {
        /*Applies the information to the platform item template*/
        platformItem.transform.GetChild(0).GetComponent<Image>().sprite = platform.image;
        platformItem.transform.GetChild(1).GetChild(0).GetComponent<TMP_Text>().text = platform.cost + "";

        /*Turns button off if player bought item already*/
        if(PlayerPrefs.GetInt(platform.name) == 2){
            Button button = platformItem.transform.GetChild(2).GetComponent<Button>();
            button.interactable = false;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }


    public void BuySeed(GameObject seedItem)
    {
        SeedShopItemsScriptableObjects seed = GameDatabase.instance.seedShopItems[int.Parse(seedItem.name)];
        Button button = seedItem.transform.GetChild(2).GetComponent<Button>();

        if(Global.orb >= seed.cost){
            Global.orb -= seed.cost;
            CurrencyManager.instance.UpdateCurrency();
            orb.GetComponent<TMP_Text>().text = Global.orb + "";
            button.interactable = false;
            PlayerPrefs.SetInt(seed.name, button.interactable? 0 : 1);
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }


    public void BuyPlatform(GameObject platformItem)
    {
        PlatformShopItemsScriptableObjects platform = GameDatabase.instance.platformShopItems[int.Parse(platformItem.name)];
        Button button = platformItem.transform.GetChild(2).GetComponent<Button>();

        if(Global.orb >= platform.cost){
            Global.orb -= platform.cost;
            CurrencyManager.instance.UpdateCurrency();
            orb.GetComponent<TMP_Text>().text = Global.orb + "";
            button.interactable = false;
            PlayerPrefs.SetInt(platform.name, button.interactable? 0 : 2);
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }

    public void UpdateOrb()
    {
        orb.gameObject.GetComponent<TMP_Text>().text = Global.orb + "";
    }
}
