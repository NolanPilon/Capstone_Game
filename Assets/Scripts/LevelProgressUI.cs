using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressUI : MonoBehaviour
{
    [Header("UI references :")]
    [SerializeField] private Image uiFillImage;
    [SerializeField] private GameObject BossHP;

    [Header("Player & Endline references :")]
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Transform[] triggerpoints;     //Create an array so that it can be used in other game scenes
    [SerializeField] private Transform endLineTransform;


    private Vector3[] triggerpointPs;   //Create an array so that it can be used in other game scenes
    private Vector3 endLinePosition;

    private int PQty;    // if triggerPoints are 3, distance Qty will be 4 (0, 1, 2, 3)
    private float[] dist;       //Each distance value (player~triggerpoint, trigger[i-1]~trigger[i])
    private float fullDistance; //Full distance of this game scene

    public bool[] checkTriggered;  //Make sure where the player is

    private void Awake()
    {
        PQty = triggerpoints.Length;
        triggerpointPs = new Vector3[PQty];
        checkTriggered = new bool[PQty];

        //Check the player respawn from checkpoint
        if (GameManager.progressPoint != 0)
        {
            for (int i = 0; i < GameManager.progressPoint; i++)
            {
                triggerpoints[i].GetComponent<TriggerPoint>().isTriggered = true;
            }
        }
    }

    private void Start()
    {
        //assigns triggerpointPs & endlinePosition
        for (int i = 0; i < PQty; i++)
        {
            triggerpointPs[i] = triggerpoints[i].position;
        }
        endLinePosition = endLineTransform.position;

        initialized();
    }

    private void initialized()
    {
        dist = new float[PQty + 1];
        dist[0] = Vector2.Distance(new Vector2(0,0), triggerpointPs[0]);

        //initialize dist list checkpoints
        for (int i = 1; i < PQty; i++)
        {
            dist[i] = Vector2.Distance(triggerpointPs[i - 1], triggerpointPs[i]);
            checkTriggered[i] = triggerpoints[i].GetComponent<TriggerPoint>().isTriggered;
        }
        dist[PQty] = Vector2.Distance(triggerpointPs[PQty-1], endLinePosition);

        BossHP.SetActive(false);

        for (int i = 0; i <= PQty; i++)
        {
            fullDistance += dist[i];
        }
    }

    // get the distance value to apply to progress value
    private float GetDistance()
    {
        float fulldist = 0;
        checkTriggered[0] = triggerpoints[0].GetComponent<TriggerPoint>().isTriggered;

        if (!checkTriggered[0])
        { 
            dist[0] = Vector2.Distance(playerTransform.position, triggerpointPs[0]);
            fulldist += dist[0];
        }
        else
        { 
            dist[0] = 0; 
        }
        
        for (int i = 1; i < triggerpoints.Length; i++)
        {          
            dist[i] = CalDistance(triggerpointPs[i], dist[i], checkTriggered[i-1], checkTriggered[i]);
            fulldist += dist[i];
            checkTriggered[i] = triggerpoints[i].GetComponent<TriggerPoint>().isTriggered;
        }

        if (fulldist == 0)
        {
            fulldist = Vector2.Distance(playerTransform.position, endLinePosition);
        }
        else
        {
            fulldist += dist[PQty];
        }

        return fulldist;
    }

    //Calculate distance
    private float CalDistance(Vector2 triggerpointPs, float dist, bool checkpoint, bool checkpoint2)
    {
        if (!checkpoint) return dist;

        if (checkpoint && !checkpoint2)
        {
            dist = Vector2.Distance(playerTransform.position, triggerpointPs);
        }
        else
        {
            dist = 0;
        }

        return dist;
    }

    private void UpdateProgressFill(float value)
    {
        uiFillImage.fillAmount = value;
    }

    private void Update()
    {
        if (GameManager.playerHP == 0)
        {
            initialized();
        }

        if (playerTransform.position.x > endLinePosition.x)
        {
           this.gameObject.SetActive(false);
           BossHP.SetActive(true);
           return;
        }
            
        float newDistance = GetDistance();
        float progressValue = Mathf.InverseLerp(fullDistance, 0f, newDistance);
        //InverseLerp(min,max,v) always returns a value bet 0 & 1
        // v is bet min & max

        UpdateProgressFill(progressValue);
    }
}
 