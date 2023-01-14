using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleStartBehaviour : MonoBehaviour
{ 
    private void OnTriggerEnter(Collider other)
    {
        //start battle when collide with player and delete this object so a second battle cant be started
        if (other.gameObject.name == "Player")
        {
            GameManager.instance.UpdateGameState(GameState.BATTLE);
            Destroy(gameObject);
        }
    }
}
