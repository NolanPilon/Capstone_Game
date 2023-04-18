using UnityEngine;
using UnityEngine.UI;

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
    public GameObject LightOfSight;
    public Rigidbody2D rb;
    public float velocity = 5.0f;

    public float BossHealth;
    [SerializeField] private Image healthbar;       //boss healthbar
    float healthValue;                              //boss health value


    private Vector3 InitialEyePos;  //for go back eye function
    private Vector3 PreviousHandRPos;    // trigger to change velocity of left hand
    private Vector3 PreviousHandLPos;    // trigger to change velocity of right hand
    public Transform EyeAttackPos;  //Position of eye in Attack Phase

    private Vector2 InitialPos; //Spawn point

    // List of phase
    public enum Phases
    {
        Phase1,
        Phase2,
        Phase3,
        AttackPhase,
        Die,

        Default
    }
    public Phases phase = Phases.Default;

    private BossAI3()
    {
        instance = this;
    }

    private void Awake()
    {
        InitialPos = new Vector2 (872, 51);
    }

    private void Start()
    {
        playerStat = target.GetComponent<PlayerCombatFunctions>();
        initialization();
    }

    // Update is called once per frame
    void Update()
    {
        if (phase != Phases.Default)
        {
            CameraMove();
        }

        healthValue = (float)(BossHealth / 3f);
        healthbar.fillAmount = healthValue;
    }

    public void initialization()
    {
        this.transform.position = InitialPos;
        BossHandL.SetActive(false);
        BossHandR.SetActive(false);
        rb.gravityScale = 1.0f;
        BossHealth = 3;
        phase = Phases.Default;
    }

    public void CameraMove()
    {
        MainCam.transform.position = new Vector3(target.transform.position.x, target.transform.position.y + 7.0f, -10.0f);
    }

    public void startPhase()
    {
        switch(phase)
        {
            case Phases.Phase1:
                GetComponent<BossAI3Shooting>().shootBullet();
                MainCam.orthographicSize = 10.0f;
                break;
            case Phases.Phase2:
                EyeGoBack();
                GetComponent<BossAI3Shooting>().shootBullet();
                BossMove();
                break;
            case Phases.Phase3:
                EyeGoBack();
                GetComponent<BossAI3Shooting>().shootBullet();
                HandCross();
                break;
            case Phases.AttackPhase:
                InitialEyePos = BossEye.transform.position;
                PreviousHandLPos = BossHandL.transform.position;
                PreviousHandRPos = BossHandR.transform.position;
                rbZero();
                EyeMove();
                break;
            case Phases.Die:
                Die();
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

        GetComponent<BossAI3Shooting>().ShootBulletEye();
        LightOfSight.SetActive(true);
    }

    public void EyeGoBack()
    {
        BossEye.GetComponent<RectTransform>().transform.position = InitialEyePos;
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

        if (BossHandR.transform.position.x == PreviousHandRPos.x && BossHandL.transform.position.x == PreviousHandLPos.x)
        {
            Invoke("HandCross", 0);
            return;
        }

        BossHandR.transform.position = Vector2.MoveTowards(BossHandR.transform.position, PreviousHandRPos, 0.5f * Time.deltaTime);
        BossHandL.transform.position = Vector2.MoveTowards(BossHandL.transform.position, PreviousHandLPos, 0.5f * Time.deltaTime);

        Invoke("HandGoBack", 0);
    }

    public void HandCross()
    {
        if (phase != Phases.Phase3)
        {
            return;
        }

        if (BossHandR.transform.position.x == PreviousHandLPos.x && BossHandL.transform.position.x == PreviousHandRPos.x)
        {
            Invoke("HandGoBack", 0);
            return;
        }

        BossHandR.transform.position = Vector2.MoveTowards(BossHandR.transform.position, PreviousHandLPos, 0.5f * Time.deltaTime);
        BossHandL.transform.position = Vector2.MoveTowards(BossHandL.transform.position, PreviousHandRPos, 0.5f * Time.deltaTime);

        Invoke("HandCross", 0);
    }

    public void Die()
    {
        SoundManager.Instance.PlayBossDeath();
        Destroy(gameObject, 2);
        GameManager.Instance.BossDied = true;
    }
}
