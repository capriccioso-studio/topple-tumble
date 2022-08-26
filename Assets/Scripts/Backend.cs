
using System;
using System.Net.Http;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class Backend : MonoBehaviour
{
    public static Backend Instance {get; private set;}
    public string auth;
    public string baseUrl;
    public string bearer;
    public string token_name;
    public string token_id;
    public string game_id;
    public string username;
    public int tokenAmount = 0;
    public TMP_Text authText;
    public TMP_Text amountText;
    public float originalFontSize = 0;
    // public TMP_Text tokentext;
    public HttpClient client = new HttpClient();
    public string deeplinkURL;
    public GameObject login;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;                
            Application.deepLinkActivated += onDeepLinkActivated;
            if (!string.IsNullOrEmpty(Application.absoluteURL))
            {
                // Cold start and Application.absoluteURL not null so process Deep Link.
                onDeepLinkActivated(Application.absoluteURL);
            }
            // Initialize DeepLink Manager global variable.
            else deeplinkURL = "[none]";
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Start()
    {
        Global.backend= this;
        DontDestroyOnLoad(this.gameObject);
        client = new HttpClient();
        // originalFontSize = tokentext.fontSize;
        Debug.Log("baseurl: " + baseUrl);
        Debug.Log("bearer: " + bearer);
        Debug.Log("user: " + auth);
#if UNITY_WEBGL
        GetAuthFromWebGL();
#endif

#if UNITY_EDITOR
        StartCoroutine(GetUser(()=>
        {
            GetDBToken();
        }));
#endif
    }   

    public void GetAuthFromWebGL()
    {
        int pm = Application.absoluteURL.IndexOf("?");
        if (pm != -1)
        {
            auth = Application.absoluteURL.Split("?"[0])[1].Split("=")[1];
            Debug.Log("new user: " + auth);
        }
    }

    public void SetDBScore(int score)
    {
        var newscore = PlayerPrefs.GetInt("score-"+username) + score;
        PlayerPrefs.SetInt("score-"+ username, score);
    }

    public void GetDBToken()
    {
        this.amountText.text = PlayerPrefs.GetInt("score-" + username) + "";
    }

    private IEnumerator GetToken()
    {
        Debug.Log(baseUrl + "/v1/users/" + auth + "/tokens/" + token_id);
        // Call asynchronous network methods in a try/catch block to handle exceptions.

        UnityWebRequest request = new UnityWebRequest($"{baseUrl}/v1/users/{auth}/tokens/{token_id}", "GET");
        request.SetRequestHeader("Authorization", "Bearer " + bearer);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error :(");
            // onErrorCallback(request.result);
            Debug.LogError(request.error, this);
        }
        else
        {
            Debug.Log("Token amount retrieved.");
        }

    }

    public IEnumerator GetUser(Action callback)
    {
        // Call asynchronous network methods in a try/catch block to handle exceptions.

        UnityWebRequest request = UnityWebRequest.Get($"{baseUrl}/api/v1/users/{auth}");
        request.SetRequestHeader("Authorization", "Bearer " + bearer);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error :(");
            // onErrorCallback(request.result);
            authText.text = "login error";
            Debug.LogError(request.error, this);
        }
        else
        {
            var jsonData = JSON.Parse(request.downloadHandler.text);
            this.username = jsonData["username"];
            authText.text = username;
            Debug.Log("User retrieved.");
            callback.Invoke();
        }
    }
    public void VoidClaimToken()
    {
        Debug.Log("Clicked claim button");
        StartCoroutine(ClaimTokens());
    }
    public IEnumerator ClaimTokens()
    {
        var amount = 100;
        //Get from firestore
        Debug.Log("Claiming Tumblecoins " );

        UnityWebRequest request = new UnityWebRequest($"{baseUrl}/api/v1/transactions/distribute?TokenId={token_name}&Amount={amount}&Auth={auth}", "POST");
        request.SetRequestHeader("Authorization", "Bearer " + bearer);

        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error :(");
            // onErrorCallback(request.result);
            Debug.LogError(request.error, this);
        }
        else
        {
            PlayerPrefs.SetInt("score-" + username, 0);
            GetDBToken();
            Debug.Log("Claimed tokens. Please check Xarcade.");
        }
    }

    public IEnumerator AirdropTokens(int amount)
    {
        Debug.Log("Airdropping Tumblecoins " + amount);

        UnityWebRequest request = new UnityWebRequest(baseUrl + "/api/v1/transactions/airdrop?DeveloperToken=" + token_name + "&DeveloperTokenAmount=" + amount + "&GamerId=" + auth, "POST");
        request.SetRequestHeader("Authorization", "Bearer " + bearer);

        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error :(");
            Debug.LogError(request.error, this);
        }
        else
        {
            Debug.Log("Airdrop Success!");

        }
    }

    public void OpenLogin()
    {
        Application.OpenURL("https://xarcade-gamer.proximaxtest.com/android-auth/" + game_id + "/tt:%2F%2Fauth");
    }

    private void onDeepLinkActivated(string url)
    {
        // Update DeepLink Manager global variable, so URL can be accessed from anywhere.
        deeplinkURL = url;
        // Decode the URL to determine action. 
        // In this example, the app expects a link formatted like this:
        // unitydl://mylink?scene1
        this.auth = url.Split("?"[0])[1].Split("=")[1];
        authText.text =this.auth;
        StartCoroutine(GetUser(()=>
        {
            GetDBToken();
        }));

        login.SetActive(false);
        Debug.Log("Opened from deeplink!");
    }

}
