using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableData : MonoBehaviour
{
    private static CollectableData collectableData;
    
    public static CollectableData Instance { get { return collectableData; } }

    public static int totalCollectables;
    public int collectableCount;
    
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //Show in inspector
        collectableCount = totalCollectables;
    }
}
