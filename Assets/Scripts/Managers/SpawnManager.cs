using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }

    public GameObject human;

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
    }

    public GameObject SpawnHuman(Vector3 pos, Quaternion rot)
    {
        // Instantiate at position and rotation.
        GameObject newGO = Instantiate(human, pos, rot);
        return newGO;
    }
}
