using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight; 
    public KeyCode toggleKey = KeyCode.F; 
    public AudioClip turnOnSound;
    public AudioClip turnOffSound;
    private AudioSource audioSource;

    private bool isOn = false;
    public float fadeDuration = 0.5f;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponentInChildren<Light>();
        }
        flashlight.enabled = isOn; 

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;

            if (isOn)
            {
                audioSource.PlayOneShot(turnOnSound);
            }
            else
            {
                audioSource.PlayOneShot(turnOffSound);
            }
        }
    }

    private IEnumerator FadeLight(bool turnOn)
    {
        float targetIntensity = turnOn ? 1f : 0f; 
        float startIntensity = flashlight.intensity;
        float elapsedTime = 0f;

        flashlight.enabled = true;

        while (elapsedTime < fadeDuration)
        {
            flashlight.intensity = Mathf.Lerp(startIntensity, targetIntensity, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        flashlight.intensity = targetIntensity;
        flashlight.enabled = turnOn; 
    }
}
