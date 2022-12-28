using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Unit unitStats;
    private int currentHP;
    private bool isDead;

    private void Awake()
    {
        currentHP = unitStats.maxHP;
        isDead = false;
    }


    public int GetSpeed()
    {
        return unitStats.speed;
    }

    public int Attack()
    {
        return unitStats.attack;
    }
    //damage formula = attack*(100/(100+defense))
    public bool Defend(int attackVal)
    {
        //currentHP -= attackVal*(100/(100+unitStats.defense));
        currentHP -= attackVal;
        Debug.Log(currentHP);
        if(currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }
    public void Die()
    {
        isDead = true;
    }

    public bool IsFriendly()
    {
        return unitStats.friendly;
    }


}
