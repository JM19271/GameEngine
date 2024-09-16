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
            animator.SetBool("isRunning", true);
            animator.SetBool("isWalking", false);
            navAgent.SetDestination(player.position);
        }
        else
        {
            navAgent.speed = walkSpeed;
            animator.SetBool("isRunning", false);
            animator.SetBool("isWalking", true);
            Patrol();
        }
    }
    void Patrol()
    {
        
    }

}
