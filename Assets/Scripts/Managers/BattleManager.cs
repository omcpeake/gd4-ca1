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

    private GameObject enemy;

    private Unit playerUnit;
    private Unit enemyUnit;

    BattleState state;

    Queue turnOrder = new Queue();
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
        List<Unit> unitList = new List<Unit>();
        //spawn player
        GameManager.instance.SaveCurrentPositionRotation();
        GameManager.instance.MovePlayerPosition(allySlot1.transform.position, allySlot1.transform.rotation);
        playerUnit = GameManager.instance.GetComponent<Unit>();
        totalUnitCount++;
        unitList.Add(playerUnit);

        //spawn enemies
        enemy = SpawnManager.instance.SpawnHuman(enemySlot1.transform.position, enemySlot1.transform.rotation);
        enemyUnit = enemy.GetComponent<Unit>();
        totalUnitCount++;
        unitList.Add(enemyUnit);

        SortBySpeed(unitList);

    }

    private void SortBySpeed(List<Unit> unitList)
    {
        int highestSpeed=0;
        Unit fastestUnit = new Unit();
        while(unitList.Count > 0)
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i].speed >= highestSpeed)
                {
                    //TODO - speed ties
                    highestSpeed = unitList[i].speed;
                    fastestUnit = unitList[i];
                }
            }
            turnOrder.Enqueue(fastestUnit);
            unitList.Remove(fastestUnit);
        }

        
        
    }

    private void EndBattle()
    {
        GameManager.instance.UpdateGameState(GameState.OVERWORLD);
        GameManager.instance.ReturnToSavedPosition();
    }
}
