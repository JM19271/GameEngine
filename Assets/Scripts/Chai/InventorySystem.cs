using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySystem : MonoBehaviour
{
    // 存储玩家的物品，使用 Dictionary 存储物品 ID 和名称
    private Dictionary<string, string> items = new Dictionary<string, string>();

    // UI 相关引用
    public GameObject inventoryUI;  // 引用 UI，用来显示和隐藏背包
    public TextMeshProUGUI inventoryText;   // Text 组件，用于显示物品内容

    private bool isInventoryOpen = false;  // 用于追踪背包是否打开

    void Start()
    {
        // 游戏开始时隐藏背包 UI
        inventoryUI.SetActive(false);
    }

    void Update()
    {
        // 按下 Tab 键，切换背包的显示状态
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isInventoryOpen = !isInventoryOpen;
            inventoryUI.SetActive(isInventoryOpen);
            UpdateInventoryUI();  // 确保 UI 每次显示时都更新
            Debug.Log("Inventory UI is now " + (isInventoryOpen ? "Open" : "Closed"));
        }
    }

    // 添加物品到背包
    public void AddItem(string itemID, string itemName)
    {
        // 检查物品是否已在背包中
        if (!items.ContainsKey(itemID))
        {
            items[itemID] = itemName;
            Debug.Log(itemName + " (" + itemID + ") added to inventory.");
            UpdateInventoryUI();  // 添加物品后更新 UI
        }
    }

    // 检查背包中是否有特定 ID 的物品
    public bool HasItem(string itemID)
    {
        return items.ContainsKey(itemID);
    }

    // 移除特定 ID 的物品
    public bool RemoveItem(string itemID)
    {
        if (items.ContainsKey(itemID))
        {
            items.Remove(itemID);  // 从背包中移除物品
            UpdateInventoryUI();    // 移除物品后更新 UI
            return true;  // 表示成功移除物品
        }
        return false;  // 表示背包中没有这个物品
    }

    // 更新 Inventory UI
    private void UpdateInventoryUI()
    {
        // 清空现有的文本内容
        inventoryText.text = "";

        // 遍历物品字典中的物品并添加到显示文本
        foreach (var item in items.Values)
        {
            inventoryText.text += item + "\n";  // 每个物品独占一行
        }

        Debug.Log("Inventory now contains: " + string.Join(", ", items.Values));  // 确认物品内容
    }

    // 获取特定物品的数量
    public int GetItemCount(string itemName)
    {
        int count = 0;
        foreach (string item in items.Values)
        {
            if (item == itemName)
            {
                count++;
            }
        }
        return count;
    }

    // 检查是否有所有指定的物品
    public bool HasRequiredItems(List<string> requiredItems)
    {
        foreach (string item in requiredItems)
        {
            if (!items.ContainsValue(item))
            {
                return false;  // 缺少某个必需的物品
            }
        }
        return true;  // 所有物品都已收集
    }
}
