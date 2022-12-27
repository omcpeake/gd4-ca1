using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking.Types;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance { get; private set; }
    // Start is called before the first frame update
    public GameObject allySlot1;
    public GameObject player;

    public GameObject enemySlot1;

    private GameObject enemy1;
    
    

    BattleState state;

    Queue<GameObject> turnOrder = new Queue<GameObject>();
    int turnCount;

    int totalUnitCount;

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
        state = BattleState.START;
        //create list of units, will be sorted by speed later
        List<GameObject> unitList = new List<GameObject>();
        //spawn player
        GameManager.instance.SaveCurrentPositionRotation();
        GameManager.instance.MovePlayerPosition(allySlot1.transform.position, allySlot1.transform.rotation);
        totalUnitCount++;
        unitList.Add(player);

        //spawn enemies
        enemy1 = SpawnManager.instance.SpawnHuman(enemySlot1.transform.position, enemySlot1.transform.rotation);
        totalUnitCount++;
        unitList.Add(enemy1);

        SortBySpeed(unitList);
        GetNextTurn();
    }

    private void GetNextTurn()
    {
        if(turnOrder.Peek().GetComponent<Stats>().IsFriendly()==true)
        {
            state = BattleState.PLAYERTURN;
        }
        else
        {
            state = BattleState.ENEMYTURN;
        }
    }

    private void SortBySpeed(List<GameObject> unitList)
    {
        int highestSpeed=0;
        int index = 0;
        GameObject fastestUnit = new ();
        
        while(unitList.Count > 0)
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i].GetComponent<Stats>().GetSpeed() >= highestSpeed)
                {
                    //TODO - speed ties
                    highestSpeed = unitList[i].GetComponent<Stats>().GetSpeed();
                    fastestUnit=unitList[i];
                    index = i;
                    
                }
            }
            unitList.Remove(unitList[index]);
            turnOrder.Enqueue(fastestUnit);
            
        }
    }

    private void EndBattle()
    {
        GameManager.instance.UpdateGameState(GameState.OVERWORLD);
        GameManager.instance.ReturnToSavedPosition();
    }
}
