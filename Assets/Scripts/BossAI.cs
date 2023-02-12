using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using static UnityEditor.FilePathAttribute;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;

public class BossAI : MonoBehaviour
{
    public GameObject target;   //player
    public GameObject[] spawner;   //spawner
    public float speed = 5f;    //speed of boss
    int bounceTime;             //track bounce time
    Vector2 direction;          //direction of the boss movement
    Vector2 destination;        //destination for after boss bounce (1st motion)
    Vector2 location;           //curr location (1st & 2nd motion)
    
    bool Motion1;               //trigger of 1st motion
    bool Motion2;               //trigger of 2nd motion
    bool Motion3;               //trigger of 3rd motion3

    bool Range;                 //ON/OFF range dectect
    bool start;                 //start translate (1st & 2nd motion)
    bool coolDown;              //start cooldown (2nd motion)
    bool attack;                //start attack (2nd motion)

    float angle = (-2 * Mathf.PI) / 6;      //angle in radians (3rd motion)
    RectTransform rectTransform;            //for get boss height and width
    float height;               //boss height
    float width;                //boss width

    PlayerCombatFunctions playerStat;     //for take damage function
    int BossHealth = 3;         //boss lives

    public Image healthbar;     //boss healthbar
    float healthValue;          //boss health value

    void initalize()
    {
        bounceTime = 0;
        Motion1 = true;
        Motion2 = true;
        Motion3 = true;

        Range = true;
        start = false;
        coolDown = false;
        attack = false;

        for (int i = 0; i < 5; i++)
        {
            spawner[i].SetActive(false);
        }

        direction = Vector2.zero;
    }

    private void Start()
    {
        destination = this.transform.position; //initial position == destination
        rectTransform= GetComponent<RectTransform>();
        height = rectTransform.rect.height * rectTransform.localScale.y;
        width = rectTransform.rect.width * rectTransform.localScale.x;
        playerStat = target.GetComponent<PlayerCombatFunctions>();
        initalize();
    }

    private void Update()
    {
        if (TargetInRange() && Range == true)
        {
            if (!start)
            {
                direction = target.transform.position - this.transform.position;
                start = true;
                Motion1 = false;
            }
            Range= false;
        }
        if (!Motion1)
        {
            if (bounceTime < 4)
            {
                transform.Translate(direction.normalized * speed * Time.deltaTime);
            }

            if (bounceTime == 4)
            {
                transform.position = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);

                location = this.transform.position;

                if (location == destination)
                {
                    Motion1 = true;
                    Motion2 = false;
                    start = false;
                    bounceTime = 0;
                }
            }
        }
        if(!Motion2)
        {
            if (!coolDown)
            {
                StartCoroutine("delayTime"); // delay 3seconds
                coolDown = true;
            }
            if (start)
            {
                direction = target.transform.position - this.transform.position;
                direction.y *= -1;
                start = false;
                attack = true;
            }
            if (attack && !start)
            {
                transform.Translate(direction.normalized * 1.5f * speed * Time.deltaTime);
            }
        }
        if (!Motion3)
        {
            CreateSpawns();
            Motion3 = true;
        }

        healthValue = (float)(this.BossHealth / 3f);
        healthbar.fillAmount = healthValue;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (GameManager.parryCombo >= 3)
            {
                BossHealth--;
                transform.position = destination;
                initalize();
            }
            else
            {
                transform.position = destination;
                playerStat.takeDamage(collision.transform.position - this.transform.position);
                initalize();
            }

            Rigidbody2D rb = this.GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            Range = true;
        }
        else
        {
            if (!Motion1)
            {
                if (bounceTime < 4)
                {
                    direction = Vector2.Reflect(direction.normalized, collision.contacts[0].normal);

                    bounceTime++;
                }

                if (bounceTime == 4)
                {
                    location = this.transform.position;
                }
            }

            if (!Motion2)
            {
                if (bounceTime == 0)
                {
                    direction = target.transform.position - this.transform.position;
                    bounceTime++;
                }
                else
                {
                    direction = Vector2.zero;
                    Motion2 = true;
                    Motion3 = false;
                }
            }
        }
    }

    bool TargetInRange()
    {
        if (Vector2.Distance(this.transform.position, target.transform.position) < 10) //Distance bet player and boss is less than 10
            return true;
        return false;
    }

    IEnumerator delayTime()
    {
        yield return new WaitForSeconds(3);
        
        start = true;
    }

    void CreateSpawns()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject sphere = spawner[i];
            sphere.SetActive(enabled);
            float r = width + 0.5f;    // distance from center
                             // sin and cos need value in radians
            Vector2 pos2d = new Vector2(Mathf.Sin(angle) * r + this.transform.position.x, Mathf.Cos(angle) * r + this.transform.position.y - (height/2));
            sphere.transform.position = new Vector2(pos2d.x, pos2d.y);

            angle += Mathf.PI / 6;
        }
    }
}
