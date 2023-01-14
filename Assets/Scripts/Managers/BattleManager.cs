using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking.Types;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance { get; private set; }
    [SerializeField]
    private GameObject allySlot1;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject enemySlot1;
    private GameObject enemy1;

    [SerializeField]
    private GameObject battleUI;


    private GameObject selectedUnit;


    BattleState state;

    Queue<GameObject> turnOrder = new Queue<GameObject>();
    int turnCount;

    int allyUnitCount;
    int enemyUnitCount;

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

        battleUI.SetActive(false);
    }


    public void StartBattle()
    {
        state = BattleState.START;
        //create list of units, will be sorted by speed in placed into a queue later
        List<GameObject> unitList = new List<GameObject>();
        //spawn player
        GameManager.instance.SaveCurrentPositionRotation();
        GameManager.instance.MovePlayerPosition(allySlot1.transform.position, allySlot1.transform.rotation);
        allyUnitCount++;
        unitList.Add(player);

        //spawn enemies
        enemy1 = SpawnManager.instance.SpawnHuman(enemySlot1.transform.position, enemySlot1.transform.rotation);

        unitList.Add(enemy1);
        enemyUnitCount++;
        selectedUnit = enemy1;

        SortBySpeed(unitList);

        SoundManager.instance.SwitchMusic();
        GetNextTurn();
    }

    private void GetNextTurn()
    {
        turnCount++;
        if(turnOrder.Peek().GetComponent<Stats>().IsFriendly()==true)
        {
            state = BattleState.PLAYERTURN;
            battleUI.SetActive(true);
        }
        else
        {
            state = BattleState.ENEMYTURN;
            battleUI.SetActive(false);
            StartCoroutine(EnemyTurn());
        }
    }

  
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack());
    }

    IEnumerator PlayerAttack()
    {
        //can only attack enemies
        if(selectedUnit.GetComponent<Stats>().IsFriendly()==false)
        {
            //damage
            bool isDead = selectedUnit.GetComponent<Stats>().Defend(turnOrder.Peek().GetComponent<Stats>().Attack());
            yield return new WaitForSeconds(2f);


            //check if dead
            if (isDead == true)
            {
                selectedUnit.GetComponent<Stats>().Die();
                enemyUnitCount--;
            }

            //change state
            if (enemyUnitCount == 0)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                //remove from turn queue and add back onto the back
                GameObject temp = turnOrder.Peek();
                turnOrder.Dequeue();
                turnOrder.Enqueue(temp);
                GetNextTurn();
            }
        }
        

        

    }

    IEnumerator EnemyTurn()
    {
        //TODO - Enemy can choose targets
        yield return new WaitForSeconds(1f);
        //damage
        bool isDead = player.GetComponent<Stats>().Defend(turnOrder.Peek().GetComponent<Stats>().Attack());
        yield return new WaitForSeconds(2f);

        //check if dead
        if (isDead == true)
        {
            player.GetComponent<Stats>().Die();
            allyUnitCount--;
        }

        //change state
        if (allyUnitCount == 0)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            //remove from turn queue and add back onto the back
            GameObject temp = turnOrder.Peek();
            turnOrder.Dequeue();
            turnOrder.Enqueue(temp);

            //if the next turn is a unit that has already died it will be null and needs to be removed
            if (turnOrder.Peek()==null)
            {
                turnOrder.Dequeue();
            }

            GetNextTurn();
        }

    }

    public void SelectUnit(SelectedUnit selected)
    {
        switch (selected)
        {
            case SelectedUnit.ALLY1:
                selectedUnit = player;
                break;
            case SelectedUnit.ENEMY1:
                selectedUnit = enemy1;
                break;


            default:
                throw new ArgumentOutOfRangeException(nameof(selected), selected, null);
        }
    }

    public int SelectedUnitHP()
    {
        return selectedUnit.GetComponent<Stats>().GetCurrentHP();
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
        turnOrder.Clear();
        battleUI.SetActive(false);
        SoundManager.instance.SwitchMusic();
        if (state==BattleState.WON)
        {
            UIManager.instance.BattleWon();
            GameManager.instance.UpdateGameState(GameState.OVERWORLD);
            GameManager.instance.ReturnToSavedPosition();
        }
        else if(state==BattleState.LOST)
        {
            GameManager.instance.UpdateGameState(GameState.LOSE);
        }
        
    }
}
