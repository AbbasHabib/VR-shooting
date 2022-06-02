using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float health = 50f;
    private Animator animator;
    private Rigidbody[] ragDollBodies;
    [SerializeField]
    private Transform GunHolder;
    [SerializeField]
    private float shootingInterval;
    private EnemyBodyPart[] EnemyBodyPart;
    private bool died;
  
    public bool Died { get => died; set => died = value; }

    private void Awake()
    {
        //ToggleRagDoll(false);
        animator = GetComponent<Animator>();
        ragDollBodies = GetComponentsInChildren<Rigidbody>();
        EnemyBodyPart = GetComponentsInChildren<EnemyBodyPart>();

        InvokeRepeating("ShootAtPlayer", shootingInterval, shootingInterval);  //1s delay, repeat every 1s
    }


    private void ShootAtPlayer()
    {
        Debug.Log("Enemy shooting!!");
    }

    public void ToggleRagDoll(bool state)
    {
        
        animator.enabled = !state;

        foreach (Rigidbody rb in ragDollBodies)
        {
            rb.isKinematic = !state;
            rb.interpolation = RigidbodyInterpolation.Interpolate; 
        }
        if (state)
        {
            foreach (EnemyBodyPart enemyBodyParts in EnemyBodyPart)
            {
                enemyBodyParts.DestroyBodyPart();
            }
        }
        Destroy(this.gameObject, 10f);
       
    }
}
