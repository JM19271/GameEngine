using UnityEngine;

public class ItemPlacer : MonoBehaviour
{
    public InventorySystem inventory;   // 引用玩家的背包系统
    public GameObject cubePrefab;       // 需要放置的Cube预制件
    public ItemTrigger currentTrigger;  // 当前触发器，表示玩家所在的区域

    void Update()
    {
        if (currentTrigger != null && currentTrigger.IsPlayerInRange() && Input.GetKeyDown(KeyCode.F))
        {
            // 检查背包中是否有需要放置的物品
            if (inventory.HasItem(currentTrigger.requiredItem))
            {
                // 在指定位置放置物品并使其不可移动
                GameObject placedCube = Instantiate(cubePrefab, currentTrigger.placementPoint.position, Quaternion.identity);
                Debug.Log("已放置Cube！");

                // 禁用物体的Rigidbody使其不可移动（如果物体有Rigidbody）
                Rigidbody rb = placedCube.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;  // 禁用物理移动
                }

                // 从背包中移除物品
                inventory.RemoveItem(currentTrigger.requiredItem);

                // 隐藏触发器的提示信息
                currentTrigger.DisableTriggerText();
            }
            else
            {
                Debug.Log("你没有所需的物品！");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 当玩家进入触发器范围时，记录当前触发器
        if (other.GetComponent<ItemTrigger>() != null)
        {
            currentTrigger = other.GetComponent<ItemTrigger>();
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 当玩家离开触发器范围时，清除当前触发器
        if (other.GetComponent<ItemTrigger>() != null)
        {
            currentTrigger = null;
        }
    }
}
