using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Objective : MonoBehaviour
{
    public TextMeshProUGUI objectiveText; // 任务UI文本
    public InventorySystem inventory;     // Inventory系统的引用
    public List<string> carPartIDs = new List<string> { "CarPart1", "CarPart2" }; // 汽车零件的ID
    public LetterInteraction letterInteraction; // 信件交互脚本的引用
    public CarMiniGame carMiniGame;       // 修车小游戏的引用

    public string keyID = "key"; // 钥匙的ID
    public string lastPartID = "003"; // 最后一部分的ID

    private int currentObjective = 0; // 当前任务编号
    private bool isObjectiveVisible = false; // UI是否可见

    void Start()
    {
        objectiveText.gameObject.SetActive(false); // 初始隐藏任务UI
    }

    void Update()
    {
        // 按下 "G" 键切换任务UI可见性
        if (Input.GetKeyDown(KeyCode.G))
        {
            ToggleObjectiveVisibility();
        }

        // 根据当前任务编号更新任务逻辑
        if (currentObjective > 0)
        {
            switch (currentObjective)
            {
                case 1: UpdateObjective1(); break;
                case 2: UpdateObjective2(); break;
                case 3: UpdateObjective3(); break;
                case 4: UpdateObjective4(); break;
                case 5: UpdateObjective5(); break;
            }
        }
    }

    public void ActivateObjective1()
    {
        currentObjective = 1;
        objectiveText.gameObject.SetActive(true);
        UpdateObjective1();
    }

    private void UpdateObjective1()
    {
        int collectedCount = 0;
        foreach (string itemID in carPartIDs)
        {
            if (inventory.HasItem(itemID))
                collectedCount++;
        }

        objectiveText.text = "Objective 1: find 2 car parts (" + collectedCount + "/2)";

        if (collectedCount >= 2)
        {
            CompleteObjective("Objective 1 Complete!!!", 2);
        }
    }

    private void UpdateObjective2()
    {
        objectiveText.text = "Objective 2: find the letter";

        if (letterInteraction != null && letterInteraction.HasInteracted)
        {
            CompleteObjective("Objective 2 Complete!!!", 3);
        }
    }

    private void UpdateObjective3()
    {
        objectiveText.text = "Objective 3: find the key";

        if (inventory.HasItem(keyID))
        {
            CompleteObjective("Objective 3 Complete!!!", 4);
        }
    }

    private void UpdateObjective4()
    {
        objectiveText.text = "Objective 4: find the last part (0/1)";

        if (inventory.HasItem(lastPartID))
        {
            CompleteObjective("Objective 4 Complete!!!", 5);
        }
    }

    private void UpdateObjective5()
    {
        objectiveText.text = "Objective 5: Fix the car";

        if (carMiniGame != null && carMiniGame.MaingameCompleted)
        {
            CompleteObjective("All objectives completed!", 0); // 0表示任务结束
        }
    }

    private void CompleteObjective(string message, int nextObjective)
    {
        Debug.Log(message);
        objectiveText.text = message;

        currentObjective = nextObjective;

        // 如果任务结束，隐藏UI
        if (nextObjective == 0)
        {
            Invoke(nameof(HideObjectiveUI), 3f); // 3秒后隐藏
        }
    }

    private void HideObjectiveUI()
    {
        objectiveText.gameObject.SetActive(false);
    }

    private void ToggleObjectiveVisibility()
    {
        isObjectiveVisible = !isObjectiveVisible;
        objectiveText.gameObject.SetActive(isObjectiveVisible);
    }
}