using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LetterInteraction : MonoBehaviour
{
    public float interactionDistance;
    public GameObject interactionText;
    public LayerMask interactionLayers;

    public bool HasInteracted { get; private set; } // 用于检测玩家是否已与信件交互

    void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance, interactionLayers))
        {
            if (hit.collider.gameObject.GetComponent<letter>())
            {
                interactionText.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    hit.collider.gameObject.GetComponent<letter>().openCloseLetter();
                    HasInteracted = true; // 设置为 true 表示已与信件交互
                }
            }
            else
            {
                interactionText.SetActive(false);
            }
        }
        else
        {
            interactionText.SetActive(false);
        }
    }
}
