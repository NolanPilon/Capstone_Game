using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Air")]
public class EnemyAirInfo : ScriptableObject
{
    [Header("EnemyStats")]
    public int health = 10;
    public int damage = 10;
    public bool isDead = false;


    [Header("Pathfinding")]
    public string targetTag;
    public float activateDistance = 50.0f;
    public float pathUpdateSeconds = 0.5f;

    [Header("Physics")]
    public float nextWaypointDistance = 3.0f;
    public float mySpeed = 200f;

    [Header("Attack Info")]
    public float attackRange = 0.8f;
    public float safetyRange = 10.0f;
    public float delayAfterAttack = 3.0f;
    public float delayAfterGettingHit = 3.0f;
    public bool isRanger = false;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool directionLookEnabled = true;
    public bool isTerritorial = false;

    [Header("Other")]
    public GameObject deathParticles;

    private Path path;
    private int currentWaypoint = 0;
    bool isGrounded = false;
    Seeker seeker;
    Rigidbody2D rb;
}
