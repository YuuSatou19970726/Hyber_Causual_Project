using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;

    void Start()
    {
        DefaultUI();
        progressBar.value = 0;
        levelText.text = "Level " + (PlayerPrefs.GetInt(PlayerPrefsTag.LEVEL) + 1).ToString();

        GameManager.onGameStateChanged += GameStateChangeCallback;
    }

    void OnDestroy()
    {
        GameManager.onGameStateChanged -= GameStateChangeCallback;
    }

    void Update()
    {
        UpdateProgressBar();
    }

    private void DefaultUI()
    {
        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        levelCompletePanel.SetActive(false);
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameState.Game);
        menuPanel.SetActive(false);

        progressBar.value = 0;
        gamePlayPanel.SetActive(true);
    }

    public void RetryButtonPressed()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState()) return;

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishLinePosZ();
        progressBar.value = progress;
    }

    private void ShowGameOverPanel()
    {
        gamePlayPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    private void ShowLevelCompletePanel()
    {
        gamePlayPanel.SetActive(false);
        levelCompletePanel.SetActive(true);
    }

    // Callback
    private void GameStateChangeCallback(GameState gameState)
    {
        switch (gameState)
        {
            case GameState.GameOver:
                ShowGameOverPanel();
                break;
            case GameState.LevelComplete:
                ShowLevelCompletePanel();
                break;
        }
    }
}
