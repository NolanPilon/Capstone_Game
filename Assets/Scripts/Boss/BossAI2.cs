using UnityEngine;
using UnityEngine.UI;

public class BossAI2 : MonoBehaviour
{
    private static BossAI2 instance;
    public static BossAI2 Instance => instance;

    [SerializeField] private GameObject[] Boss; //[0] snake [1] wolf [2] buffalo [3] vulture
    public enum Phases
    {
        snake,
        wolf,
        buffalo,
        vulture,
        died,

        nothing
    }

    public Phases phase;

    public bool start; // check the boss mob start (BossAI2Start.cs)
    public GameObject target;   // player
    public GameObject boundary;
    private float height;
    private float width;
    public static float LeftPosBD;  //x value of left side of boundary
    public static float RightPosBD; //x value of right side of boundary
    
    public int BossHealth = 3;    //boss lives
    [SerializeField] private Image healthbar;       //boss healthbar
    float healthValue;                              //boss health value

    public PlayerCombatFunctions playerStat;               //for take damage function

    public float distance;

    private BossAI2()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        start = false;
        height = boundary.GetComponent<RectTransform>().rect.height * boundary.GetComponent<RectTransform>().localScale.y;
        width = boundary.GetComponent<RectTransform>().rect.width * boundary.GetComponent<RectTransform>().localScale.x;
        LeftPosBD = boundary.GetComponent<RectTransform>().rect.position.x - (width / 2);
        RightPosBD = boundary.GetComponent<RectTransform>().rect.position.x + (width / 2);
        playerStat = target.GetComponent<PlayerCombatFunctions>();
        phase = Phases.nothing;
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetInRange(Boss[0]) && !start)
        {
            phase = Phases.wolf;
            startPhase();
            start = true;
        }

        healthValue = (float)(BossHealth / 3f);
        healthbar.fillAmount = healthValue;
    }

    public void startPhase()
    {
        switch(phase)
        {
            case Phases.snake:
                Boss[0].GetComponent<BossAI2Snake>().StartSnake();
                break;
            case Phases.wolf:
                Boss[1].SetActive(true);
                break;
            case Phases.buffalo:
                Boss[2].SetActive(true);
                break;
            case Phases.vulture:
                Boss[3].SetActive(true);
                break;
            case Phases.died:
                Die();
                GameManager.Instance.BossDied = true;
                break;
            default: break;
        }
    }

    bool TargetInRange(GameObject boss)
    {
        if (Vector2.Distance(boss.transform.position, target.transform.position) < distance) //Distance bet player and boss is less than 10
            return true;
        return false;
    }

    void Die()
    {
        Destroy(Boss[0], 2);
    }
}
