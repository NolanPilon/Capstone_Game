using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    // Start is called before the first frame update
    private static BackgroundMusic musicManager;
    public static BackgroundMusic Instance { get { return musicManager; } }
    void Awake()
    {
        if (musicManager != null && musicManager != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            musicManager = this;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
