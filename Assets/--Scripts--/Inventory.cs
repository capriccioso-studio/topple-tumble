using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameDatabase GD;
    public GameObject SeedItemTemplate, PlatformItemTemplate;
    GameObject seedItem,platformItem;

    int sID=0, pID=0;

    public Image SeedImage, PlatformImage;

    [SerializeField] Transform SeedInventoryScrollView,  PlatformInventoryScrollView;

    //Need to display items when buy button is clicked

    public void Start()
    {
        SeedItemTemplate = SeedInventoryScrollView.GetChild (0).gameObject;
        PlatformItemTemplate = PlatformInventoryScrollView.GetChild (0).gameObject;

        /*Loops through the list of unlocked seed items*/
        foreach(SeedShopItemsScriptableObjects seed in GD.seedShopItems)
        {
            if(seed.defaultSeed || PlayerPrefs.GetInt(seed.name) == 1){
                seedItem = Instantiate(SeedItemTemplate, SeedInventoryScrollView);
                seedItem.name = sID.ToString();
                sID++;
                    
                /*Applies the information to the seed item template*/
                foreach(Transform child in seedItem.transform)
                {
                    if(child.gameObject.name == "seed"){
                        child.gameObject.GetComponent<Image>().sprite = seed.image;
                    }
                }

                /*Turns button off if player equipped item already*/
                if(seed.seedEquipped){
                    Button button = seedItem.transform.GetChild(1).gameObject.GetComponent<Button>();
                    button.interactable = false;
                    button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIPPED";
                }
            }
        }

        /*Loops through the list of platform items*/
        foreach(PlatformShopItemsScriptableObjects platform in GD.platformShopItems)
        {
            if(platform.defaultPlatform || PlayerPrefs.GetInt(platform.name) == 2){
                platformItem = Instantiate(PlatformItemTemplate, PlatformInventoryScrollView);
                platformItem.name = pID.ToString();
                pID++;
                    
                /*Applies the information to the seed item template*/
                foreach(Transform child in platformItem.transform)
                {
                    if(child.gameObject.name == "platform"){
                        child.gameObject.GetComponent<Image>().sprite = platform.image;
                    }
                }

                /*Turns button off if player equipped item already*/
                if(platform.platformEquipped){
                    Button button = platformItem.transform.GetChild(1).gameObject.GetComponent<Button>();
                    button.interactable = false;
                    button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIPPED";
                }
            }
        }

        Destroy(SeedItemTemplate);
        Destroy(PlatformItemTemplate);
    }

    public void UpdateSeedInventory(GameObject seedID)
    {
        SeedItemTemplate = SeedInventoryScrollView.GetChild(0).gameObject;
        SeedShopItemsScriptableObjects seed = GameDatabase.instance.seedShopItems[int.Parse(seedID.name)];

        if(PlayerPrefs.GetInt(seed.name) == 1){
            seedItem = Instantiate(SeedItemTemplate, SeedInventoryScrollView);
            seedItem.name = sID.ToString();
            sID++;

            foreach(Transform child in seedItem.transform)
            {
                if(child.gameObject.name == "seed"){
                    child.gameObject.GetComponent<Image>().sprite = seed.image;
                }
            }

            Button button = seedItem.transform.GetChild(1).gameObject.GetComponent<Button>();
            button.interactable = true;
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIP";
        }
    }

    public void UpdatePlatformInventory(GameObject platformID)
    {
        PlatformItemTemplate = PlatformInventoryScrollView.GetChild(0).gameObject;
        PlatformShopItemsScriptableObjects platform = GameDatabase.instance.platformShopItems[int.Parse(platformID.name)];

        if(PlayerPrefs.GetInt(platform.name) == 2){
            platformItem = Instantiate(PlatformItemTemplate, PlatformInventoryScrollView);
            platformItem.name = pID.ToString();
            pID++;

            foreach(Transform child in platformItem.transform)
            {
                if(child.gameObject.name == "platform"){
                    child.gameObject.GetComponent<Image>().sprite = platform.image;
                }
            }

            Button button = platformItem.transform.GetChild(1).gameObject.GetComponent<Button>();
            button.interactable = true;
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIP";
        }
        
    }

    public void EquipSeed(GameObject seedItem)
    {
        SeedShopItemsScriptableObjects seed = GameDatabase.instance.seedShopItems[int.Parse(seedItem.name)];
        Button button = seedItem.transform.GetChild(1).gameObject.GetComponent<Button>();

        Global.seedtype = seed.seed;
        SeedImage.GetComponent<Image>().sprite = seed.image;
        button.interactable = false;
        seed.seedEquipped = true;
        button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIPPED";
    }

    public void EquipPlatform(GameObject platformItem)
    {
        PlatformShopItemsScriptableObjects platform = GameDatabase.instance.platformShopItems[int.Parse(platformItem.name)];
        Button button = platformItem.transform.GetChild(1).gameObject.GetComponent<Button>();

        Global.platformtype = platform.platform;
        PlatformImage.GetComponent<Image>().sprite = platform.image;
        button.interactable = false;
        platform.platformEquipped = true;
        button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIPPED";
    }

    public void DisableSeedButtons(GameObject content)
    {
        SeedShopItemsScriptableObjects seed;
        foreach (Transform item in content.transform)
        {
            seed = GameDatabase.instance.seedShopItems[int.Parse(item.name)];
            seed.seedEquipped = false;
        }
        foreach(var btn in content.GetComponentsInChildren<Button>())
        {
            btn.interactable = true;
            btn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIP";
        }
    }

    public void DisablePlatformButtons(GameObject content)
    {
        PlatformShopItemsScriptableObjects platform;
        foreach (Transform item in content.transform)
        {
            platform = GameDatabase.instance.platformShopItems[int.Parse(item.name)];
            platform.platformEquipped = false;
        }
        foreach(var btn in content.GetComponentsInChildren<Button>())
        {
            btn.interactable = true;
            btn.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIP";
        }
    }

}


