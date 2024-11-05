using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class CreditScreen : MonoBehaviour
{
    [SerializeField] GameObject creditScreen;

    public void OpenCredit()
    {
        creditScreen.SetActive(true);
    }

    public void CloseCredit()
    {
        creditScreen.SetActive(false);
    }
}
