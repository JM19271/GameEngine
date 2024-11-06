using UnityEngine;
using UnityEngine.UI;  // 引入UI库

public class GhostJumpscare : MonoBehaviour
{
    public Image jumpscareImage;  // 用于显示 jumpscare 的图片
    public GameObject player;     // 玩家对象
    public float jumpscareDuration = 3f;  // jumpscare 持续时间
    public AudioSource jumpscareAudio;    // jumpscare 音效 (可选)

    private bool hasJumpscared = false;   // 防止重复触发

    void Start()
    {
        // 确保图片一开始是不可见的
        if (jumpscareImage != null)
        {
            Color tempColor = jumpscareImage.color;
            tempColor.a = 0f;  // 设置 Alpha 为 0，图片不可见
            jumpscareImage.color = tempColor;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 检测是否与玩家发生碰撞
        if (other.gameObject == player && !hasJumpscared)
        {
            hasJumpscared = true;  // 确保 jumpscare 只触发一次
            TriggerJumpscare();
        }
    }

    void TriggerJumpscare()
    {
        // 显示 jumpscare 图片
        if (jumpscareImage != null)
        {
            Color tempColor = jumpscareImage.color;
            tempColor.a = 1f;  // 设置 Alpha 为 1，图片可见
            jumpscareImage.color = tempColor;
        }

        // 播放 jumpscare 音效
        if (jumpscareAudio != null)
        {
            jumpscareAudio.Play();
        }

        // 禁用玩家移动 (可选)
        if (player != null)
        {
            MOVEMENT playerMovement = player.GetComponent<MOVEMENT>();
            if (playerMovement != null)
            {
                playerMovement.enabled = false;  // 禁用玩家移动脚本
            }
        }

        // 在指定时间后隐藏图片
        Invoke("EndJumpscare", jumpscareDuration);
    }

    void EndJumpscare()
    {
        // 隐藏 jumpscare 图片
        if (jumpscareImage != null)
        {
            Color tempColor = jumpscareImage.color;
            tempColor.a = 0f;  // 设置 Alpha 为 0，隐藏图片
            jumpscareImage.color = tempColor;
        }

        // 恢复玩家的控制
        if (player != null)
        {
            MOVEMENT playerMovement = player.GetComponent<MOVEMENT>();
            if (playerMovement != null)
            {
                playerMovement.enabled = true;  // 启用玩家移动脚本
            }
        }

        hasJumpscared = false;  // 重置，允许后续 jumpscare
    }
}
