using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    private NavMeshAgent agent;
    [SerializeField]
    private Transform target;
    [SerializeField]
    //private float stoppingDistance = 3;
    private Animator animator;
    private string walking = "walk";
    //private string speed = "speed";
    
    // Start is called before the first frame update
    void Start()
    {
        GetRefrences();
    }

    private void GetRefrences()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }
    private void MoveToTarget()
    {
        animator.SetBool(walking, true);
        agent.destination = target.position;
        //animator.SetFloat(speed,1f,0.3f,Time.deltaTime);
        RotateToTarget();
        float distanceToTarget = Vector3.Distance(target.position, transform.position);
        if(distanceToTarget<=agent.stoppingDistance)
        {
            animator.SetBool(walking, false);
            //animator.SetFloat(speed, 0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        MoveToTarget();
    }

    private void RotateToTarget()
    {
        transform.LookAt(target);
       
    }
   
}
