using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePlayPanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private Text levelText;

    void Start()
    {
        gamePlayPanel.SetActive(false);
        progressBar.value = 0;
        levelText.text = "Level " + (PlayerPrefs.GetInt(PlayerPrefsTag.LEVEL) + 1).ToString();
    }

    void Update()
    {
        UpdateProgressBar();
    }

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameState.Game);
        menuPanel.SetActive(false);

        progressBar.value = 0;
        gamePlayPanel.SetActive(true);
    }

    private void UpdateProgressBar()
    {
        if (!GameManager.instance.IsGameState()) return;

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishLinePosZ();
        progressBar.value = progress;
    }
}
