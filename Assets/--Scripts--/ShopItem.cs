using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItem : MonoBehaviour
{
    public bool isItemBought;
    public Button button;
    public TMP_Text text;

    public void OnButtonPress()
    {
        //test code
        if(Global.orb > 0){
            button.interactable = false;
            isItemBought = true;
            button.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "BOUGHT";
        }
    }
}
