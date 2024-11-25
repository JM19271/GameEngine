using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSphere : MonoBehaviour
{
    public float maxRadius = 20f;
    public float expansionRate = 5f;
    public float lifeDuration = 1f;
    private float currentRadius = 0f;

    private SphereCollider sphereCollider;
    private float elapsedLifeTime = 0f;
    private bool isMoving = true; // Local variable to store the passed state

    public void Initialize(Transform player, bool moving)
    {
        isMoving = moving; // Store the passed value
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
        // Expand or shrink the sphere based on the isMoving state
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

        // Apply scale changes
        transform.localScale = new Vector3(currentRadius, currentRadius, currentRadius);

        // Track lifetime and destroy the sphere when its life ends
        elapsedLifeTime += Time.deltaTime;
        if (elapsedLifeTime >= lifeDuration)
        {
            Destroy(gameObject);
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

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 0, 0, 0.3f); // Semi-transparent red
        Gizmos.DrawSphere(transform.position, currentRadius);
    }
}
