using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerControlls))]
public class ParryBehavior : MonoBehaviour
{
    public PlayerControlls playerController;

    [SerializeField] private GameObject parryArrow;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject playerGlow;
    private Rigidbody2D playerRB;
    private float launchSpeed = 20.0f;
    private float launchMultiplier = 3.5f;
    private Vector2 launchDir;
    private float holdTimer = 0;

    public bool canParry = false;
    public static bool inParry = false;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Time t'ill you get kicked out of parry state
        if (holdTimer > 0) 
        {
            holdTimer -= Time.deltaTime * 5;
        }

        if (!canParry && !inParry) 
        {
            holdTimer = 1.0f;
        }

        if (canParry) 
        {
            ParryFunction();
        }


        // Temp fix for control issues
        if (playerController.isGrounded() || Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Space) && !playerController.isGrounded() && !inParry)
        {
            playerController.controlsEnabled = true;
        }
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Parry")
        {
            playerGlow.SetActive(true);
            canParry = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Parry")
        {
            playerGlow.SetActive(false);
            canParry = false;
            inParry = false;
        }
    }

    private void ParryFunction() 
    {
        // If the player is in the slow-mo state and lets go of space
        // Launch towards arrow indicator
        if (inParry && Input.GetKeyUp(KeyCode.Space))
        {
            launchDir = (parryArrow.transform.position - playerObject.transform.position);
            playerRB.AddForce(launchDir * (launchSpeed + (launchMultiplier * GameManager.parryCombo)), ForceMode2D.Impulse);
            GameManager.parryCombo += 1;
        }

        // If in range of projectile and space is pressed
        // Parry arrow shows up and time is slowed
        // When you let go hide the arrow and reset time
        if (Input.GetKey(KeyCode.Space))
        {
            holdTimer = 1.0f;
            Time.timeScale = 0.1f;
            playerController.controlsEnabled = false;
            playerRB.gravityScale = 0;
            playerRB.velocity = Vector2.zero;
            parryArrow.SetActive(true);
            inParry = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || !inParry || holdTimer <= 0)
        {
            inParry = false;
            Time.timeScale = 1f;
            playerRB.gravityScale = 2;
            parryArrow.SetActive(false);
        }
    }
}
