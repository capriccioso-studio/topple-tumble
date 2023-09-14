using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using GooglePlayGames.BasicApi;

public class InventoryManagement : MonoBehaviour
{
    public GameDatabase GD;
    public GameObject SeedItemTemplate, PlatformItemTemplate;
    GameObject seedItem, platformItem;

    public Image SeedImage, PlatformImage;

    [SerializeField] Transform SeedInventoryScrollView, PlatformInventoryScrollView;

    public void Awake()
    {

        /***************
        Change the bool of seed/platform equip because boolean will not reset when clearing playerprefs or when closing Unity. Might have to use playerprefs
        ************/

        PlayerPrefs.SetInt(GD.seedShopItems[0].name, 1);
        PlayerPrefs.SetInt(GD.platformShopItems[0].name, 2);

        SeedItemTemplate = SeedInventoryScrollView.GetChild(0).gameObject;
        PlatformItemTemplate = PlatformInventoryScrollView.GetChild(0).gameObject;

        // DisableSeedButtons(SeedItemTemplate);

        /*Loops through the list of unlocked seed items*/
        foreach(SeedShopItemsScriptableObjects seed in GD.seedShopItems)
        {
            if(PlayerPrefs.GetInt(seed.name) == 1){
                seedItem = Instantiate(SeedItemTemplate, SeedInventoryScrollView);
                seedItem.name = seed.ID.ToString();
                    
                AddSeedItemDetails(seedItem, seed);
            }
        }

        /*Loops through the list of platform items*/
        foreach(PlatformShopItemsScriptableObjects platform in GD.platformShopItems)
        {
            if(PlayerPrefs.GetInt(platform.name) == 2){
                platformItem = Instantiate(PlatformItemTemplate, PlatformInventoryScrollView);
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

        /*Turns button off if player equipped item already by checking the SEED KEY in Game Manager*/
        if(seedItem.name == PlayerPrefs.GetInt(GameManager.SEED_KEY, 0).ToString()){
            Button button = seedItem.transform.GetChild(1).GetComponent<Button>();
            button.interactable = false;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "EQUIPPED";
        }
    }


    public void AddPlatformItemDetails(GameObject platformItem, PlatformShopItemsScriptableObjects platform)
    {
        /*Applies the information to the platform item template*/
        platformItem.transform.GetChild(0).GetComponent<Image>().sprite = platform.image;

        /*Turns button off if player equipped item already by checking the PLATFORM KEY in Game Manager*/
        if(platformItem.name == PlayerPrefs.GetInt(GameManager.PLATFORM_KEY, 0).ToString()){
            Button button = platformItem.transform.GetChild(1).GetComponent<Button>();
            button.interactable = false;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "EQUIPPED";
        }
    }


    /*****
    When updating inventory, seed is added to the end of list, only arranges itself
    when the game restarts 
    *****/
    public void UpdateSeedInventory(GameObject seedID)
    {
        SeedItemTemplate = SeedInventoryScrollView.GetChild(0).gameObject;
        SeedShopItemsScriptableObjects seed = GameDatabase.instance.seedShopItems[int.Parse(seedID.name)];

        if(PlayerPrefs.GetInt(seed.name) == 1){
            seedItem = Instantiate(SeedItemTemplate, SeedInventoryScrollView);
            seedItem.name = seed.ID.ToString();

            seedItem.transform.GetChild(0).GetComponent<Image>().sprite = seed.image;

            Button button = seedItem.transform.GetChild(1).GetComponent<Button>();
            button.interactable = true;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "EQUIP";
        }
    }

    public void UpdatePlatformInventory(GameObject platformID)
    {
        PlatformItemTemplate = PlatformInventoryScrollView.GetChild(0).gameObject;
        PlatformShopItemsScriptableObjects platform = GameDatabase.instance.platformShopItems[int.Parse(platformID.name)];

        if(PlayerPrefs.GetInt(platform.name) == 2){
            platformItem = Instantiate(PlatformItemTemplate, PlatformInventoryScrollView);
            platformItem.name = platform.ID.ToString();

            platformItem.transform.GetChild(0).GetComponent<Image>().sprite = platform.image;

            Button button = platformItem.transform.GetChild(1).GetComponent<Button>();
            button.interactable = true;
            button.transform.GetChild(0).GetComponent<TMP_Text>().text = "EQUIP";
        }
        
    }

    public void EquipSeed(GameObject seedItem)
    {
        SeedShopItemsScriptableObjects seed = GameDatabase.instance.seedShopItems[int.Parse(seedItem.name)];
        Button button = seedItem.transform.GetChild(1).GetComponent<Button>();

        Global.seedtype = seed.seed;
        SeedImage.GetComponent<Image>().sprite = seed.image;
        button.interactable = false;
        PlayerPrefs.SetInt(GameManager.SEED_KEY, int.Parse(seedItem.name));
        button.transform.GetChild(0).GetComponent<TMP_Text>().text = "EQUIPPED";
    }

    public void EquipPlatform(GameObject platformItem)
    {
        PlatformShopItemsScriptableObjects platform = GameDatabase.instance.platformShopItems[int.Parse(platformItem.name)];
        Button button = platformItem.transform.GetChild(1).GetComponent<Button>();

        Global.platformtype = platform.platform;
        PlatformImage.GetComponent<Image>().sprite = platform.image;
        button.interactable = false;
        PlayerPrefs.SetInt(GameManager.PLATFORM_KEY, int.Parse(platformItem.name));
        button.transform.GetChild(0).GetComponent<TMP_Text>().text = "EQUIPPED";
    }

    public void DisableSeedButtons(GameObject content)
    {
        SeedShopItemsScriptableObjects seed;
        foreach (Transform item in content.transform)
        {
            seed = GameDatabase.instance.seedShopItems[int.Parse(item.name)];
        }
        foreach(var btn in content.GetComponentsInChildren<Button>())
        {
            btn.interactable = true;
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = "EQUIP";
        }
    }

    public void DisablePlatformButtons(GameObject content)
    {
        PlatformShopItemsScriptableObjects platform;
        foreach (Transform item in content.transform)
        {
            platform = GameDatabase.instance.platformShopItems[int.Parse(item.name)];
        }
        foreach(var btn in content.GetComponentsInChildren<Button>())
        {
            btn.interactable = true;
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = "EQUIP";
        }
    }
}


