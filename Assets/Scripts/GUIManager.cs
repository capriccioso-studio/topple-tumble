using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIManager : MonoBehaviour
{
    [Header("GUI Panels")]
    public GameObject[] guiPanels;


    [Header("Result")]
    public Text txt_resultScore;
    void Start()
    {
        ChangeGUI((int)GUISTATE.mainmenu);
    }

    public void ChangeGUI(int nextState)
    {
        print("GUI switch from " + Global.guiState + " to " + (GUISTATE)nextState);
        Global.guiState = (GUISTATE)nextState;
        
        switch(Global.guiState)
        {
            case GUISTATE.game: 
                StartGameGUI(); break;
            case GUISTATE.mainmenu:
                MainMenuGUI(); break;
            case GUISTATE.shop:
                ShopGUI(); break;
            case GUISTATE.achievements:
                AchievementsGUI(); break;
            case GUISTATE.achievementPanel:
                AchievementPanelGUI(); break;
            case GUISTATE.settings:
                SettingsGUI(); break;
            case GUISTATE.settingsabout:
                SettingsAboutGUI(); break;
            case GUISTATE.pause:
                PauseGUI(); break;
            case GUISTATE.inventory:
                InventoryGUI(); break;
            case GUISTATE.confirmationdialog:
                ConfirmationDialogGUI(); break;
            case GUISTATE.results:
                ResultsGUI(); break;
            case GUISTATE.loading:
                LoadingGUI(); break;
        }

    }

    public void ResetGUI()
    {
        foreach(GameObject panel in guiPanels)
        {
            if(panel != null)
                panel.SetActive(false);
        }
    }
    public void StartGameGUI()
    {
        ResetGUI();
        guiPanels[(int)Global.guiState].SetActive(true);
        Time.timeScale = 1;        

    }
    public void MainMenuGUI()
    {
        ResetGUI();
        guiPanels[(int)Global.guiState].SetActive(true);
    }
    public void ShopGUI()
    {
        guiPanels[(int)Global.guiState].SetActive(true);
    }
    public void AchievementsGUI()
    {
        
    }
    public void AchievementPanelGUI()
    {
        
    }
    public void SettingsGUI()
    {
        
    }
    public void SettingsAboutGUI()
    {
        
    }
    public void PauseGUI()
    {
        guiPanels[(int)Global.guiState].SetActive(true);
        Time.timeScale = 0;        
    }
    public void InventoryGUI()
    {
        
    }
    public void ConfirmationDialogGUI()
    {
        
    }
    public void ResultsGUI()
    {
        guiPanels[(int)Global.guiState].SetActive(true);
        StartCoroutine(Counter(5, 0, Global.score, txt_resultScore));
        print(Global.score);
    }
    public void LoadingGUI()
    {

    }



    public IEnumerator Counter(float dur, int min, int max, Text text)
    {
        float rate = (max - min) / (dur * 10);
        float timer = 0;
        float progress = min;
        while(timer < dur &&  Mathf.Ceil(progress) < max)
        {
            progress+=rate;
            timer+=0.01f;
            text.text = Mathf.Ceil(progress) + "";
            yield return new WaitForSeconds(0.01f);
        }
    }




    
    
}
