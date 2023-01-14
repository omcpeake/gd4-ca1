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

    int unitID;

    bool BossBattle;

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

    public void GetUnitID(int id)
    {
        unitID = id;
    }

    public void StartBattle()
    {
        //reset enemy
        enemy1 = null;
        state = BattleState.START;
        //create list of units, will be sorted by speed in placed into a queue later
        List<GameObject> unitList = new List<GameObject>();
        //spawn player
        GameManager.instance.SaveCurrentPositionRotation();
        GameManager.instance.MovePlayerPosition(allySlot1.transform.position, allySlot1.transform.rotation);
        allyUnitCount++;
        unitList.Add(player);

        //spawn enemies
        if(unitID == 2)
        {
            enemy1 = SpawnManager.instance.SpawnHuman(enemySlot1.transform.position, enemySlot1.transform.rotation);
        }
        else if (unitID == 3)
        {
            enemy1 = SpawnManager.instance.SpawnWizard(enemySlot1.transform.position, enemySlot1.transform.rotation);
        }
        else if (unitID == 4)
        {
            enemy1 = SpawnManager.instance.SpawnHero(enemySlot1.transform.position, enemySlot1.transform.rotation);
        }

        if(enemy1.GetComponent<Stats>().IsBoss()==true)
        {
            BossBattle = true;
        }
        else
        {
            BossBattle = false;
        }


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

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }


    IEnumerator PlayerAttack()
    {
        //can only attack enemies
        if(selectedUnit.GetComponent<Stats>().IsFriendly()==false)
        {
            //damage
            bool isDead = selectedUnit.GetComponent<Stats>().Defend(turnOrder.Peek().GetComponent<Stats>().Attack());
            SoundManager.instance.PlaySwordEffect();
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
    IEnumerator PlayerHeal()
    {
        //can only heal allies
        if (selectedUnit.GetComponent<Stats>().IsFriendly() == true)
        {
            selectedUnit.GetComponent<Stats>().Heal(player.GetComponent<Stats>().Attack());
            yield return new WaitForSeconds(2f);


            //remove from turn queue and add back onto the back
                GameObject temp = turnOrder.Peek();
                turnOrder.Dequeue();
                turnOrder.Enqueue(temp);
                GetNextTurn();
        }
    }
    IEnumerator EnemyTurn()
    {
        //TODO - Enemy can choose targets
        yield return new WaitForSeconds(1f);
        //damage
        bool isDead = player.GetComponent<Stats>().Defend(turnOrder.Peek().GetComponent<Stats>().Attack());
        SoundManager.instance.PlaySwordEffect();
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
            if (BossBattle==true)
            {
                GameManager.instance.BossSlain();

            }
            if(GameManager.instance.gameState!=GameState.WIN)
            {
                UIManager.instance.BattleWon();

                GameManager.instance.UpdateGameState(GameState.OVERWORLD);
                GameManager.instance.ReturnToSavedPosition();
            }
            SoundManager.instance.PlayWarHornEffect();
        }
        else if(state==BattleState.LOST)
        {
            GameManager.instance.UpdateGameState(GameState.LOSE);
        }
        
    }
}
