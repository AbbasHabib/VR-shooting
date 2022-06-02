using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAi : MonoBehaviour
{
    Transform target;
    public NavMeshAgent agent;
    public Transform player;
    public float rotationSpeed = 10f;
    private string walking = "walk";
    private Animator animator;
    public LayerMask whatIsGround, whatIsPLayer;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        GameObject player = GameObject.Find("Player");
        target = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowards(target);
        RotateTowards(target);
    }
    private void MoveTowards(Transform target)
    {
        animator.SetBool(walking, true);
        agent.SetDestination(target.position);
    }

    private void RotateTowards(Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }
}
