using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMiniGame : MonoBehaviour
{
    public CarTrigger carTrigger;
    public InventorySystem inventory;
    public List<string> requiredItems;
    public GameObject miniGameUI;
    public Slider progressBar;
    public GameObject subGameUI;
    public PointerMiniGame pointerMiniGame;
    public TextMeshProUGUI winMessageText;
    public TextMeshProUGUI CarMessageText;

    public bool isPlaying = false;
    private bool canStartGame = false;
    public bool inSubGame = false;
    private bool isCompleted = false;

    [SerializeField]
    private float progress = 0f;
    private const float SubGameProgressThreshold = 0.5f;
    private const float ProgressRate = 0.2f;
    public bool MaingameCompleted { get; private set; } = false;

    void Start()
    {
        InitializeUI();
    }

    void Update()
    {
        HandlePlayerProximity();

        if (isPlaying && !inSubGame && !isCompleted)
        {
            HandleProgress();
        }

        Debug.Log($"Current Status: isSubGameActive={inSubGame}, isMainGameActive={isPlaying}, progress={progress}");
    }

    private void InitializeUI()
    {
        miniGameUI.SetActive(false);
        subGameUI.SetActive(false);
    }

    private void HandlePlayerProximity()
    {
        if (carTrigger.IsPlayerInRange())
        {
            if (!isPlaying && !isCompleted)
            {
                CheckForMiniGameStart();
            }
            else if (isPlaying && !inSubGame)
            {
                HandleProgress();
            }
        }
        else
        {
            if (isPlaying && !isCompleted)
            {
                PauseMiniGame();
            }

            if (inSubGame)
            {
                EndSubGame();
            }
        }
    }

    private void PauseMiniGame()
    {
        if (isPlaying)
        {
            Debug.Log("Player left the range, mini-game paused.");
            isPlaying = false;
            miniGameUI.SetActive(false);
            ResetProgress();
            EndSubGame();  
        }
    }

    private void CheckForMiniGameStart()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (inventory.HasRequiredItems(requiredItems))
            {
                canStartGame = true;
                Debug.Log("Player has collected all required items. Ready to start mini-game.");

                if (!isPlaying && !inSubGame)
                {
                    StartMainGame();
                }
            }

            else
            {
                canStartGame = false;
                DisplayCarMessage();
                Debug.Log("Player has not collected all required items.");
            }
        }
    }
    private void DisplayCarMessage()
    {
        if (CarMessageText != null)
        {
            CarMessageText.gameObject.SetActive(true);
            CarMessageText.text = "You didn't collect all of the parts!";

            StartCoroutine(FadeInCarMessage(1f));
        }
    }
    private IEnumerator FadeInCarMessage(float duration)
    {
        Color textColor = CarMessageText.color;
        textColor.a = 0;
        CarMessageText.color = textColor;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(elapsedTime / duration);
            CarMessageText.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(0.6f);
        StartCoroutine(FadeOutCarMessage(1f));
    }

    private IEnumerator FadeOutCarMessage(float duration)
    {
        Color textColor = CarMessageText.color;
        float startAlpha = textColor.a;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(startAlpha - (elapsedTime / duration));
            CarMessageText.color = textColor;
            yield return null;
        }

        CarMessageText.gameObject.SetActive(false);
    }

    private void StartMainGame()
    {
        isPlaying = true;
        miniGameUI.SetActive(true);
        progressBar.value = progress;
        Debug.Log("Main repair mini-game started.");
    }

    private void HandleProgress()
    {
        if (Input.GetKey(KeyCode.E))
        {
            progress += Time.deltaTime * ProgressRate;
            progressBar.value = progress;

            if (progress >= SubGameProgressThreshold && !pointerMiniGame.gameCompleted)
            {
                PauseMainGameAndStartSubGame();
            }

            if (progress >= 1f)
            {
                CompleteGame();
            }
        }
    }

    private void PauseMainGameAndStartSubGame()
    {
        Debug.Log("Progress reached threshold, pausing main game and starting sub-game.");
        isPlaying = false;
        inSubGame = true;
        subGameUI.SetActive(true);
        pointerMiniGame.pointer.gameObject.SetActive(true);
    }

    private void CompleteGame()
    {
        if (isCompleted) return;

        Debug.Log("Repair mini-game completed. Hiding UI.");
        isPlaying = false;
        isCompleted = true; 
        miniGameUI.SetActive(false); 

        MaingameCompleted = true;

        DisplayWinningMessage();
        StartCoroutine(LoadWinningSceneAfterDelay(2f));
    }

    private void EndMainGame(bool resetProgress = false)
    {
        if (!isCompleted) return;

        Debug.Log("Main repair mini-game ended. Hiding UI.");
        isPlaying = false;
        miniGameUI.SetActive(false);

        if (resetProgress)
        {
            ResetProgress();
        }
    }

    private void ResetProgress()
    {
        progress = 0f;
        progressBar.value = progress;
        isCompleted = false;
        MaingameCompleted = false;
        winMessageText.gameObject.SetActive(false);
    }

    private void DisplayWinningMessage()
    {
        if (winMessageText != null)
        {
            winMessageText.gameObject.SetActive(true);
            winMessageText.text = "The car has been fixed!";

            StartCoroutine(FadeInWinMessage(1f));
        }
    }

    private IEnumerator FadeInWinMessage(float duration)
    {
        Color textColor = winMessageText.color;
        textColor.a = 0;
        winMessageText.color = textColor;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(elapsedTime / duration); 
            winMessageText.color = textColor;
            yield return null;
        }

        yield return new WaitForSeconds(5f); 
        StartCoroutine(FadeOutWinMessage(1f)); 
    }

    private IEnumerator FadeOutWinMessage(float duration)
    {
        Color textColor = winMessageText.color;
        float startAlpha = textColor.a;

        float elapsedTime = 0f;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            textColor.a = Mathf.Clamp01(startAlpha - (elapsedTime / duration)); 
            winMessageText.color = textColor;
            yield return null;
        }

        winMessageText.gameObject.SetActive(false);
    }

    private IEnumerator LoadWinningSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("WinScene"); 
    }

    private IEnumerator HideWinningMessageAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        winMessageText.gameObject.SetActive(false); 
    }

    private void EndSubGame()
    {
        if (inSubGame)
        {
            Debug.Log("Player left the range of the car, sub-game canceled.");
            inSubGame = false;
            subGameUI.SetActive(false);
            pointerMiniGame.pointer.gameObject.SetActive(false);
            miniGameUI.SetActive(false);    
        }
    }

    public void ContinueMainGame()
    {
        if (inSubGame)
        {
            Debug.Log("Sub-game completed. Continuing main game.");
            inSubGame = false;
            subGameUI.SetActive(false);
            isPlaying = true;
        }
        else
        {
            Debug.LogError("Sub-game has not been activated. Cannot resume main game.");
        }
    }
}