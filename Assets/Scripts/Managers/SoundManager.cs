using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    public AudioSource MusicSource;
    public AudioSource EffectsSource;
    public AudioClip MusicClip;

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
        MusicSource.clip = MusicClip;
        MusicSource.Play();

    }


}
