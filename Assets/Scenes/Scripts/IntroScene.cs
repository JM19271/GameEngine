using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IntroScene : MonoBehaviour
{

    
    public string secondLevel;
    public void SContinue()
    {
        SceneManager.LoadScene(secondLevel);
    }
}
