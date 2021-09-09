using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UiController : MonoBehaviour
{
    public static UiController instance { get; private set; }

    [Header("Text")]
    public TMP_Text levelScore;
    [Header("Buttons")]
    public Button startGameButton;
    public Button exitGameButton;
    public Button resumeGameButton;
    public Button exit2GameButton;
    public Button menuGameButton;
    [Header("Panels")]
    public GameObject menuPanel;
    public GameObject pausePanel;

    private void Awake()
    {
        instance = this;
    }


    public void StartGameHandler()
    {
        menuPanel.SetActive(false);
        menuGameButton.gameObject.SetActive(true);
    }

    public void ExitGameHandler()
    {
        Application.Quit();
    }

    public void ResumeGameHandler()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void pauseMenuHandler()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0;
    }

}
