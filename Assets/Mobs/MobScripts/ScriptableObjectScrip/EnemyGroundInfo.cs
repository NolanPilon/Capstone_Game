using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Ground")]
public class EnemyGroundInfo : ScriptableObject
{
    [Header("EnemyStats")]
    public int health = 10;
    public int damage = 10;
    public bool isDead = false;

    [Header("Pathfinding")]
    public string targetTag;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.8f;
    public float minPlayerHeightDistance = 10f;
    public float jumpModifier = 0.3f;
    public float jumpCheckOffset = 0.1f;
    public float mySpeed = 200.0f;

    [Header("Attack Info")]
    public float attackRange = 0.8f;
    public float safetyRange = 10.0f;
    public float delayAfterAttack = 3.0f;
    public float delayAfterGettingHit = 3.0f;
    public bool isRanger = false;

    [Header("Custom Behavior")]
    public bool isTurret = false;
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool minJumpDistanceEnabled = false;
    public bool directionLookEnabled = true;

    [Header("Other")]
    public GameObject[] deathParticles;
    public AudioClip deathSound;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;

}
