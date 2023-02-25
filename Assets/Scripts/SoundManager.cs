using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    private AudioSource[] soundEffectAudio;
    private AudioSource jump;
    private AudioSource hit;
    private AudioSource hurt;
    private AudioSource qTE;  //
    private AudioSource enemyAttack;
    private AudioSource enemyDeath;
    private AudioSource Menu;

    //Start is called before the first frame update
    void Start()
    {
        soundEffectAudio= GetComponents<AudioSource>();

        jump = soundEffectAudio[0];
        hurt = soundEffectAudio[1];
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

    public void PlayEAttack()
    {
        enemyAttack.Play();
    }

    public void PlayEDeath()
    {
        enemyDeath.Play();
    }

    public void PlayMenu()
    {
        Menu.Play();
    }

}
