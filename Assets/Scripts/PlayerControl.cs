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

    public AudioSource footstepAudioSource; // Reference to the AudioSource component
    public AudioClip[] Footsteps; // Array of footstep sound clips
    public float stepInterval = 0.5f; // Interval between steps
    private float stepTimer;
    public float pitchSlowDownFactor = 0.5f;


    void Start()
    {
        controller = GetComponent<CharacterController>();
        stepTimer = stepInterval;
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
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstepSound();
                stepTimer = stepInterval; 
            }
        }
        else
        {
            if (footstepAudioSource.isPlaying)
            {
                footstepAudioSource.Stop(); 
            }
            stepTimer = stepInterval;
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void PlayFootstepSound()
    {
        if (footstepAudioSource && Footsteps.Length > 0)
        {
            // Play a random footstep sound
            int randomIndex = Random.Range(0, Footsteps.Length);
            footstepAudioSource.clip = Footsteps[randomIndex];
            footstepAudioSource.pitch = pitchSlowDownFactor; // Set pitch to slow down playback

            // Compensate volume for pitch adjustment
            footstepAudioSource.volume = Mathf.Clamp01(1 / pitchSlowDownFactor); // Increase volume proportionally to pitch

            footstepAudioSource.Play();
        }
    }
}
