using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InventorySystem : MonoBehaviour
{
    // 存储玩家的物品
    public List<string> inventory = new List<string>();  // 用于存储物品的列表
    public GameObject inventoryUI;  // 引用UI，用来显示和隐藏背包
    public TextMeshProUGUI inventoryText;   // Text组件，用于显示物品内容

    private bool isInventoryOpen = false;  // 用于追踪背包是否打开


    void Start()
    {
        // 游戏开始时隐藏背包UI
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
    public void AddItem(string itemName)
    {
        Debug.Log("Adding item to inventory: " + itemName);
        inventory.Add(itemName);  // 将物品添加到inventory列表
        UpdateInventoryUI();      // 更新UI显示
    }

    public bool HasItem(string itemName)
    {
        return inventory.Contains(itemName);
    }

    // 更新Inventory UI
    private void UpdateInventoryUI()
    {
        // 清空现有的文本内容
        inventoryText.text = "";

        // 遍历背包中的物品并添加到显示文本
        foreach (string item in inventory)
        {
            inventoryText.text += item + "\n";  // 每个物品独占一行
        }

        Debug.Log("Inventory now contains: " + string.Join(", ", inventory));  // 确认物品内容
    }

    // 获取物品数量
    public int GetItemCount(string itemName)
    {
        int count = 0;
        foreach (string item in inventory)
        {
            if (item == itemName)
            {
                count++;
            }
        }
        return count;
    }
}
