using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Objective : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;   // 任务的UI文本
    public InventorySystem inventory;       // Inventory系统的引用
    public List<string> requiredItemIDs = new List<string> { "001", "002", "003", "Item2" };
    public CarMiniGame carMiniGame;         // 引用 CarMiniGame，用于检查任务完成状态

    private bool isObjective1Active = false;
    private bool isObjective2Active = false;

    void Start()
    {
        objectiveText.gameObject.SetActive(false);  // 初始隐藏任务UI
    }

    // 激活任务1
    public void ActivateObjective1()
    {
        isObjective1Active = true;
        objectiveText.gameObject.SetActive(true);
        UpdateObjective1UI();
    }

    // 每帧检测物品和任务进度
    void Update()
    {
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
            // 检查任务2是否完成
            if (carMiniGame != null && carMiniGame.MaingameCompleted)
            {
                Debug.Log("Car MiniGame completed, marking Objective 2 as complete.");
                CompleteObjective2();  // 如果小游戏完成，标记任务2完成
            }
            else
            {
                objectiveText.text = "Objective 2: Fix the Car";  // 任务2的默认描述
            }
        }
    }

    // 更新任务1的UI显示
    private void UpdateObjective1UI()
    {
        int collectedCount = 0;
        foreach (string itemID in requiredItemIDs)
        {
            if (inventory.HasItem(itemID))
                collectedCount++;
        }
        objectiveText.text = "Objective 1: Collect 4 items (" + collectedCount + "/4)";
    }

    // 检查任务1是否完成
    private bool CheckObjective1Complete()
    {
        foreach (string itemID in requiredItemIDs)
        {
            if (!inventory.HasItem(itemID))
                return false;
        }
        return true;
    }

    // 任务1完成时调用
    private void CompleteObjective1()
    {
        objectiveText.text = "Objective 1 Complete!";
        isObjective1Active = false;
        isObjective2Active = true;  // 激活任务2
    }

    // 任务2完成时调用
    private void CompleteObjective2()
    {
        objectiveText.text = "Objective 2 Complete!!";
        isObjective2Active = false;  // 标记任务2完成
    }
}