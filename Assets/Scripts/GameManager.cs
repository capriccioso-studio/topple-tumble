using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private GameDatabase gameDB = null;
    void Awake()
    {
        gameDB = GetComponent<GameDatabase>();

        SceneManager.LoadScene("Splash", LoadSceneMode.Additive);
        StartCoroutine(LoadingScreen());
        
        InitializeGameScene(gameDB.seedTypes[0], gameDB.environmentTypes, gameDB.platformTypes[0]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator LoadingScreen()
    {

        if(!SceneManager.GetSceneByName("GameScene").isLoaded)
            LoadGameScene();

        yield return new WaitForSeconds(3);
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
}
