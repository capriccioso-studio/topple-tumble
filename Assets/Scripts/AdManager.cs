using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdManager : MonoBehaviour, IUnityAdsListener
{
    string gameId = "1234567";
    string myPlacementId = "rewardedVideo";
    bool testMode = true;
    Button btn_watchAd = null;

    // Initialize the Ads listener and service:
    void Start () {
        Global.adManager = this;
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished) {
            if(Global.adRewardType == ADREWARDTYPE.revive)
            {
                Global.gameManager.Revive();
            }
        } else if (showResult == ShowResult.Skipped) {
            print("FUCKING ASSEHOLE");

        } else if (showResult == ShowResult.Failed) {
            Debug.LogWarning ("The ad did not finish due to an error.");
        }
    }

    public void LoadAD(Button btn_watchAd)
    {
        Advertisement.AddListener (this);
        Advertisement.Initialize (gameId, testMode);
        this.btn_watchAd = btn_watchAd;
    }

    public void StartAd()
    {
        StartCoroutine(ShowWhenReady());
    }

    IEnumerator ShowWhenReady () {
        while (!Advertisement.IsReady (myPlacementId)) {
            yield return new WaitForSeconds (0.5f);
        }
        Advertisement.Show (myPlacementId);
    }

    public void OnUnityAdsReady (string placementId) {
        // If the ready Placement is rewarded, show the ad:
        if (placementId == myPlacementId) {
            btn_watchAd.interactable = true;
        }
    }

    public void OnUnityAdsDidError (string message) {
        // Log the error.
    }

    public void OnUnityAdsDidStart (string placementId) {
        // Optional actions to take when the end-users triggers an ad.
    } 
}
