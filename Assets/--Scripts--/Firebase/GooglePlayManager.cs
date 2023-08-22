using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayManager : MonoBehaviour
{
    public bool connected;

    private void Start()
    {
        PlayGamesPlatform.Activate();
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus status)
    {
        if (status == SignInStatus.Success)
        {
            // Continue with Play Games Services
            // print(PlayGamesPlatform.Instance.GetUserId());
            PlayGamesPlatform.Instance.RequestServerSideAccess(
            /* forceRefreshToken= */ false,
            code =>
            {
                Debug.Log(code);
                SignInWithGoogleAsync(code);
            });
        }
        else
        {
            // Disable your integration with Play Games Services or show a login button
            // to ask users to sign-in. Clicking it should call
            PlayGamesPlatform.Instance.ManuallyAuthenticate(ProcessAuthentication);
            Debug.Log("No");
        }
    }

    public void SignInWithGoogleAsync(string idToken)
    {
        // Your implementation goes here...
    }
}

