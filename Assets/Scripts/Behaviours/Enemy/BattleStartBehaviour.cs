using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleStartBehaviour : MonoBehaviour
{
    public GameObject obj;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            GameManager.instance.UpdateGameState(GameState.BATTLE);
            Destroy(obj);
        }
    }
}
