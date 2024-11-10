using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSphere : MonoBehaviour
{
    public float maxRadius = 20f;    
    public float expansionRate = 5f; 
    public float lifeDuration = 1f;  
    private float currentRadius = 0f;

    public Transform playerTransform;
    private SphereCollider sphereCollider;
    private float elapsedLifeTime = 0f;
    private bool isMoving = true;

    public void Initialize(Transform player, bool moving)
    {
        playerTransform = player;
        isMoving = moving;
        Debug.Log("Initializing sound sphere with player: " + player.name);
        sphereCollider = GetComponent<SphereCollider>();

        if (sphereCollider == null)
        {
            Debug.LogError("SphereCollider component missing on SoundSphere prefab.");
            return;
        }

        sphereCollider.isTrigger = true;
        sphereCollider.radius = 0.1f; 
        transform.localScale = Vector3.zero;
    }

    private void Update()
    {
        if (isMoving)
        {
            if (currentRadius < maxRadius)
            {
                currentRadius += expansionRate * Time.deltaTime;
            }
        }
        else
        {
            if (currentRadius > 0f)
            {
                currentRadius -= expansionRate * Time.deltaTime;
            }

            if (currentRadius <= 0f)
            {
                currentRadius = 0f; 
            }
        }

        transform.localScale = new Vector3(currentRadius, currentRadius, currentRadius);

        if (currentRadius == maxRadius)
        {
            Debug.Log("Max radius reached.");
        }
    } 


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            AIController monsterAI = other.GetComponent<AIController>();
            if (monsterAI != null)
            {
                monsterAI.HeardPlayer(transform.position);
            }
        }
    }
}
