using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterCollision : MonoBehaviour
{
    public string jumpScareScene = "JumpScare";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  
        {
            SceneManager.LoadScene(jumpScareScene);  
        }
    }
}
