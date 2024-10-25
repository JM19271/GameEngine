using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public OptionsScreen optionsScreen;

    // Start is called before the first frame update
    void Start()
    {
        // Ensure both menus are hidden at the start
        pauseMenu.gameObject.SetActive(false);
        optionsScreen.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If OptionsScreen is active, close it
            if (optionsScreen.gameObject.activeSelf)
            {
                optionsScreen.gameObject.SetActive(false);
                pauseMenu.gameObject.SetActive(true); // Go back to PauseMenu
            }
            // If PauseMenu is active and OptionsScreen is not active, close the PauseMenu
            else if (pauseMenu.gameObject.activeSelf)
            {
                pauseMenu.gameObject.SetActive(false);
            }
            // If both are inactive, open the PauseMenu
            else
            {
                pauseMenu.gameObject.SetActive(true);
            }
        }
    }

    // This method can be called when the options button in the pause menu is clicked
    public void OpenOptions()
    {
        pauseMenu.gameObject.SetActive(false);
        optionsScreen.gameObject.SetActive(true);
    }
}

