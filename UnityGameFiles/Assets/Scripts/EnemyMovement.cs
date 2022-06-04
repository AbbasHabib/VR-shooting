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


    void Awake()
    {
        animator = GetComponent<Animator>();
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
            Vector3 goTo = new Vector3(target.transform.position.x, 0.59f, target.transform.position.z);
            transform.position = Vector3.Lerp(transform.position, goTo, Time.deltaTime * 0.3f);
        }

        RotateToTarget();
    }

    void Update()
    {
        if(!GetComponent<EnemyScript>().Died)
            MoveToTarget();
    }

    private void RotateToTarget()
    {
        transform.LookAt(target);
    }

}
