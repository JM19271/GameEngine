using UnityEngine;
using TMPro;

public class ItemPickup : MonoBehaviour
{
    public string itemName = "New Item";  // 物品名称
    public string itemID = "defaultID";   // 物品ID
    public InventorySystem inventorySystem;  // 引用 InventorySystem
    public TextMeshProUGUI pickUpText;    // 拾取提示文本
    private bool isInRange = false;       // 检查玩家是否在捡物品的范围内

    void Start()
    {
        if (pickUpText != null)
        {
            pickUpText.gameObject.SetActive(false);  // 初始隐藏拾取提示
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = true;
            if (pickUpText != null)
            {
                pickUpText.text = "Press E to pick up " + itemName;
                pickUpText.gameObject.SetActive(true);
            }
        }
    }

    void Update()
    {
        if (isInRange && Input.GetKeyDown(KeyCode.E))
        {
            if (inventorySystem != null)
            {
                inventorySystem.AddItem(itemID, itemName);  // 添加物品到背包
                Destroy(gameObject);  // 拾取后销毁物品
                if (pickUpText != null)
                {
                    pickUpText.gameObject.SetActive(false);
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInRange = false;
            if (pickUpText != null)
            {
                pickUpText.gameObject.SetActive(false);
            }
        }
    }
}