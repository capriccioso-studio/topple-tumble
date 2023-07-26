using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    GameObject ItemTemplate,  ItemTemplate2; //this script duplicates the items to 15 times
    GameObject g,h;
    [SerializeField] Transform InventoryScrollView,  InventoryScrollView2;

    void Start()
    {
        
        ItemTemplate = InventoryScrollView.GetChild (0).gameObject;
        ItemTemplate2 = InventoryScrollView2.GetChild (0).gameObject;

        for(int i = 0; i<5; i++)
        {
            g = Instantiate (ItemTemplate, InventoryScrollView);
            h = Instantiate (ItemTemplate2, InventoryScrollView2);
        }

        Destroy  (ItemTemplate);
        Destroy  (ItemTemplate2);
    }


}
