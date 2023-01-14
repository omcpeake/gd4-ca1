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
            BattleManager.instance.GetUnitID(gameObject.GetComponent<Stats>().GetID());
            GameManager.instance.UpdateGameState(GameState.BATTLE);
            //wait to destroy otherwise itll crash
            StartCoroutine(DestroyAfterSeconds(1f));
        }
    }


    IEnumerator DestroyAfterSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(gameObject);
    }    
}
