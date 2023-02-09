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
    private Rigidbody2D playerRB;
    private float launchSpeed = 20.0f;
    private Vector2 launchDir;

    private bool canParry = false;
    private bool inParry = false;

    void Awake()
    {
        playerRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ParryFunction();
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Parry" || collision.tag == "Enemy")
        {
            canParry = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Parry" || collision.tag == "Enemy")
        {
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
            playerRB.AddForce(launchDir * launchSpeed, ForceMode2D.Impulse);
            GameManager.parryCombo += 1;
        }

        // If in range of enemy and space is pressed
        // Parry arrow shows up and time is slowed
        // When you let go hide the arrow and reset time
        if (canParry && Input.GetKeyDown(KeyCode.Space))
        {
            Time.timeScale = 0.1f;
            playerController.controlsEnabled = false;
            playerRB.gravityScale = 0;
            playerRB.velocity = Vector2.zero;
            parryArrow.SetActive(true);
            inParry = true;
        }
        else if (Input.GetKeyUp(KeyCode.Space) || !inParry)
        {
            inParry = false;
            Time.timeScale = 1f;
            playerRB.gravityScale = 2;
            parryArrow.SetActive(false);
        }

        // Temp fix for air movement
        if (playerController.isGrounded() || Input.anyKeyDown && !Input.GetKeyDown(KeyCode.Space) && !playerController.isGrounded() && !inParry)
        {
            playerController.controlsEnabled = true;
        }
    }
}
