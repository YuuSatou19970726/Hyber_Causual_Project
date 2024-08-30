using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { Menu, Game, LevelComplete, GameOver }

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private GameState gameState;
    public static Action<GameState> onGameStateChanged;

    // Start is called before the first frame update
    void Start()
    {
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetGameState(GameState _gameState)
    {
        this.gameState = _gameState;
        onGameStateChanged?.Invoke(_gameState);
    }

    public bool IsGameState()
    {
        return gameState == GameState.Game;
    }
}
