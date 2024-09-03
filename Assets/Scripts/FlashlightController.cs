using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight; //This assigns the flashlight a light component
    public KeyCode toggleKey = KeyCode.F; // This allows the player to toggle the flashlight with the key F

    private bool isOn = false;

    void Start()
    {
        if (flashlight == null)
        {
            flashlight = GetComponentInChildren<Light>();
        }
        flashlight.enabled = isOn; //Ensure the flashlight is off when the game starts
    }

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            isOn = !isOn;
            flashlight.enabled = isOn;
        }
    }
}
