using UnityEngine;
using TMPro;  // 引入 TextMeshPro 命名空间

public class CarTrigger : MonoBehaviour
{
    private bool playerInRange = false;  // 检测玩家是否在范围内
    public TextMeshProUGUI hintTextTMP;  // 引用 TextMeshPro 提示文本

    // 当玩家进入触发范围时
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("玩家接近了车。");

            // 显示 TMP 提示文本并设置内容
            if (hintTextTMP != null)
            {
                hintTextTMP.text = "Press F to Repair Car"; // 设置提示文本内容
                hintTextTMP.gameObject.SetActive(true); // 显示提示文本
            }
        }
    }

    // 当玩家离开触发范围时
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("玩家离开了车的范围。");

            // 隐藏 TMP 提示文本
            if (hintTextTMP != null)
            {
                hintTextTMP.gameObject.SetActive(false);
            }
        }
    }

    // 用于其他脚本调用，检测玩家是否在范围内
    public bool IsPlayerInRange()
    {
        return playerInRange;
    }
}
