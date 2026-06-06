using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiantBallCollision : MonoBehaviour
{
    public GameObject warrior;
    private Animator warriorAnim;
    private WarriorStatusController warriorStatusController;
    
    void Start()
    {
        warriorAnim = warrior.GetComponent<Animator>();
        warriorStatusController = warrior.GetComponent<WarriorStatusController>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.tag == "Player")
        {
            // Only kill the warrior if the ball rolls fast enough, where it is only possible/fast enough when the giant
            // ball is rolling down the ramp.
            if (collision.impulse.magnitude > 10.0f)
            {
                warriorStatusController.health = 0.0f;
                warriorAnim.SetTrigger("died");
            }
            
        }
    }
}
