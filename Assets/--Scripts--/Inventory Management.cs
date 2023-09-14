using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManagement : MonoBehaviour
{
    public GameDatabase GD;
    public GameObject SeedItemTemplate, PlatformItemTemplate;
    GameObject seedItem, platformItem;

    public Image SeedImage, PlatformImage;

    [SerializeField] Transform SeedInventoryScrollView, PlatformInventoryScrollView;

    public void DisplayItemList()
    {
        PlayerPrefs.SetInt(GD.seedShopItems[0].name, 1);
        PlayerPrefs.SetInt(GD.platformShopItems[0].name, 2);

        SeedItemTemplate = SeedInventoryScrollView.GetChild(0).gameObject;
        PlatformItemTemplate = PlatformInventoryScrollView.GetChild(0).gameObject;

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

        SeedItemTemplate.SetActive(false);
        PlatformItemTemplate.SetActive(false);
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

    public void DisableEquipButtons(GameObject content)
    {
        foreach(var btn in content.GetComponentsInChildren<Button>())
        {
            btn.interactable = true;
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = "EQUIP";
        }
    }

    
    public void ClearSeedItems(GameObject content)
    {
        foreach (Transform item in content.transform)
        {
            if(item.gameObject.activeSelf)
                Destroy(item.gameObject);
        }
        SeedItemTemplate.SetActive(true);
    }


    public void ClearPlatformItems(GameObject content)
    {
        foreach (Transform item in content.transform)
        {
            if(item.gameObject.activeSelf)
                Destroy(item.gameObject);
        }
        PlatformItemTemplate.SetActive(true);
    }
}


