using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorDeathTrigger : MonoBehaviour
{
    public GameObject warrior;
    private Animator warriorAnim;
    private WarriorStatusController warriorStatusController;
    void Start()
    {
        warriorAnim = warrior.GetComponent<Animator>();
        warriorStatusController = warrior.GetComponent<WarriorStatusController>();

        if (warriorAnim == null)
        {
            Debug.LogError("Animator warriorAnim is not found");
        }

        if (warriorStatusController == null)
        {
            Debug.LogError("WarriorStatusController warriorStatusController is not found");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.gameObject.tag == "Player")
        {
            warriorStatusController.health = 0.0f;
            warriorAnim.SetTrigger("died");
        }
    }
}
