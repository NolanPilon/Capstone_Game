using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerControlls))]
public class ParryBehavior : MonoBehaviour
{
    public PlayerControlls playerController;
    public GameObject storedParryObject;
    public GameObject previousParryObject;

    [SerializeField] private GameObject parryArrow;
    [SerializeField] private GameObject playerObject;
    [SerializeField] private GameObject playerGlow;
    private Rigidbody2D playerRB;
    private Rigidbody2D bulletRb = null;
    private Transform bulletPos = null;
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

        ParryFunction();

        // Temp fix for control issues
        if (playerController.isGrounded() || Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Space) && !playerController.isGrounded() && !inParry)
        {
            playerController.controlsEnabled = true;
        }
    }   


    private void OnTriggerStay2D(Collider2D collision)
    {
        // Store current parry object
        if (collision.tag == "Parry")
            storedParryObject = collision.gameObject;

        if (collision.tag == "Parry" && !storedParryObject.GetComponent<PreviouslyParried>().beenParried)
        {
            // Change the previous parry point to be active when entering a new parry point
            if (previousParryObject != null)
                previousParryObject.GetComponent<PreviouslyParried>().beenParried = false;

            if (bulletRb == null) 
            {
                bulletRb = collision.gameObject.GetComponent<Rigidbody2D>();
                bulletPos = collision.gameObject.GetComponent<Transform>();
                playerGlow.SetActive(true);
            }

            // Stop the bullet if moving
            if (bulletRb != null && inParry) 
            {
                bulletRb.velocity = Vector2.zero;
            }

            canParry = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Parry")
        {
            playerGlow.SetActive(false);
            bulletRb = null;
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
            previousParryObject = storedParryObject;
            storedParryObject.GetComponent<PreviouslyParried>().beenParried = true;
            launchDir = (parryArrow.transform.position - playerObject.transform.position);
            if (bulletRb != null)
                bulletRb.AddForce((-launchDir) * 10, ForceMode2D.Impulse);
            playerRB.AddForce(launchDir * (launchSpeed + (launchMultiplier * GameManager.parryCombo)), ForceMode2D.Impulse);
            GameManager.parryCombo += 1;
        }

        // If in range of projectile and space is pressed
        // Parry arrow shows up and time is slowed
        // When you let go hide the arrow and reset time
        if (Input.GetKeyDown(KeyCode.Space) && canParry && !playerController.isGrounded())
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

        // Reset parry objects if grounded
        if (playerController.isGrounded() && previousParryObject != null) 
        {
            previousParryObject.GetComponent<PreviouslyParried>().beenParried = false;
        }
    }
}
