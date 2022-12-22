using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }


    public GameState gameState;
    public CamState camState;
    public static event Action<GameState> GameStateChanged;

    public GameObject player;

    Vector3 savedPosition;


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
        CamManager.instance.SetMainCam();
        UpdateGameState(GameState.OVERWORLD);
    }

    private void Update()
    {
        if(gameState == GameState.MENU)
        {

        }
        else if(gameState == GameState.OVERWORLD)
        {
            if(CamManager.instance.getCurrentCam()!=CamState.MAINCAM)
            {
                CamManager.instance.SetMainCam();
            }
        }
        else if (gameState == GameState.BATTLE)
        {
            if (CamManager.instance.getCurrentCam() != CamState.BATTLECAM)
            {
                CamManager.instance.SetBattleCam();
                BattleManager.instance.StartBattle();
            }
        }
        else if (gameState == GameState.WIN)
        {

        }
        else if (gameState == GameState.LOSE)
        {

        }

    }

    //https://www.youtube.com/watch?v=4I0vonyqMi8&ab_channel=Tarodev Game state tutorial
    public void UpdateGameState(GameState newState)
    {
        gameState = newState;

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

    public void SaveCurrentPosition()
    {
        savedPosition = player.transform.position;
    }
    public void ReturnToSavedPosition()
    {
        player.GetComponent<NavMeshAgent>().Warp(savedPosition);
    }
    public void MovePlayerPosition(Vector3 pos)
    {
        player.GetComponent<NavMeshAgent>().Warp(pos);
        
    }

}
