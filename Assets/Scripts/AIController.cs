using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent navAgent;
    public Transform Player;

    public float walkSpeed = 2f;
    public float runSpeed = 5f;
    public float chaseDistance = 10f;

    public Transform[] waypoints;
    private int currentWaypointIndex = 0;
    public float waypointTolerance = 1f;


    private void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, Player.position);

        if (distanceToPlayer < chaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void ChasePlayer()
    {
        navAgent.speed = runSpeed;
        navAgent.SetDestination(Player.position);

        animator.SetBool("IsRunning", true);
        animator.SetBool("IsWalking", false);

        Debug.Log("Chasing Player.");
    }

    void Patrol()
    {
        if (waypoints.Length == 0) return;

        navAgent.speed = walkSpeed;
        navAgent.SetDestination(waypoints[currentWaypointIndex].position);

        animator.SetBool("IsRunning", false);
        animator.SetBool("IsWalking", true);

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < waypointTolerance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length; 
        }

        Debug.Log("Patrolling.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            JumpScareManager jumpScareManager = FindObjectOfType<JumpScareManager>();
            if (jumpScareManager != null)
            {
                jumpScareManager.TriggerJumpScare();
            }
        }
    }

    public void HeardPlayer()
    {
        Debug.Log("Monster heard the player's footsteps!");
    }

}
