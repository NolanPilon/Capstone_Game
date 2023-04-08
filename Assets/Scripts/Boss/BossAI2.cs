using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossAI2 : MonoBehaviour
{
    private static BossAI2 instance;
    public static BossAI2 Instance => instance;

    [SerializeField] private GameObject[] Boss; //[0] snake [1] wolf [2] buffalo [3] vulture
    public bool[] motion = new bool[5]; //trigger the motions
                                        //motion[0] : snake
                                        //motion[1] : wolf
                                        //motion[2] : buffalo
                                        //motion[3] : vulture
                                        //motion[4] : died

    private bool start = false; // check the boss mob start
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

    private BossAI2()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        height = boundary.GetComponent<RectTransform>().rect.height * boundary.GetComponent<RectTransform>().localScale.y;
        width = boundary.GetComponent<RectTransform>().rect.width * boundary.GetComponent<RectTransform>().localScale.x;
        LeftPosBD = boundary.GetComponent<RectTransform>().rect.position.x - (width / 2);
        RightPosBD = boundary.GetComponent<RectTransform>().rect.position.x + (width / 2);
        playerStat = target.GetComponent<PlayerCombatFunctions>();
    }

    // Update is called once per frame
    void Update()
    {
        if (TargetInRange(Boss[0]) && !start)
        {
            motion[1] = true;
            start = true;
        }
        else
        {
            if (motion[1])
            {
                Boss[1].SetActive(true);
            }
            else if (motion[2])
            {
                Boss[2].SetActive(true);
            }
            else if (motion[3])
            {
                Boss[3].SetActive(true);
            }
        }

        healthValue = (float)(BossHealth / 3f);
        healthbar.fillAmount = healthValue;

        if (motion[4])
        {
            Die();
            GameManager.Instance.BossDied = true;
        }
    }

    bool TargetInRange(GameObject boss)
    {
        if (Vector2.Distance(boss.transform.position, target.transform.position) < 10) //Distance bet player and boss is less than 10
            return true;
        return false;
    }

    void Die()
    {
        Destroy(Boss[0], 2);
    }
}
