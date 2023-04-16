using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Timeline;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UI;
using System;

public class BossAI : MonoBehaviour
{
    public GameObject target;   //player
    public GameObject[] spawner;   //spawner
    private Rigidbody2D bossRB;
    public float speed = 5f;    //speed of boss
    int bounceTime;             //track bounce time
    int maxBounces = 6;
    int checkDistance = 20;     //distance before activating 
    Vector2 direction;          //direction of the boss movement
    Vector2 destination;        //destination for after boss bounce (1st motion)
    public Vector2 spawnPos;
    Vector2 location;           //curr location (1st & 2nd motion)
    
    bool Motion1;               //trigger of 1st motion
    bool Motion2;               //trigger of 2nd motion
    bool Motion3;               //trigger of 3rd motion3

    bool Range;                 //ON/OFF range dectect
    bool start;                 //start translate (1st & 2nd motion)
    bool coolDown;              //start cooldown (2nd motion)
    bool attack;                //start attack (2nd motion)
    bool spawnsCreated;
    bool canTakeDamage = true;
    bool alive = true;

    float angle = (-2 * Mathf.PI) / 6;      //angle in radians (3rd motion)
    RectTransform rectTransform;            //for get boss height and width
    float height;               //boss height
    float width;                //boss width

    PlayerCombatFunctions playerStat;     //for take damage function
    int BossHealth = 3;         //boss lives

    public Image healthbar;     //boss healthbar
    float healthValue;          //boss health value

    Animator animator;


    public void initalize()
    {
        bounceTime = 0;
        Motion1 = true;
        Motion2 = true;
        Motion3 = true;

        Range = true;
        start = false;
        coolDown = false;
        attack = false;
        

        for (int i = 0; i < 4; i++)
        {
            spawner[i].SetActive(false);
            
        }

        direction = Vector2.zero;
    }

    private void Start()
    {
        bossRB = GetComponent<Rigidbody2D>();

        destination = this.transform.position; //initial position == destination
        spawnPos = this.transform.position;
        rectTransform = GetComponent<RectTransform>();
        height = rectTransform.rect.height * rectTransform.localScale.y;
        width = rectTransform.rect.width * rectTransform.localScale.x;
        playerStat = target.GetComponent<PlayerCombatFunctions>();
        animator = GetComponentInChildren<Animator>();


        spawnsCreated = false;
        initalize();
    }

    private void Update()
    {
        if (alive) 
        {
            if (TargetInRange() && Range == true)
            {
                if (!start)
                {
                    direction = -(target.transform.position - this.transform.position);
                    start = true;
                    Motion1 = false;
                }
                Range = false;
            }
            if (!Motion1)
            {
                if (bounceTime < maxBounces)
                {
                    animator.SetBool("Moving", true);
                    bossRB.velocity = direction.normalized * speed;
                    //transform.Translate(direction.normalized * speed * Time.deltaTime);
                }

                if (bounceTime == maxBounces)
                {
                    transform.position = Vector2.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);

                    location = this.transform.position;

                    if (location == destination)
                    {
                        Motion1 = true;
                        Motion2 = false;
                        start = false;
                        bounceTime = 0;
                        animator.SetBool("Moving", false);
                    }
                }
            }
            if (!Motion2)
            {
                if (!coolDown)
                {
                    bossRB.velocity = Vector3.zero;
                    StartCoroutine("delayTime"); // delay 2seconds
                    coolDown = true;
                }
                if (start)
                {
                    direction = target.transform.position - this.transform.position;
                    direction.y *= -1;
                    start = false;
                    animator.SetBool("Attacking", false);
                    attack = true;
                }
                if (attack && !start)
                {
                    bossRB.velocity = direction.normalized * speed*2;
                }
            }
            if (!Motion3)
            {
                CameraShake.Instance.OnShakeCameraPosition(0.4f, 0.2f);
                bossRB.velocity = Vector3.zero;
                CreateSpawns();
                StartCoroutine(bossResetTimer());
                animator.SetBool("Attacking", true);
                Motion3 = true;
            }

            healthValue = (float)(this.BossHealth / 3f);
            healthbar.fillAmount = healthValue;


            if (BossHealth <= 0)
            {
                Die();
                GameManager.Instance.BossDied = true;
            }
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alive) 
        {
            
            
        if (collision.collider.CompareTag("Player"))
        {
            if (GameManager.parryCombo >= 3 && canTakeDamage)
            {
                
                BossHealth--;
                SoundManager.Instance.PlayBossHit();
                transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
                playerStat.launchOpposite();
                transform.position = spawnPos;
                StartCoroutine(invincible());
            }
            else
            {
                playerStat.takeDamage(collision.transform.position - this.transform.position);
                transform.position = spawnPos;
                initalize();
            }
            bossRB.velocity = Vector3.zero;
            Range = true;
        }
        else
        {
            if (!Motion1)
            {
                if (bounceTime < maxBounces)
                {
                    direction = Vector2.Reflect(direction.normalized, collision.contacts[0].normal);
                    bounceTime++;
                    SoundManager.Instance.PlayBossBounce();
                }

                if (bounceTime == maxBounces)
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
                    SoundManager.Instance.PlayBossBounce();
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
    }

    bool TargetInRange()
    {
        if (Vector2.Distance(this.transform.position, target.transform.position) < checkDistance) //Distance bet player and boss is less than 10
            return true;
        return false;
    }

    IEnumerator delayTime()
    {
        yield return new WaitForSeconds(2);
        
        start = true;
    }

    IEnumerator bossResetTimer()
    {
        yield return new WaitForSeconds(5);
        if (Motion1 && Motion3) 
        {
            transform.position = spawnPos;
            initalize();
        }
    }

    IEnumerator invincible()
    {
        canTakeDamage = false;
        initalize();
        yield return new WaitForSeconds(2);
        canTakeDamage = true;
    }

    void Die() 
    {
        alive = false;
        SoundManager.Instance.PlayBossDeath();
        bossRB.velocity = Vector2.zero;
        animator.SetBool("Dying", true);
        Destroy(gameObject, 2);
    }

    void CreateSpawns()
    {
        if (!spawnsCreated)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject sphere = spawner[i];
                sphere.SetActive(enabled);
                float r = width + 1.8f;    // distance from center
                                           // sin and cos need value in radians
                Vector2 pos2d = new Vector2(Mathf.Sin(angle) * r + this.transform.position.x, Mathf.Cos(angle) * r + this.transform.position.y - (height / 2));
                sphere.transform.position = new Vector2(pos2d.x, pos2d.y);

                angle += Mathf.PI / 4.5f;
                spawnsCreated = true;
            }
        }
        else 
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject sphere = spawner[i];
                sphere.SetActive(enabled);
            }
        }
    }

    public bool getIfMotion2()
    {
        return Motion2;
    }
}
