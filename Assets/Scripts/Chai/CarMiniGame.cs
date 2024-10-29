using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CarMiniGame : MonoBehaviour
{
    public CarTrigger carTrigger;      // 车的触发器
    public InventorySystem inventory;  // 玩家背包系统
    public List<string> requiredItems; // 三个需要的物品
    public GameObject miniGameUI;      // 小游戏的UI，包括进度条
    public Slider progressBar;         // 进度条
    public GameObject subGameUI;       // 指针滑动小游戏的UI
    public PointerMiniGame pointerMiniGame; // 指针小游戏的引用

    public bool isPlaying = false;     // 检查是否正在进行主修理小游戏
    private bool canStartGame = false; // 是否可以启动主修理小游戏
    public bool inSubGame = false;     // 是否正在进行指针滑动小游戏
    private float progress = 0f;       // 进度值

    void Start()
    {
        // 在游戏开始时隐藏主修理小游戏和指针滑动小游戏的UI
        miniGameUI.SetActive(false);
        subGameUI.SetActive(false);
    }

    void Update()
    {
        // 检查玩家是否在车的范围内
        if (carTrigger.IsPlayerInRange())
        {
            // 检查玩家是否拥有所需的物品
            if (inventory.HasRequiredItems(requiredItems))
            {
                canStartGame = true; // 玩家可以开始游戏
                Debug.Log("玩家已收集所有必需物品，可以开始小游戏。");

                // 按下 F 键开始主修理小游戏
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
            inSubGame = false;  // 退出指针滑动小游戏
            subGameUI.SetActive(false); // 隐藏指针小游戏的UI
            pointerMiniGame.pointer.gameObject.SetActive(false); // 隐藏指针
            miniGameUI.SetActive(false); // 隐藏主修理小游戏的UI
        }
        else
        {
            // 玩家离开了车的范围，处理小游戏的隐藏
            if (isPlaying)
            {
                EndMiniGame(); // 结束小游戏
                Debug.Log("玩家离开了车的范围，主修理小游戏被取消。");
            }
        }

        // 输出状态调试信息
        Debug.Log($"当前状态: inSubGame={inSubGame}, isPlaying={isPlaying}, progress={progress}");

        // 如果游戏正在进行，并且修理进度达到25%，暂停修理并触发新的小游戏
        if (isPlaying && progress >= 0.25f && !inSubGame)
        {
            PauseProgressAndStartSubGame();
        }

        // 如果主修理小游戏正在进行，并且玩家在车的范围内，按住F键让进度条前进
        if (isPlaying && carTrigger.IsPlayerInRange() && !inSubGame)
        {
            if (Input.GetKey(KeyCode.F))
            {
                miniGameUI.SetActive(true);
                progress += Time.deltaTime * 0.2f;  // 控制进度条前进速度
                progressBar.value = progress;

                // 如果修理进度达到100%，结束小游戏
                if (progress >= 1f)
                {
                    EndMiniGame();
                }
            }
        }
    }

    // 开始主修理小游戏
    public void StartMiniGame()
    {
        isPlaying = true;
        miniGameUI.SetActive(true);  // 显示主修理小游戏UI（进度条）
        progressBar.value = progress; // 将当前进度值应用于进度条
        Debug.Log("主修理小游戏开始。");
    }

    // 修理进度到达25%，暂停修理并启动指针滑动小游戏
    void PauseProgressAndStartSubGame()
    {
        Debug.Log("修理进度达到25%，暂停主修理并开始指针小游戏。");
        isPlaying = false;  // 暂停修理进度
        inSubGame = true;   // 进入指针滑动小游戏
        subGameUI.SetActive(true);  // 显示指针滑动小游戏的UI
        pointerMiniGame.pointer.gameObject.SetActive(true); // 显示指针
    }

    // 结束主修理小游戏
    void EndMiniGame()
    {
        Debug.Log("主修理小游戏结束，隐藏UI。");
        isPlaying = false;
        miniGameUI.SetActive(false); // 隐藏主修理小游戏UI
    }

    // 完成指针滑动小游戏，继续主修理小游戏
    public void ContinueMainGame()
    {
        Debug.Log($"inSubGame={inSubGame}, isPlaying={isPlaying}. 准备继续主修理游戏。");

        if (inSubGame)  // 检查是否确实在指针小游戏中
        {
            inSubGame = false;   // 退出指针滑动小游戏
            subGameUI.SetActive(false); // 隐藏指针滑动小游戏的UI
            isPlaying = true;    // 恢复主修理小游戏
            Debug.Log($"inSubGame 设置为 false，isPlaying 设置为 true. inSubGame={inSubGame}, isPlaying={isPlaying}.");
        }
        else
        {
            Debug.LogError("指针小游戏未激活，无法恢复主修理进度！");
        }
    }
}
