using UnityEngine;
using UnityEngine.UI;

public class DoorInteraction : MonoBehaviour
{
    public GameObject door;  // Assign your door in the inspector
    public float interactDistance = 3f;  // Distance required to interact
    private bool isNearDoor = false;
    private bool isDoorOpen = false;

    public Text interactionText;  // Assign the UI text in the inspector

    void Start()
    {
        // Hide the interaction text at the start
        interactionText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check the distance between the player and the door
        if (Vector3.Distance(transform.position, door.transform.position) < interactDistance)
        {
            isNearDoor = true;
            ShowInteractionMessage();  // Show message when near door

            // If 'E' key is pressed and near the door, open the door
            if (Input.GetKeyDown(KeyCode.E))
            {
                OpenDoor();
            }
        }
        else
        {
            isNearDoor = false;
            HideInteractionMessage();  // Hide message when far from the door
        }
    }

    void ShowInteractionMessage()
    {
        if (isNearDoor && !isDoorOpen)
        {
            // Show the UI text on the screen
            interactionText.gameObject.SetActive(true);
            interactionText.text = "Press E to open the door";
        }
    }

    void HideInteractionMessage()
    {
        // Hide the UI text when the player is far away
        interactionText.gameObject.SetActive(false);
    }

    void OpenDoor()
    {
        if (!isDoorOpen)
        {
            // Rotate the door on Y axis to simulate opening
            door.transform.Rotate(0, 90, 0);
            isDoorOpen = true;

            // Optionally hide the message once the door is opened
            interactionText.gameObject.SetActive(false);
        }
    }
}
