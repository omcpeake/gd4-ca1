using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    public AudioSource MusicSource;
    public AudioSource EffectsSource;
    [Space]
    public AudioClip OverworldMusic;
    public AudioClip CombatMusic;
    [Space]
    public AudioClip Sword;
    public AudioClip WarHorn;
    public AudioClip HumanDeath;

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

        MusicSource.clip = OverworldMusic;
        MusicSource.Play();
    }


    public void SwitchMusic()
    {
        if (MusicSource.clip == OverworldMusic)
        {
            MusicSource.clip = CombatMusic;
            MusicSource.Play();
        }
        else if(MusicSource.clip == CombatMusic)
        {
            MusicSource.clip = OverworldMusic;
            MusicSource.Play();
        }
            
    }

    public void PlaySwordEffect()
    {
        EffectsSource.clip = Sword;
        EffectsSource.Play();
    }

    public void PlayWarHornEffect()
    {
        EffectsSource.clip = WarHorn;
        EffectsSource.Play();
    }

    public void PlayHumanDeathEffect()
    {
        EffectsSource.clip = HumanDeath;
        EffectsSource.Play();
    }


}
