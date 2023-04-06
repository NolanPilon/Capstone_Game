//Audio from soundsnap.com

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    private static SoundManager soundManager;
    private AudioSource[] soundEffectAudio;
    //Player Noises
    private AudioSource jump;
    private AudioSource hit;
    private AudioSource hurt;
    private AudioSource qTE;

    //Mobs
    private AudioSource mobDeath;

    //Boss1
    private AudioSource BossHit;
    private AudioSource BossDeath;
    private AudioSource BossAt2;
    private AudioSource BossBounce;

    //Level1Obstacles
    private AudioSource Icicle;

    private AudioSource Menu;

    private int rando;

    public static SoundManager Instance { get { return soundManager; } }
    void Awake()
    {
        if (soundManager != null && soundManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            soundManager = this;
        }
    }

    void Start()
    {
        soundEffectAudio= GetComponents<AudioSource>();

        jump = soundEffectAudio[0];
        hurt = soundEffectAudio[1];
        mobDeath = soundEffectAudio[2];
        BossHit = soundEffectAudio[3];
        BossDeath = soundEffectAudio[4];
        BossAt2 = soundEffectAudio[5];
        BossBounce = soundEffectAudio[6];
        Icicle = soundEffectAudio[7];
        //hit = soundEffectAudio[2];
        //qTE = soundEffectAudio[3];
        //enemyAttack = soundEffectAudio[4];
        //enemyDeath = soundEffectAudio[5];
        //Menu = soundEffectAudio[6];
    }

    public void PlayJump()
    {
        jump.Play();
    }

    public void PlayHit()
    {
      hit.Play();
    }

    public void PlayHurt()
    {
        hurt.Play();
    }

    public void PlayQTE()
    {
        qTE.Play();
    }

    public void PlayBossHit()
    {
        BossHit.Play();
    }

    public void PlayBossDeath()
    {
        BossDeath.Play();
    }

    public void PlayBossAt2()
    {
        BossAt2.Play();
    }

    public void PlayBossBounce()
    {
            BossBounce.Play();
    }

    public void Playicicle()
    {
        Icicle.Play();
    }

    public void PlayMenu()
    {
        Menu.Play();
    }

    ///////////////////////////////////
    public void mobDeathNoise(AudioClip DeathNoise)
    {
        mobDeath.clip = DeathNoise;
        mobDeath.Play();
    }
}
