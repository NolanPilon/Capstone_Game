using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditorInternal.ReorderableList;

public class BossAI3 : MonoBehaviour
{
    private static BossAI3 instance;
    public static BossAI3 Instance => instance;

    public Camera MainCam;
    public GameObject target;
    public PlayerCombatFunctions playerStat;        //for take damage function

    public GameObject ParryBullet;
    public GameObject ParryObject;
    public GameObject BossBody;
    public GameObject BossHandR;
    public GameObject BossHandL;
    public GameObject BossEye;
    public Transform targetSpawner;
    public Rigidbody2D rb;
    public float velocity = 5.0f;

    public float BossHealth;
    [SerializeField] private Image healthbar;       //boss healthbar
    float healthValue;                              //boss health value


    private Vector3 InitialEyePos;  //for go back eye function
    private Vector3 InitialHandRPos;    // trigger to change velocity of left hand
    private Vector3 InitialHandLPos;    // trigger to change velocity of right hand
    public Transform EyeAttackPos;  //Position of eye in Attack Phase
    private Vector3 InitialBossPos; //for go back boss function

    // List of phase
    public enum Phases
    {
        Phase1,
        Phase2,
        Phase3,
        AttackPhase,

        Default
    }
    public Phases phase = Phases.Default;

    private BossAI3()
    {
        instance = this;
    }

    private void Start()
    {
        BossHealth = 3;
        playerStat = target.GetComponent<PlayerCombatFunctions>();
        InitialEyePos = BossEye.transform.position;
        InitialBossPos = this.transform.position;
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

    public void startPhase()
    {
        switch(phase)
        {
            case Phases.Phase1:
                GetComponent<BossAI3Shooting>().shootBullet();
                break;
            case Phases.Phase2:
                EyeGoBack();
                GetComponent<BossAI3Shooting>().shootBullet();
                BossMove();
                break;
            case Phases.Phase3:
                EyeGoBack();
                GetComponent<BossAI3Shooting>().shootBullet();
                InitialHandLPos = BossHandL.transform.position;
                InitialHandRPos = BossHandR.transform.position;
                HandCross();
                break;
            case Phases.AttackPhase:
                BossGoBack();
                rbZero();
                EyeMove();
                GetComponent<BossAI3Shooting>().ShootBulletEye();
                break;
            default:
                break;
        }
    }

    public bool TargetInRange(GameObject boss)
    {
        if (Vector2.Distance(boss.transform.position, target.transform.position) < 10) //Distance bet player and boss is less than 10
            return true;
        return false;
    }

    public void EyeMove()
    {
        BossEye.transform.position = EyeAttackPos.position;
    }

    public void EyeGoBack()
    {
        BossEye.transform.position = InitialEyePos;
    }

    public void BossGoBack()
    {
        this.transform.position = InitialBossPos;
    }

    public void BossMove()
    {
        rb.velocity = new Vector2(velocity, 0);
    }

    public void rbZero()
    {
        rb.gravityScale = 0;
        rb.velocity = Vector2.zero;
    }

    public void HandGoBack()
    {
        if (phase != Phases.Phase3)
        {
            return;
        }

        if (BossHandR.transform.position.x == InitialHandRPos.x && BossHandL.transform.position.x == InitialHandLPos.x)
        {
            Invoke("HandCross", 0);
            return;
        }

        BossHandR.transform.position = Vector2.MoveTowards(BossHandR.transform.position, InitialHandRPos, 2 * Time.deltaTime);
        BossHandL.transform.position = Vector2.MoveTowards(BossHandL.transform.position, InitialHandLPos, 2 * Time.deltaTime);

        Invoke("HandGoBack", 0);
    }

    public void HandCross()
    {
        if (phase != Phases.Phase3)
        {
            return;
        }

        if (BossHandR.transform.position.x == InitialHandLPos.x && BossHandL.transform.position.x == InitialHandRPos.x)
        {
            Invoke("HandGoBack", 0);
            return;
        }

        BossHandR.transform.position = Vector2.MoveTowards(BossHandR.transform.position, InitialHandLPos, 2 * Time.deltaTime);
        BossHandL.transform.position = Vector2.MoveTowards(BossHandL.transform.position, InitialHandRPos, 2 * Time.deltaTime);

        Invoke("HandCross", 0);
    }
}
