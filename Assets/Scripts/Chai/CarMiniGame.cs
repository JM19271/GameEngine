using System.Collections.Generic;
using UnityEngine;
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

    public bool isPlaying = false;
    private bool canStartGame = false;
    public bool inSubGame = false;
    [SerializeField]
    private float progress = 0f;


    void Start()
    {
        miniGameUI.SetActive(false);
        subGameUI.SetActive(false);
    }

    void Update()
    {
        if (carTrigger.IsPlayerInRange())
        {
            if (inventory.HasRequiredItems(requiredItems))
            {
                canStartGame = true;
                Debug.Log("玩家已收集所有必需物品，可以开始小游戏。");

                if (Input.GetKeyDown(KeyCode.F) && !isPlaying && !inSubGame)
                {
                    StartMiniGame();
                }
            }
            else
            {
                canStartGame = false;
                Debug.Log("物品未收集齐全，不能开始小游戏。");
            }
        }
        else if (inSubGame)
        {
            Debug.Log("玩家离开了车的范围，指针小游戏取消。");
            inSubGame = false;
            subGameUI.SetActive(false);
            pointerMiniGame.pointer.gameObject.SetActive(false);
            miniGameUI.SetActive(false);
        }
        else
        {
            if (isPlaying)
            {
                EndMiniGame();
                Debug.Log("玩家离开了车的范围，主修理小游戏被取消。");
            }
        }

        Debug.Log($"当前状态: inSubGame={inSubGame}, isPlaying={isPlaying}, progress={progress}");


        if (isPlaying && progress >= 0.5f && !inSubGame)
        {
            if (!pointerMiniGame.gameCompleted)
            {
                PauseProgressAndStartSubGame();
            }
        }

        if (isPlaying && carTrigger.IsPlayerInRange() && !inSubGame)
        {
            if (Input.GetKey(KeyCode.F))
            {
                miniGameUI.SetActive(true);
                progress += Time.deltaTime * 0.2f;
                progressBar.value = progress;


                if (progress >= 1f)
                {
                    EndMiniGame();
                }
            }
        }
    }

    public void StartMiniGame()
    {
        isPlaying = true;
        miniGameUI.SetActive(true);
        progressBar.value = progress;
        Debug.Log("主修理小游戏开始。");
    }

    void PauseProgressAndStartSubGame()
    {
        Debug.Log("修理进度达到25%，暂停主修理并开始指针小游戏。");
        isPlaying = false;
        inSubGame = true;
        subGameUI.SetActive(true);
        pointerMiniGame.pointer.gameObject.SetActive(true);
    }

    void EndMiniGame()
    {
        Debug.Log("主修理小游戏结束，隐藏UI。");
        isPlaying = false;
        miniGameUI.SetActive(false);
    }

    public void ContinueMainGame()
    {
        Debug.Log($"inSubGame={inSubGame}, isPlaying={isPlaying}. 准备继续主修理游戏。");

        if (inSubGame)
        {
            inSubGame = false;
            subGameUI.SetActive(false);
            isPlaying = true;
            Debug.Log($"inSubGame 设置为 false，isPlaying 设置为 true. inSubGame={inSubGame}, isPlaying={isPlaying}.");
        }
        else
        {
            Debug.LogError("指针小游戏未激活，无法恢复主修理进度！");
        }
    }
}
    