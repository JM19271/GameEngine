using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public Animator animator;
    public NavMeshAgent navAgent;
    public Transform player;

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
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < chaseDistance)
        {
            navAgent.speed = runSpeed;
            animator.SetBool("IsRunning", true);
            animator.SetBool("IsWalking", false);
            navAgent.SetDestination(player.position);
            Debug.Log("Running");
        }
        else
        {
            navAgent.speed = walkSpeed;
            animator.SetBool("IsRunning", false);
            animator.SetBool("IsWalking", true);
            Patrol();
            Debug.Log("Walking/Patrolling");
        }
    }
    void Patrol()
    {
        if (waypoints.Length == 0) return;

        navAgent.SetDestination(waypoints[currentWaypointIndex].position);

        if (Vector3.Distance(transform.position, waypoints[currentWaypointIndex].position) < waypointTolerance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

}
