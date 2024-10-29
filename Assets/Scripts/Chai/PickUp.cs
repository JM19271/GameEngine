using UnityEngine;
using TMPro;  // 使用TextMeshPro，确保你导入了这个命名空间

public class ItemPickup : MonoBehaviour
{
    public string itemName = "New Item";  // 物品的名称
    public InventorySystem inventorySystem;  // 引用 InventorySystem
    public TextMeshProUGUI pickUpText;  // 引用拾取提示文本
    private bool isInRange = false;  // 检查玩家是否在捡物品的范围内

    void Start()
    {
        // 确保提示文本在游戏开始时是不可见的
        if (pickUpText != null)
        {
            pickUpText.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            Debug.Log("Player is in range to pick up item.");  // 输出日志，检查是否检测到玩家

            // 显示拾取提示文本
            if (pickUpText != null)
            {
                pickUpText.text = "Press E to pick up " + itemName;
                pickUpText.gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        // 当玩家在范围内并按下E键时，拾取物品
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pick up key pressed!");  // 输出日志，确认玩家按下了 "E" 键

            InventorySystem inventory = FindObjectOfType<InventorySystem>();  // 获取 InventorySystem
            if (inventory != null)
            {
                inventory.AddItem(itemName);  // 添加物品到背包
                Debug.Log(itemName + " added to inventory!");  // 输出日志，确认物品已添加到背包
                Destroy(gameObject);  // 拾取后销毁物品

                // 隐藏提示文本
                if (pickUpText != null)
                {
                    pickUpText.gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.LogError("Inventory System not found!");  // 输出错误日志，检查 InventorySystem 是否存在
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;  // 当玩家离开范围时，取消拾取状态
            Debug.Log("Player is out of range to pick up item.");

            // 隐藏拾取提示文本
            if (pickUpText != null)
            {
                pickUpText.gameObject.SetActive(false);
            }
        }
    }
}