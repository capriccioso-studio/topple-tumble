using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;

public class GooglePlayManager : MonoBehaviour
{
    public bool connected;

    private void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status) {
      connected = (status == SignInStatus.Success)? true : false;
    }

    public void ShowLeaderboard()
    {
      if(!connected)
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
      Social.ShowLeaderboardUI();
    }

    public void SubmitToLeaderboard()
    {
      Social.ReportScore(Global.score, "CgkIqcOlxe4JEAIQAA", (bool success) =>{
        Debug.Log(success);
      });
    }

    public void ShowAchievements()
    {
      if(!connected)
        PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
      Social.ShowAchievementsUI();
    }

    public void GetHighScoreAchievement(int score)
    {
      Social.ReportProgress("CgkIqcOlxe4JEAIQAg", 100.0f, (bool success) => {
        Debug.Log(success);
      });
    }

    public void GetOrbAchievement(int orb)
    {
      Social.ReportProgress("CgkIqcOlxe4JEAIQAw", 100.0f, (bool success) => {
        Debug.Log(success);
      });
    }
}

