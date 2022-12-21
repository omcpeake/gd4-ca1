using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }


    public GameState state;
    public static event Action<GameState> GameStateChanged;

    // Start is called before the first frame update
    void Awake()
    {
        //creating singleton
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    void Start()
    {
        UpdateGameState(GameState.OVERWORLD);
    }

    private void Update()
    {
        
    }

    //https://www.youtube.com/watch?v=4I0vonyqMi8&ab_channel=Tarodev Game state tutorial
    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.MENU:
                break;
            case GameState.OVERWORLD:
                break;
            case GameState.BATTLE:
                break;
            case GameState.WIN:
                break;
            case GameState.LOSE:
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
        }

        GameStateChanged?.Invoke(newState);  //checks if anything is subscribed to this before calling
    }

}
