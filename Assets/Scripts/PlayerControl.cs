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
    public float soundSphereLifetime = 5f;
    public float maxSoundRadius = 5f;
    public float soundExpandSpeed = 10f;


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
                InstantiateSoundSphere();
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
            int randomIndex = Random.Range(0, Footsteps.Length);
            footstepAudioSource.clip = Footsteps[randomIndex];
            footstepAudioSource.pitch = pitchSlowDownFactor; 

            footstepAudioSource.volume = Mathf.Clamp01(1 / pitchSlowDownFactor); 

            footstepAudioSource.Play();
        }
    }

    private void InstantiateSoundSphere()
    {
        if (soundSpherePrefab)
        {
            GameObject soundSphere = Instantiate(soundSpherePrefab, transform.position, Quaternion.identity);
            SoundSphere sphereScript = soundSphere.GetComponent<SoundSphere>();
            sphereScript.Initialize();  
        }
    }

    private IEnumerator ExpandSoundSphere(GameObject soundSphere)
    {
        float elapsedTime = 0f;
        Vector3 targetScale = Vector3.one * maxSoundRadius; 

        while (elapsedTime < soundSphereLifetime)
        {
            soundSphere.transform.localScale = Vector3.Lerp(Vector3.zero, targetScale, (elapsedTime / soundSphereLifetime));
            elapsedTime += Time.deltaTime * soundExpandSpeed;
            yield return null; 
        }

        soundSphere.transform.localScale = targetScale;

        Destroy(soundSphere, soundSphereLifetime);
    }
}
