using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms;

public class GooglePlayManager : MonoBehaviour
{
    public bool connected;
    public TMP_Text test;

    private void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        
    }

    internal void ProcessAuthentication(SignInStatus status) {
      if (status == SignInStatus.Success) {
        test.text = "Connected to Google Play";
      } else {
        test.text = "Did not connect to Google Play";
      }
    }

    public void SignInWithGoogleAsync()
    {
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
    }

    public void ShowLeaderboard()
    {
      Social.ShowLeaderboardUI();
    }
}

