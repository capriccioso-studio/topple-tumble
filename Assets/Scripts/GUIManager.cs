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

    [Header("Revive")]
    public Button btn_WatchAdToRevive;
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
            case GUISTATE.reviveprompt:
                RevivePromptGUI(); break;
        }

    }

    private void ResetGUI()
    {
        foreach(GameObject panel in guiPanels)
        {
            if(panel != null)
                panel.SetActive(false);
        }
    }
    private void StartGameGUI()
    {
        ResetGUI();
        guiPanels[(int)Global.guiState].SetActive(true);
        Time.timeScale = 1;        

    }
    private void MainMenuGUI()
    {
        ResetGUI();
        guiPanels[(int)Global.guiState].SetActive(true);
    }
    private void ShopGUI()
    {
        guiPanels[(int)Global.guiState].SetActive(true);
    }
    private void AchievementsGUI()
    {
        
    }
    private void AchievementPanelGUI()
    {
        
    }
    private void SettingsGUI()
    {
        
    }
    private void SettingsAboutGUI()
    {
        
    }
    private void PauseGUI()
    {
        ResetGUI();
        guiPanels[(int)Global.guiState].SetActive(true);
        Time.timeScale = 0;        
    }
    private void InventoryGUI()
    {
        
    }
    private void ConfirmationDialogGUI()
    {
        
    }
    private void ResultsGUI()
    {
        ResetGUI();
        guiPanels[(int)Global.guiState].SetActive(true);
        StartCoroutine(Counter(5, 0, Global.score, txt_resultScore));
    }
    private void LoadingGUI()
    {

    }

    private void RevivePromptGUI()
    {
        btn_WatchAdToRevive.interactable = false;
        guiPanels[(int)Global.guiState].SetActive(true);
        Global.adManager.LoadAD(btn_WatchAdToRevive);
        Global.adRewardType = ADREWARDTYPE.revive;
    }



    private IEnumerator Counter(float dur, int min, int max, Text text)
    {
        float rate = (max - min) / (dur * 10);
        float timer = 0;
        float progress = min;
        while(timer < dur &&  Mathf.Floor(progress) < max)
        {
            progress+=rate;
            timer+=0.01f;
            text.text = Mathf.Floor(progress) + "";
            yield return new WaitForSeconds(0.01f);
        }
    }




    
    
}
