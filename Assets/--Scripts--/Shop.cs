using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    GameObject ItemTemplate;
    GameObject ItemTemplate2; //this script duplicates the items to 15 times
    GameObject g;
    GameObject h;
    [SerializeField] Transform ShopScrollView;
    [SerializeField] Transform ShopScrollView2;

    void Start()
    {
         ItemTemplate2 = ShopScrollView2.GetChild (0).gameObject;

        ItemTemplate = ShopScrollView.GetChild (0).gameObject;

        for(int i = 0; i<15; i++)
        {
            h = Instantiate (ItemTemplate2, ShopScrollView2);
            g = Instantiate (ItemTemplate, ShopScrollView);
        }

        Destroy  (ItemTemplate);
        Destroy  (ItemTemplate2);
    }
}
