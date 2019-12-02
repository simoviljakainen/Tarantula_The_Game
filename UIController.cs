using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;
using TMPro;

public class UIController : MonoBehaviour
{
    public GameObject hiScorePanel;
    public GameObject respawnPanel;
    public GameObject pausePanel;
    public GameObject service;
    public TMP_Text panelText;
    public Text uiScore;
    public Text uiKillcount;
    public static UIController uiInstance;

    void Awake()
    {
        uiInstance = this;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Restart") && respawnPanel.activeInHierarchy)
        {
            StartGame();
        }
        else if (Input.GetButtonDown("Cancel"))
        {
            LoadMainMenu();

        }else if (Input.GetButtonDown("Pause") && GameService.playing)
        {
            TogglePause();
        }
    }

    public void SetScore(float score)
    {
        uiScore.text = $"Distance: {(int)score}";
    }

    public void SetKillCount(int hits)
    {
        uiKillcount.text = $"Bug 'fixes': {hits}";
    }

    public void QuitGame()
    {

#if UNITY_EDITOR
       
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif

    }

    public void PlayButtonSound()
    {
        SoundController.sc.PlaySound("UI_Click", new Vector3(0f,0f,0f), true);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game");
    }

    public void LoadOptions()
    {
        SceneManager.LoadScene("Options");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
        GameService.playing = false;
    }

    public void LoadHiScores()
    {
        SceneManager.LoadScene("HiScores");
    }

    public void PlayerDeath()
    {
        GameService.playing = false;

        if (HiScoreUtilities.IsHiScore(GameService.score))
        {
            hiScorePanel.SetActive(true);
        }
        else
        {
            ToggleRespawnPanel();
        }
    }

    public void CheckUsername()
    {
        string input = GetComponentInChildren<InputField>().text;
        Regex alnum = new Regex("^[a-zA-Z0-9]+$");

        if (!alnum.IsMatch(input))
        {
            panelText.text = $"<color=\"red\">Username must be alphanumeric.</color>";
        }
        else if(input.Length > 30)
        {
            panelText.text = $"<color=\"red\">Username must be under 30 chars.</color>";
        }
        else if(!HiScoreUtilities.IsUsernameAvailable(input))
        {
            panelText.text = $"<color=\"red\">Username is already taken.</color>";
        }
        else
        {
            HiScoreUtilities.AddIntoHiScores(input, GameService.score);
            SceneManager.LoadScene("HiScores");
        }
    }
    private void OnApplicationQuit()
    {
        HiScoreStorage.GetInstance().SaveHiScores();
    }

    private void ToggleRespawnPanel()
    {
        respawnPanel.SetActive(!respawnPanel.activeInHierarchy);
    }

    public void TogglePause()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        
    }
}
