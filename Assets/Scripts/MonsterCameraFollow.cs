using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterCameraFollow : MonoBehaviour
{
    public Transform monster;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    public float followSpeed = 5f;
    private void LateUpdate()
    {
        if (monster != null)
        {
            Vector3 targetPosition = monster.position + positionOffset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

            Quaternion targetRotation = Quaternion.Euler(monster.eulerAngles + rotationOffset);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
        }
    }
}
