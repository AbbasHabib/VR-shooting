using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private float targetEnemyDistanceAllowance = 6.0f;

    private float startYPos;
    [SerializeField]
    private bool staticPlayer = false;


    void Awake()
    {
        animator = GetComponent<Animator>();
        startYPos = gameObject.transform.position.y;
    }
    private void MoveToTarget()
    {
        float distance = Vector3.Distance(this.gameObject.transform.position, target.transform.position);
        if(distance < targetEnemyDistanceAllowance)
        {
            animator.SetBool("walk", false);
        }
        else
        {
            animator.SetBool("walk", true);
            Vector3 goTo = new Vector3(target.transform.position.x, startYPos, target.transform.position.z);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("walk")) 
                transform.position = Vector3.Lerp(transform.position, goTo, Time.deltaTime * 0.3f);
        }
    }

    void Update()
    {
        if(!GetComponent<EnemyScript>().Died && !staticPlayer)
            MoveToTarget();

        RotateToTarget();

    }

    private void RotateToTarget()
    {
        transform.LookAt(target);
    }

}
