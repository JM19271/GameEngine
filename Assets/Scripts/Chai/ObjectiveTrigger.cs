using UnityEngine;

public class ObjectiveTrigger : MonoBehaviour
{
    public Objective objective;  // 引用Objective Manager

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            objective.ActivateObjective1();  // 激活任务
            Destroy(gameObject);  // 触发后销毁触发器，防止重复激活
        }
    }
}