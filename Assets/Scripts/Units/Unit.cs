using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Unit", menuName = "Assets/Unit")]

public class Unit : ScriptableObject
{
    public string unitName;
    public int unitLevel;

    public bool friendly;
    public bool boss;

    public int maxHP;
    public int currentHP;

    public int attack;
    public int defense;
    public int speed;



}
