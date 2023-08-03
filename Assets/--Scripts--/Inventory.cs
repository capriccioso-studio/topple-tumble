using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Very messy code, needs cleaning up

public class Inventory : MonoBehaviour
{
    GameObject SeedItemTemplate,  PlatformItemTemplate; //this script duplicates the items to 15 times
    GameObject seedItem,platformItem;

    public SeedItems[] seeds;
    public PlatformItems[] platforms;
    public Image SeedImage, PlatformImage;

    [SerializeField] Transform SeedInventoryScrollView,  PlatformInventoryScrollView;

    void Awake()
    {
        SeedItemTemplate = SeedInventoryScrollView.GetChild (0).gameObject;
        PlatformItemTemplate = PlatformInventoryScrollView.GetChild (0).gameObject;

        /*Puts in default seed*/
        PlayerPrefs.SetInt(seeds[0].name, 1);
        SeedImage.GetComponent<Image>().sprite = seeds[0].image;

        /*Loops through the list of unlocked seed items*/
        foreach(SeedItems seed in seeds)
        {
            if(PlayerPrefs.GetInt(seed.name) == 1)
            {
                seedItem = Instantiate (SeedItemTemplate, SeedInventoryScrollView);
                seed.itemRef = seedItem;

                /*Checks if gameobject exists, then applies the information to the seed item template*/
                foreach(Transform child in seedItem.transform)
                {
                    if(child.gameObject.name == "seed")
                        child.gameObject.GetComponent<Image>().sprite = seed.image;
                }

                /*Checks for playerprefs if player equipped object. If so, button interactable is set 
                to false and marked equipped. Otherwise, player can equip item*/
                // Button buttonS = seedItem.transform.GetChild(1).gameObject.GetComponent<Button>();
                // if(PlayerPrefs.GetInt(seed.seedEquipped) == 1){
                //     buttonS.interactable = false;
                //     buttonS.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIPPED";
                // }else{
                //     buttonS.onClick.AddListener(() => {
                //         EquipSeed(seed);
                //     }); 
                // }
            }
        }

        /*Puts in default platform*/
        PlayerPrefs.SetInt(platforms[0].name, 1);
        PlatformImage.GetComponent<Image>().sprite = platforms[0].image;

        /*Loops through the list of platform items*/
        foreach(PlatformItems platform in platforms)
        {
            if(PlayerPrefs.GetInt(platform.name) == 1)
            {
                platformItem = Instantiate (PlatformItemTemplate, PlatformInventoryScrollView);
                platform.itemRef = platformItem;
                
                /*Checks if gameobject exists, then applies the information to the platform item template*/
                foreach(Transform child in platformItem.transform)
                {
                    if(child.gameObject.name == "platform")
                        child.gameObject.GetComponent<Image>().sprite = platform.image;
                }

                /*Checks for playerprefs if player equipped object. If so, button interactable is set 
                to false and marked equipped. Otherwise, player can equip item*/
                // Button buttonP = seedItem.transform.GetChild(1).gameObject.GetComponent<Button>();
                // if(PlayerPrefs.GetInt(platform.platformEquipped) == 1){
                //     buttonP.interactable = false;
                //     buttonP.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIPPED";
                // }else{
                //     buttonP.onClick.AddListener(() => {
                //         EquipPlatform(platform);
                //     }); 
                // }
            }
        }

        Destroy(SeedItemTemplate);
        Destroy(PlatformItemTemplate);
    }

    public void EquipSeed(SeedItems seed){
        Global.seedtype = seed.seed;
        SeedImage.GetComponent<Image>().sprite = seed.image;
        Button button = seed.itemRef.transform.GetChild(1).gameObject.GetComponent<Button>();
        button.interactable = false;
        PlayerPrefs.SetInt(seed.seedEquipped, button.interactable? 0 : 1);
        button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIPPED";
    }

    public void EquipPlatform(PlatformItems platform){
        Global.platformtype = platform.platform;
        PlatformImage.GetComponent<Image>().sprite = platform.image;
        Button button = platform.itemRef.transform.GetChild(1).gameObject.GetComponent<Button>();
        button.interactable = false;
        PlayerPrefs.SetInt(platform.platformEquipped, button.interactable? 0 : 1);
        button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "EQUIPPED";
    }

}


