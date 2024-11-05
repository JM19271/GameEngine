using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class WinScene : MonoBehaviour
{
    [SerializeField] GameObject winScene;
    public GameObject creditScreen;

    public void Home()
    {
        SceneManager.LoadScene("Main menu");
        Time.timeScale = 1;
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void OpenCredit()
    {
        creditScreen.SetActive(true);
    }

    public void CloseCredit()
    {
        creditScreen.SetActive(false);
    }
}
