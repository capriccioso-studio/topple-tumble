using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;
    public const string currency = "ORB_CURRENCY";

    private void Awake()
    {
        if(instance != null)
            Destroy(this);
        instance = this;

        Global.orb = PlayerPrefs.GetInt(currency);
        Debug.Log("currency: " + PlayerPrefs.GetInt(currency));
    }

    public void UpdateCurrency()
    {
        Debug.Log("Global Orb" + Global.orb);
        PlayerPrefs.SetInt(currency, Global.orb);
        Debug.Log("currency: " + PlayerPrefs.GetInt(currency));
    }
}
