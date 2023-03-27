using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI3 : MonoBehaviour
{
    private static BossAI3 instance;
    public static BossAI3 Instance => instance;

    public GameObject target;
    public GameObject ParryBullet;
    public GameObject BossBody;
    public GameObject BossHandR;
    public GameObject BossHandL;
    public GameObject BossEye;
    public Camera MainCam;
    public Transform targetSpawner;

    public float BossHealth;
    [SerializeField] private Image healthbar;       //boss healthbar
    float healthValue;                              //boss health value

    private PlayerCombatFunctions playerStat;               //for take damage function

    private BossAI3()
    {
        instance = this;
    }

    private void Start()
    {
        BossHealth = 3;
        playerStat = target.GetComponent<PlayerCombatFunctions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (BossHandL.activeSelf && BossHandR.activeSelf)
        {
            MainCam.transform.position = new Vector3(MainCam.transform.position.x, target.transform.position.y + 3.0f, -10.0f);
        }

        healthValue = (float)(BossHealth / 3f);
        healthbar.fillAmount = healthValue;
    }
}
