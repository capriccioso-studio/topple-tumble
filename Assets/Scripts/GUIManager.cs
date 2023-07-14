using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIManager : MonoBehaviour
{
    [Header("GUI Panels")]
    public GameObject[] guiPanels;


    [Header("Result")]
    public TMP_Text txt_resultScore;

    [Header("Revive")]
    public TMP_Text txt_reviveCountdown;
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
            case GUISTATE.metaxar:
                MetaXarGUI(); break;
            case GUISTATE.metaxarclaimconfirmation:
                MetaXarClaimConfirmationGUI(); break;
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
        guiPanels[(int)Global.guiState].SetActive(true);
        Global.adRewardType = ADREWARDTYPE.revive;
        StartCoroutine(ReviveCountdown(5, txt_reviveCountdown));
    }

    private void MetaXarGUI()
    {
        ResetGUI();
        guiPanels[(int)Global.guiState].SetActive(true);
    }
    private void MetaXarClaimConfirmationGUI()
    {
        ResetGUI();
        guiPanels[(int)Global.guiState].SetActive(true);
    }

    private IEnumerator Counter(float dur, int min, int max, TMP_Text text)
    {
        float rate = (max - min) / (dur * 10);
        float timer = 0;
        float progress = min;
        while(timer < dur &&  Mathf.Floor(progress) < max )
        {
            progress+=rate;
            timer+=0.01f;
            text.text = Mathf.Floor(progress) + "";
            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator ReviveCountdown(float start, TMP_Text text)
    {
        float timer = start;

        while (timer > 0)
        {
            timer -= Time.deltaTime;
            text.text = Mathf.Ceil(timer) + "";
            yield return new WaitForEndOfFrame();
        }
        if(Global.guiState == GUISTATE.reviveprompt)
            ChangeGUI(11);
    }




    
    
}
