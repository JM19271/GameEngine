using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float speed = 6.0f;
    public float gravity = -9.81f;
    private Vector3 velocity;
    private bool isGrounded;
    public Transform cameraTransform;
    private CharacterController controller;

    public AudioSource footstepAudioSource; 
    public AudioClip[] Footsteps; 
    public float stepInterval = 0.5f; 
    private float stepTimer;
    public float pitchSlowDownFactor = 0.5f;

    public GameObject soundSpherePrefab;
    private float lastSoundSphereTime;
    public float soundSphereInterval = 0.5f;

    private bool isMoving = false;

    private GameObject activeSoundSphere;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stepTimer = stepInterval;
        CreateSoundSphere();
    }

    void Update()
    {

        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 move = cameraTransform.right * moveX + cameraTransform.forward * moveZ;
        move.y = 0;
        controller.Move(move * speed * Time.deltaTime);

        if (move.magnitude > 0.1f && isGrounded)
        {
            isMoving = true;
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                HandleFootsteps();
                CreateSoundSphere();
            }
        }
        else
        {
            isMoving = false;
            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop(); 
            }
            stepTimer = stepInterval;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void HandleFootsteps()
    {
        stepTimer -= Time.deltaTime;
        if (stepTimer <= 0f)
        {
            PlayFootstepSound();
            UpdateSoundSphereRadius();
            stepTimer = stepInterval;
        }
    }

    private void PlayFootstepSound()
    {
        if (footstepAudioSource && Footsteps.Length > 0)
        {
            int randomIndex = Random.Range(0, Footsteps.Length);
            footstepAudioSource.clip = Footsteps[randomIndex];
            footstepAudioSource.pitch = pitchSlowDownFactor; 
            footstepAudioSource.volume = Mathf.Clamp01(1 / pitchSlowDownFactor); 
            footstepAudioSource.Play();
            Debug.Log("Playing footstep sound.");
        }
    }

    private void CreateSoundSphere()
    {
        if (soundSpherePrefab != null && Time.time - lastSoundSphereTime >= soundSphereInterval)
        {
            lastSoundSphereTime = Time.time; // Track last time a SoundSphere was created

            // Destroy previous sound sphere if it exists
            if (activeSoundSphere != null)
            {
                Debug.Log("Destroying previous sound sphere"); // Debug log
                Destroy(activeSoundSphere);
            }

            // Create a new sound sphere
            activeSoundSphere = Instantiate(soundSpherePrefab, transform.position, Quaternion.identity);
            SoundSphere sphereScript = activeSoundSphere.GetComponent<SoundSphere>();
            if (sphereScript != null)
            {
                sphereScript.Initialize(transform, isMoving);  // Pass the player's transform and moving state
            }
        }
    }


    private void UpdateSoundSphereRadius()
    {
        // This method will adjust the sound sphere's radius if necessary, depending on player movement (walking vs. running)
        if (soundSpherePrefab != null)
        {
            SoundSphere sphereScript = soundSpherePrefab.GetComponent<SoundSphere>();

            if (sphereScript != null)
            {
                if (Input.GetKey(KeyCode.LeftShift))  // Running
                {
                    sphereScript.maxRadius = 30f;
                    sphereScript.expansionRate = 10f;
                    Debug.Log("Running: Increasing sound sphere radius.");
                }
                else  // Walking
                {
                    sphereScript.maxRadius = 20f;
                    sphereScript.expansionRate = 5f;
                    Debug.Log("Walking: Setting default sound sphere radius.");
                }
            }
        }
    }
}
