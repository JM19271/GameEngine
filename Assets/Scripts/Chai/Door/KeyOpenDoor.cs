using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // 引入 TextMeshPro 命名空间

public class KeyOpenDoor : MonoBehaviour
{
    [SerializeField]
    public GameObject Door; // 门的游戏对象（Door_hinge 的子对象）
    [SerializeField]
    private Transform DoorHinge; // 门的铰链对象
    [SerializeField]
    private float doorOpenAngle = 90f; // 开门时的旋转角度
    [SerializeField]
    private float doorMoveDuration = 1f; // 开门动画的持续时间

    private Quaternion originalDoorRotation; // 门铰链的原始旋转角度
    private bool isDoorOpen = false; // 判断门是否已经打开

    // 引用玩家的 Inventory 类
    public InventorySystem playerInventory;
    [SerializeField]
    public string requiredItemID; // 开门所需的物品 ID

    // TextMeshPro 组件
    [SerializeField]
    private TextMeshProUGUI interactPromptTMP; // 显示“按‘F’开门”的提示
    [SerializeField]
    private TextMeshProUGUI noKeyTMP; // 显示“没有对应的钥匙，无法开门”的提示

    [SerializeField]
    private AudioClip doorOpenSound;
    [SerializeField]
    private AudioSource audioSource;

    private void Start()
    {
        // 存储门铰链的原始旋转角度
        originalDoorRotation = DoorHinge.rotation;

        // 获取玩家的 Inventory（假设它附加在玩家对象上）
        playerInventory = FindObjectOfType<InventorySystem>();

        // 确保 TMP 提示最初是隐藏的
        interactPromptTMP.gameObject.SetActive(false);
        noKeyTMP.gameObject.SetActive(false);

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            Debug.Log("AudioSource was missing; added automatically.");
        }

        if (doorOpenSound == null)
        {
            doorOpenSound = Resources.Load<AudioClip>("Opendoor");
            Debug.Log("Assigned default door open sound.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞对象是否为玩家
        if (other.CompareTag("Player"))
        {
            // 显示“按‘F’开门”的提示
            interactPromptTMP.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 检查碰撞对象是否为玩家
        if (other.CompareTag("Player"))
        {
            // 隐藏“按‘F’开门”的提示
            interactPromptTMP.gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 检查碰撞对象是否为玩家并且门未打开
        if (other.CompareTag("Player") && !isDoorOpen)
        {
            // 检测玩家按下“F”键
            if (Input.GetKeyDown(KeyCode.E))
            {
                // 检查玩家的 Inventory 中是否有所需的物品 ID
                if (playerInventory != null && playerInventory.HasItem(requiredItemID))
                {
                    // 开始执行开门的协程
                    StartCoroutine(OpenDoor());
                }
                else
                {
                    // 显示“没有对应的钥匙，无法开门”的提示2秒钟
                    StartCoroutine(ShowNoKeyMessage());
                }
            }
        }
    }

    // 协程：平滑地打开门
    private IEnumerator OpenDoor()
    {
        // Play sound as soon as the player presses 'E'
        if (doorOpenSound != null)
        {
            audioSource.PlayOneShot(doorOpenSound);
        }
        else
        {
            Debug.LogWarning("No door open sound assigned. Please assign an audio clip.");
        }

        isDoorOpen = true;
        interactPromptTMP.gameObject.SetActive(false); // 隐藏交互提示
        Quaternion startRotation = DoorHinge.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, doorOpenAngle, 0);
        float elapsedTime = 0;

        while (elapsedTime < doorMoveDuration)
        {
            DoorHinge.rotation = Quaternion.Slerp(startRotation, endRotation, elapsedTime / doorMoveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // 确保门完全打开到最终角度
        DoorHinge.rotation = endRotation;
    }

    // 协程：显示“没有对应的钥匙，无法开门”的提示2秒钟
    private IEnumerator ShowNoKeyMessage()
    {
        noKeyTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f); // 显示2秒
        noKeyTMP.gameObject.SetActive(false);
    }
}
