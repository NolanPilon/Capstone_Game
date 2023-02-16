using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance = null;
    private AudioSource soundEffectAudio;
    public AudioClip jump;
    public AudioClip hit;
    public AudioClip hurt;
    public AudioClip qTE;
    public AudioClip enemyAttack;
    public AudioClip enemyDeath;
    public AudioClip Menu;

    //Start is called before the first frame update
    void Start()
    {
       if (Instance == null) 
        {
          Instance = this;
        }
        else if (Instance !=this) 
        {
          Destroy(gameObject);
        }
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            if (source.clip == null)
            {
                soundEffectAudio = source;
            }
        }
    }

    public void PlayOneShot(AudioClip clip)
    {
        soundEffectAudio.PlayOneShot(clip);
    }

}
