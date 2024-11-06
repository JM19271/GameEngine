using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSphere : MonoBehaviour
{
    public float expansionRate = 80f; 
    public float maxRadius = 40f;
    public float lifeDuration = 5f;
    private Transform playerTransform;

    private SphereCollider sphereCollider;
    private Renderer sphereRenderer;
    private float lifeTimer;
    private float elapsedLifeTime = 0f;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        sphereCollider.isTrigger = true;

        sphereRenderer = GetComponent<Renderer>();
        sphereRenderer.enabled = true;
        Color color = sphereRenderer.material.color;
        color.a = 0.3f; 
        sphereRenderer.material.color = color;

        transform.localScale = Vector3.zero;
    }
    public void Initialize()
    {
    }
    private void Update()
    {
        elapsedLifeTime += Time.deltaTime;
        float currentScaleFactor = Mathf.Lerp(0, maxRadius, elapsedLifeTime / lifeDuration);

        transform.localScale = Vector3.one * currentScaleFactor;

        if (elapsedLifeTime >= lifeDuration)
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
        Gizmos.DrawWireSphere(transform.position, maxRadius); 
    }
}
