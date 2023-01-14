using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }

    public GameObject human;
    public GameObject wizard;

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
        DontDestroyOnLoad(gameObject);
    }

    public GameObject SpawnHuman(Vector3 pos, Quaternion rot)
    {
        // Instantiate at position and rotation.
        GameObject newGO = Instantiate(human, pos, rot);
        return newGO;
    }

    public GameObject SpawnWizard(Vector3 pos, Quaternion rot)
    {
        // Instantiate at position and rotation.
        GameObject newGO = Instantiate(wizard, pos, rot);
        return newGO;
    }
}
