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
    [SerializeField] private Transform triggerpoint1;
    [SerializeField] private Transform triggerpoint2;
    [SerializeField] private Transform endLineTransform;

    private Vector3 triggerpoint1Ps;
    private Vector3 triggerpoint2Ps;
    private Vector3 endLinePosition;

    private float dist1;
    private float dist2;
    private float dist3;
    private float fullDistance;

    private bool checkpoint1 = false;
    private bool checkpoint2 = true;

    private void Start()
    {
        triggerpoint1Ps = triggerpoint1.position;
        triggerpoint2Ps = triggerpoint2.position;
        endLinePosition = endLineTransform.position;

        dist1 = Vector2.Distance(playerTransform.position, triggerpoint1Ps);
        dist2 = Vector2.Distance(triggerpoint1Ps, triggerpoint2Ps);
        dist3 = Vector2.Distance(triggerpoint2Ps, endLinePosition);

        fullDistance = GetDistance();

        BossHP.SetActive(false);
    }

    private float GetDistance()
    {
        if (!checkpoint1)
        {
            if (playerTransform.position.y > triggerpoint1Ps.y)
            {
                dist1 = Vector2.Distance(playerTransform.position, triggerpoint1Ps);
            }
            else
            {
                dist1 = 0;
                checkpoint1 = true;
                checkpoint2 = false;
            }
        }
        else if (!checkpoint2)
        {
            if (playerTransform.position.y > triggerpoint2Ps.y)
            {
                dist2 = Vector2.Distance(triggerpoint2Ps, playerTransform.position);
            }
            else
            {
                dist2 = 0;
                checkpoint2 = true;
            }
        }
        else if (checkpoint1 && checkpoint2)
        {
            dist3 = Vector2.Distance(playerTransform.position, endLinePosition);
        }

        return dist1 + dist2 + dist3;
    }

    private void UpdateProgressFill(float value)
    {
        uiFillImage.fillAmount = value;
    }

    private void Update()
    {
        if (GameManager.playerHP == 0)
        {
            checkpoint1 = false;
            checkpoint2 = true;
            BossHP.SetActive(false);

            dist1 = Vector2.Distance(playerTransform.position, triggerpoint1Ps);
            dist2 = Vector2.Distance(triggerpoint1Ps, triggerpoint2Ps);
            dist3 = Vector2.Distance(triggerpoint2Ps, endLinePosition);
        }

        if (playerTransform.position.y < endLinePosition.y)
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
 