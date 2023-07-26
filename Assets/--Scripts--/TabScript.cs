using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabScript : MonoBehaviour
{
    public GameObject  tabButton1;
    public GameObject  tabButton2;

    public GameObject tabContent1;
    public GameObject tabContent2;


    void Start()
    {
        
    }

    public void HideAllTabs()
    {
        tabContent1.SetActive(false);
        tabContent2.SetActive(false);

        tabButton1.GetComponent<Button>().image.color = new Color32(183,183,183,255);
        tabButton2.GetComponent<Button>().image.color = new Color32(183,183,183,255);
    }

    public void ShowTab1()
    {
        HideAllTabs();
        tabContent1.SetActive(true);
        tabButton1.GetComponent<Button>().image.color = new Color32(255,255,255,255);
    }

    public void ShowTab2()
    {
        HideAllTabs();
        tabContent2.SetActive(true);
        tabButton2.GetComponent<Button>().image.color = new Color32(255,255,255,255);
    }
}
