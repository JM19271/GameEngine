using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Objective : MonoBehaviour
{
    public TextMeshProUGUI objectiveText; // 任务UI文本
    public InventorySystem inventory;     // Inventory系统的引用
    public List<string> carPartIDs = new List<string> { "CarPart1", "CarPart2" }; // 汽车零件的ID
    public LetterInteraction letterInteraction; // 信件交互脚本的引用
    public CarMiniGame carMiniGame;       // 引用 CarMiniGame，用于检查任务完成状态

    private bool isObjective1Active = false;
    private bool isObjective2Active = false;
    private bool isObjective3Active = false;
    private bool isObjectiveVisible = false; // 标记UI是否可见

    void Start()
    {
        objectiveText.gameObject.SetActive(false); // 初始隐藏任务UI
    }

    // 激活任务1
    public void ActivateObjective1()
    {
        isObjective1Active = true;
        objectiveText.gameObject.SetActive(true);
        isObjectiveVisible = true; // 更新可见性状态
        UpdateObjective1UI();
    }

    // 每帧检测物品和任务进度
    void Update()
    {
        // 检测按下“G”键来开启/关闭任务UI
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleObjectiveVisibility();
        }

        if (isObjective1Active)
        {
            UpdateObjective1UI();
            if (CheckObjective1Complete())
            {
                CompleteObjective1();
            }
        }
        else if (isObjective2Active)
        {
            // 检查玩家是否与信件进行了交互
            if (letterInteraction != null && letterInteraction.HasInteracted)
            {
                Debug.Log("已找到信件，标记任务2完成。");
                CompleteObjective2();
            }
            else
            {
                objectiveText.text = "Objective 2: find the letter"; // 任务2的默认描述
            }
        }
        else if (isObjective3Active)
        {
            // 检查汽车小游戏是否完成
            if (carMiniGame != null && carMiniGame.MaingameCompleted)
            {
                Debug.Log("汽车已修复，标记任务3完成。");
                CompleteObjective3();
            }
            else
            {
                objectiveText.text = "Objective 3: Fix Car"; // 任务3的默认描述
            }
        }
    }

    // 更新任务1的UI显示
    private void UpdateObjective1UI()
    {
        int collectedCount = 0;
        foreach (string itemID in carPartIDs)
        {
            if (inventory.HasItem(itemID))
                collectedCount++;
        }
        objectiveText.text = "Objective 1: find 2 car parts(" + collectedCount + "/2)";
    }

    // 检查任务1是否完成
    private bool CheckObjective1Complete()
    {
        foreach (string itemID in carPartIDs)
        {
            if (!inventory.HasItem(itemID))
                return false;
        }
        return true;
    }

    // 任务1完成时调用
    private void CompleteObjective1()
    {
        objectiveText.text = "Objective 1 Complete!!!";
        isObjective1Active = false;
        isObjective2Active = true; // 激活任务2
    }

    // 任务2完成时调用
    private void CompleteObjective2()
    {
        objectiveText.text = "Objective 2 Complete!!!";
        isObjective2Active = false;
        isObjective3Active = true; // 激活任务3
    }

    // 任务3完成时调用
    private void CompleteObjective3()
    {
        objectiveText.text = "Objective Complete!!!";
        isObjective3Active = false; // 标记任务3完成
    }

    // 切换任务UI的可见性
    private void ToggleObjectiveVisibility()
    {
        isObjectiveVisible = !isObjectiveVisible;
        objectiveText.gameObject.SetActive(isObjectiveVisible);
    }
}
