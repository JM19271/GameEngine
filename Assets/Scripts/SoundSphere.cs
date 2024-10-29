using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSphere : MonoBehaviour
{
    public float expansionRate = 5f; 
    public float maxRadius = 10f;
    public float lifeDuration = 2f;

    private SphereCollider sphereCollider;
    private float lifeTimer;

    private void Start()
    {
        GetComponent<Renderer>().enabled = false;
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;
        transform.localScale = Vector3.zero;
        lifeTimer = lifeDuration;
    }

    private void Update()
    {
        if (transform.localScale.x < maxRadius)
        {
            transform.localScale += Vector3.one * expansionRate * Time.deltaTime;
        }

        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Sound sphere touched the monster!");
            AIController monsterAI = other.GetComponent<AIController>();
            if (monsterAI != null)
            {
                monsterAI.HeardPlayer();  
            }
            Destroy(gameObject); 
        }
        else if (other.CompareTag("Obstacle"))
        {
            Debug.Log("Sound sphere hit an obstacle and was destroyed.");
            Destroy(gameObject);  
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red; 
        Gizmos.DrawSphere(transform.position, 0.5f); 
    }
}
