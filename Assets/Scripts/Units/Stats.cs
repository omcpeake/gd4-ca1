using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public Unit unitStats;
    private bool isDead;

    private void Awake()
    {
        isDead = false;
    }

    public int GetCurrentHP()
    {
        return unitStats.currentHP;
    }

    public int GetSpeed()
    {
        return unitStats.speed;
    }

    public int GetID()
    {
        return unitStats.unitID;
    }

    public int Attack()
    {
        return unitStats.attack;
    }

    public bool IsBoss()
    {
        return unitStats.boss;
    }
    //damage formula = attack*(100/(100+defense))
    public bool Defend(int attackVal)
    {
        //currentHP -= attackVal*(100/(100+unitStats.defense));
        unitStats.currentHP -= attackVal;
        if(unitStats.currentHP <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Heal(int healVal)
    {
        unitStats.currentHP += healVal;
    }


    public void ResetHP()
    {
        unitStats.currentHP = unitStats.maxHP;
    }

    public bool GetIsDead()
    {
        return isDead;
    }
    public void Die()
    {
        isDead = true;
        SoundManager.instance.PlayHumanDeathEffect();
        Destroy(gameObject);
    }

    public bool IsFriendly()
    {
        return unitStats.friendly;
    }


}
