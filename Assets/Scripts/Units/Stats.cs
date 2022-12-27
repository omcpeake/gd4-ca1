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
    public void Defend(int attackVal)
    {
        currentHP -= attackVal*(100/(100+unitStats.defense));
        if(currentHP <= 0)
        {
            isDead = true; 
        }
    }

    public bool GetIsDead()
    {
        return isDead;
    }

    public bool IsFriendly()
    {
        return unitStats.friendly;
    }


}
