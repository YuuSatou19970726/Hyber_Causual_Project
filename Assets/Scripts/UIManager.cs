using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    public void PlayButtonPressed()
    {
        GameManager.instance.SetGameState(GameState.Game);
        menuPanel.SetActive(false);
    }
}
