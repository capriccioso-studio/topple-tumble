﻿using System.Collections;
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
    void Awake()
    {
        gameDB = GetComponent<GameDatabase>();
        gui = GetComponent<GUIManager>();
        ads = GetComponent<AdManager>();
        
        Global.gameManager = this;

        SceneManager.LoadScene("Splash", LoadSceneMode.Additive);
        
        InitializeGameScene(gameDB.seedTypes[0], gameDB.environmentTypes, gameDB.platformTypes[0]);
        StartCoroutine(LoadingScreen());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadingScreen()
    {

        if(!SceneManager.GetSceneByName("GameScene").isLoaded)
            LoadGameScene();

        yield return new WaitForSeconds(4);
        SceneManager.UnloadSceneAsync("Splash");
    }

    public void  InitializeGameScene(SeedScriptableObject seedtype, EnvironmentScriptableObject[] environmentType, PlatformScriptableObject platformtype)
    {
        Global.seedtype = seedtype;
        Global.platformtype = platformtype;
        Global.environmentType = environmentType;

    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Additive);

    }

    public void ResetScene()
    {
        SceneManager.UnloadSceneAsync("GameScene");
        Global.orb = 0;
        if (txt_resultScore is not null)
        txt_resultScore.SetText(0 + "");
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
