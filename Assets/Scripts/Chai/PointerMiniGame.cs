using UnityEngine;
using UnityEngine.UI;

public class PointerMiniGame : MonoBehaviour
{
    public Slider pointerSlider;
    public RectTransform pointer;
    public RectTransform greenZone;
    public CarMiniGame carMiniGame;

    public float pointerSpeed = 5f;
    private bool movingRight = true;
    public bool gameCompleted = false;

    public bool IsInGreenZone()
    {

        return RectTransformUtility.RectangleContainsScreenPoint(greenZone, pointer.position);
    }

    void Update()
    {
        if (!gameCompleted)
        {
            MovePointer();


            if (Input.GetKeyDown(KeyCode.F) && IsInGreenZone())
            {
                Debug.Log("指针在青色区域，修理进度继续。");
                ContinueMainGame();
            }
        }
    }

    void MovePointer()
    {

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
            gameCompleted = true;
            carMiniGame.ContinueMainGame();

            pointer.gameObject.SetActive(false);
            pointerSlider.gameObject.SetActive(false);
            greenZone.gameObject.SetActive(false);
        }
        else
        {
            Debug.LogWarning("小游戏已经完成，不能重复继续！");
        }
    }
}
