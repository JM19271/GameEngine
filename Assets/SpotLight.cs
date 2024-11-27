using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    public Light lightSource; // Reference to the Light (Point Light, Spot Light, etc.)
    public float minIntensity = 0.5f; // Minimum light intensity
    public float maxIntensity = 2f; // Maximum light intensity
    public float flickerSpeed = 0.1f; // Speed of flickering

    private float nextFlickerTime; // Time for the next flicker

    void Start()
    {
        // Get the Light component if it's not assigned
        if (lightSource == null)
            lightSource = GetComponent<Light>();

        ScheduleNextFlicker();
    }

    void Update()
    {
        // Flicker the light at scheduled intervals
        if (Time.time >= nextFlickerTime)
        {
            FlickerLight();
            ScheduleNextFlicker();
        }
    }

    void FlickerLight()
    {
        // Randomly change the intensity of the light
        lightSource.intensity = Random.Range(minIntensity, maxIntensity);
    }

    void ScheduleNextFlicker()
    {
        // Schedule the next flicker with a random interval
        nextFlickerTime = Time.time + Random.Range(0.05f, flickerSpeed);
    }
}
