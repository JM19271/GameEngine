using UnityEngine;
using TMPro;

public class ObjectiveSystem : MonoBehaviour
{
    public TextMeshProUGUI objectiveText;  // 引用显示任务的UI
    public InventorySystem inventory;      // 引用玩家的Inventory系统
    private int collectedItems = 0;        // 已经收集的物品数量
    private int requiredItems = 3;         // 所需物品数量
    public string requiredItemName = "Material";  // 需要的物品名称

    private bool isObjectiveActive = false;  // 检查任务是否激活

    void Start()
    {
        objectiveText.gameObject.SetActive(false);  // 初始隐藏任务UI
    }

    // 激活任务
    public void ActivateObjective()
    {
        isObjectiveActive = true;
        objectiveText.gameObject.SetActive(true);
        UpdateObjectiveUI();
    }

    // 检测物品并更新任务进度
    void Update()
    {
        if (isObjectiveActive)
        {
            // 获取物品数量并更新任务进度
            collectedItems = inventory.GetItemCount(requiredItemName);
            UpdateObjectiveUI();

            // 当物品收集完成时，隐藏任务
            if (collectedItems >= requiredItems)
            {
                CompleteObjective();
            }
        }
    }

    // 更新任务UI显示
    private void UpdateObjectiveUI()
    {
        objectiveText.text = "Objective 1: Collect 3 items (" + collectedItems + "/" + requiredItems + ")";
    }

    // 任务完成时调用
    private void CompleteObjective()
    {
        objectiveText.text = "Objective Complete!";
        // 在这里可以添加下一个任务或其他逻辑
        // 例如，隐藏任务UI或激活下一个任务目标
    }
}
