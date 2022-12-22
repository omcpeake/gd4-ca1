using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance { get; private set; }
    // Start is called before the first frame update
    public GameObject allySlot1;

    public GameObject enemySlot1;

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle()
    {
        GameManager.instance.SaveCurrentPosition();
        GameManager.instance.MovePlayerPosition(allySlot1.transform.position);
       // GameManager.instance.player.GetComponent<NavMeshAgent>().enabled= false;
        

    }

    private void EndBattle()
    {
        GameManager.instance.UpdateGameState(GameState.OVERWORLD);
        GameManager.instance.ReturnToSavedPosition();
    }
}
