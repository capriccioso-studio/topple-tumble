
using System;
using System.IO;
using System.Net.Http;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using SimpleJSON;
using TMPro;

public class Backend : MonoBehaviour
{
    public static Backend Instance {get; private set;}
    public string userId;
    public string baseUrl = "192.168.0.102:8081";
    public string api_key;
    public string token_id;
    public int tokenAmount = 0;
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
            DontDestroyOnLoad(gameObject);
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
        Debug.Log("api_key: " + api_key);
        Debug.Log("user: " + userId);

        int pm = Application.absoluteURL.IndexOf("?");
        if (pm != -1)
        {
            userId = Application.absoluteURL.Split("?"[0])[1].Split("=")[1];
            Debug.Log("new user: " + userId);
        }
    }   

    private async Task<string> GetTokens()
    {
        Debug.Log(baseUrl + "/v1/users/" + userId + "/tokens");
        // Call asynchronous network methods in a try/catch block to handle exceptions.
        try	
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.RequestUri = new Uri(baseUrl + "/v1/users/" + userId + "/tokens");
            request.Method = HttpMethod.Get;
            request.Headers.Add("api_key", api_key);
            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            // Above three lines can be replaced with new helper method below
            // string responseBody = await client.GetStringAsync(baseUrl + "/v1/users/" + userId + "/tokens");
            var jsonData = JSON.Parse(responseBody);
            
            var tokens = jsonData["viewModel"];
            tokenAmount = 0;
            foreach (JSONNode token in tokens)
            {
                if(token["tokenId"] == token_id)
                {
                    tokenAmount = token["amount"];
                }
            }
            // tokentext.text = tokenAmount +"";
            // tokentext.fontSize = originalFontSize + tokenAmount ;
            Debug.Log(tokenAmount);
        }
        catch(HttpRequestException e)
        {
            Debug.Log("\nException Caught!");	
            Debug.Log(e.Message);
        }

        return "";
    }

    public IEnumerator AirdropTokens(int amount)
    {
        UnityWebRequest request = new UnityWebRequest(baseUrl + "/v1/transactions/airdrop?DeveloperToken=" + token_id + "&DeveloperTokenAmount=" + amount + "&GamerId=" + userId, "POST");
        // request.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        request.SetRequestHeader("Authorization", "Bearer " + api_key);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.Log("Error :(");
            // onErrorCallback(request.result);
            Debug.LogError(request.error, this);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            // callback(request.downloadHandler.text);
        }
    }

    public void OpenLogin()
    {
        Application.OpenURL("https://xarcade-gamer.proximaxtest.com/android-auth/tt:%2F%2Fauth");
    }
    private void onDeepLinkActivated(string url)
    {
        // Update DeepLink Manager global variable, so URL can be accessed from anywhere.
        deeplinkURL = url;
                
        // Decode the URL to determine action. 
        // In this example, the app expects a link formatted like this:
        // unitydl://mylink?scene1
        this.userId = url.Split("?"[0])[1];
        login.SetActive(false);
    }

}
