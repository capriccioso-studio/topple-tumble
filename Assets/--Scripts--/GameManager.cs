using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GUIManager gui = null;
    public AdManager ads = null;
    private GameDatabase gameDB = null;
    public TMP_Text txt_resultScore = null;

    public static GameManager instance;

    public const string SEED_KEY = "seedEquip";
    public const string PLATFORM_KEY = "platformEquip";
    
    void Awake()
    {
        gameDB = GetComponent<GameDatabase>();
        gui = GetComponent<GUIManager>();
        ads = GetComponent<AdManager>();
        
        Global.gameManager = this;

        if(instance != null)
            Destroy(this);
        instance = this;

        SceneManager.LoadScene("Splash", LoadSceneMode.Additive);
        
        InitializeGameScene(gameDB);
        StartCoroutine(LoadingScreen());
    }

    public IEnumerator LoadingScreen()
    {

        if(!SceneManager.GetSceneByName("GameScene").isLoaded)
            LoadGameScene();

        yield return new WaitForSeconds(4);
        SceneManager.UnloadSceneAsync("Splash");
    }

    public void InitializeGameScene(GameDatabase gameDB)
    {
        int sID = PlayerPrefs.GetInt(SEED_KEY, 0);
        int pID = PlayerPrefs.GetInt(PLATFORM_KEY, 0);

        Global.seedtype = gameDB.seedShopItems[sID].seed;
        Global.platformtype = gameDB.platformShopItems[pID].platform;
        Global.environmentType = gameDB.environmentTypes;
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);

    }

    public void ResetScene()
    {
        SceneManager.UnloadSceneAsync("GameScene");
        txt_resultScore?.SetText(0 + "");
        CurrencyManager.instance.UpdateCurrency();
        SceneManager.LoadSceneAsync("GameScene", LoadSceneMode.Additive);
    }

    public void Die()
    {
        Debug.Log("ded");

        if(Global.backend.enabled)
        {
            Debug.Log("dead and setting scores");
            Global.backend.SetDBScores(Global.score, Global.orb);
        }


        if(Global.seed.GetComponent<Seed>().hasDied)
            gui.ChangeGUI(11);
        else
            gui.ChangeGUI(13);
    }

    public void Revive()
    {
        gui.ChangeGUI(0);

        Global.seed.GetComponent<Seed>().Revive();
        Global.platform.GetComponent<Platform>().Revive();
    }
}

