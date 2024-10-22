using UnityEngine;
using UnityEngine.UI;

public class PointerMiniGame : MonoBehaviour
{
    public Slider pointerSlider;  // 滑动指针的路径
    public RectTransform pointer;  // 指针的UI物体
    public RectTransform greenZone; // 青色区域
    public CarMiniGame carMiniGame; // CarMiniGame 的引用

    public float pointerSpeed = 5f;  // 指针移动速度
    private bool movingRight = true;  // 判断指针移动方向
    private bool gameCompleted = false; // 防止多次调用ContinueMainGame

    public bool IsInGreenZone()
    {
        // 判断指针是否在青色区域内
        return RectTransformUtility.RectangleContainsScreenPoint(greenZone, pointer.position);
    }

    void Update()
    {
        if (!gameCompleted) // 检查游戏是否已经完成
        {
            MovePointer();

            // 当玩家按下F键并且指针在青色区域时，小游戏成功
            if (Input.GetKeyDown(KeyCode.F) && IsInGreenZone())
            {
                Debug.Log("指针在青色区域，修理进度继续。");
                ContinueMainGame();
            }
        }
    }

    void MovePointer()
    {
        // 指针左右来回移动
        if (movingRight)
        {
            pointerSlider.value += Time.deltaTime * pointerSpeed;
            if (pointerSlider.value >= 1f)
            {
                movingRight = false;
            }
        }
        else
        {
            pointerSlider.value -= Time.deltaTime * pointerSpeed;
            if (pointerSlider.value <= 0f)
            {
                movingRight = true;
            }
        }
    }

    public void ContinueMainGame()
    {
        if (!gameCompleted)
        {
            gameCompleted = true; // 防止重复调用
            // 调用 CarMiniGame 的 ContinueMainGame 方法
            carMiniGame.ContinueMainGame();
            Debug.Log($"调用 CarMiniGame 的 ContinueMainGame()，inSubGame={carMiniGame.inSubGame}, isPlaying={carMiniGame.isPlaying}");

            pointer.gameObject.SetActive(false); // 隐藏指针
            pointerSlider.gameObject.SetActive(false); // 隐藏滑动指针的路径
            greenZone.gameObject.SetActive(false); // 隐藏青色区域
        }
        else
        {
            Debug.LogWarning("小游戏已经完成，不能重复继续！");
        }
    }
}
