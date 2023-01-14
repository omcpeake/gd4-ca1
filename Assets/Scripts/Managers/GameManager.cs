using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using static UnityEditor.PlayerSettings;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }


    public GameState gameState;
    public CamState camState;
    public static event Action<GameState> GameStateChanged;

    public GameObject player;

    Vector3 savedPosition;
    Quaternion savedRotation;

    int BossesRemaining;

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
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        CamManager.instance.SetMainCam();
        UpdateGameState(GameState.OVERWORLD);
        player.GetComponent<Stats>().ResetHP();
        BossesRemaining = 3;
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
            UIManager.instance.GameWon();
            StartCoroutine(ReloadSceneAfterSeconds(10f));
        }
        else if (gameState == GameState.LOSE)
        {           
            UIManager.instance.GameLost();
            StartCoroutine(ReloadSceneAfterSeconds(10f));
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

    public void SaveCurrentPositionRotation()
    {
        //saves player position and rotation to be returned to later
        savedPosition = player.transform.position;
        savedRotation = player.transform.rotation;
    }
    public void ReturnToSavedPosition()
    {
        //returns player to location saved from method above
        player.GetComponent<NavMeshAgent>().Warp(savedPosition);
        player.transform.rotation = savedRotation;
    }
    public void MovePlayerPosition(Vector3 pos, Quaternion rot)
    {
        player.GetComponent<NavMeshAgent>().Warp(pos);
        player.transform.rotation = rot;
    }

    IEnumerator ReloadSceneAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        player.GetComponent<Stats>().ResetHP();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BossSlain()
    {
        BossesRemaining--;
        if(BossesRemaining == 0)
        {
            UpdateGameState(GameState.WIN);
        }
    }

}
