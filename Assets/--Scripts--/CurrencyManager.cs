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

        Global.orb = PlayerPrefs.GetInt(currency, 0);
        Debug.Log(Global.orb);
    }

    public void UpdateCurrency()
    {
        PlayerPrefs.SetInt(currency, Global.orb);
    }
}
