using UnityEngine;
using TMPro;  // 使用TextMeshPro

public class ItemTrigger : MonoBehaviour
{
    public string requiredItem = "Cube";  // 需要放置的物品名称
    public Transform placementPoint;      // 放置物品的位置
    public TextMeshProUGUI placeItemText; // UI文本，用于提示玩家放置物品

    private bool playerInRange = false;   // 检查玩家是否在触发范围内

    void Start()
    {
        // 确保初始状态下UI是不可见的
        placeItemText.gameObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;  // 玩家进入放置区域
            placeItemText.text = "Press F to place the Cube";  // 提示玩家可以放置物品
            placeItemText.gameObject.SetActive(true);  // 显示提示信息
            Debug.Log("玩家进入了放置物品的区域。");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;  // 玩家离开放置区域
            placeItemText.gameObject.SetActive(false);  // 隐藏提示信息
            Debug.Log("玩家离开了放置物品的区域。");
        }
    }

    public bool IsPlayerInRange()
    {
        return playerInRange;  // 返回玩家是否在触发范围内
    }

    // 禁用触发器的提示信息
    public void DisableTriggerText()
    {
        placeItemText.gameObject.SetActive(false);  // 隐藏提示信息
        Debug.Log("提示信息已隐藏。");
    }
}
