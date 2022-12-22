using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartBehaviour : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            GameManager.instance.UpdateGameState(GameState.BATTLE);
        }
    }
}
