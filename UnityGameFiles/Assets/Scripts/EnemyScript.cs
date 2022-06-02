using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private float health = 50f;
    private Animator animator;
    private Rigidbody[] ragDollBodies;
    private Collider[] ragDollColliders;
    private Vector3 explosionPos;
    [SerializeField]
    private Transform GunHolder;
    private EnemyBodyPart[] EnemyBodyPart;
    private bool died;
  
    public bool Died { get => died; set => died = value; }

    private void Start()
    {
        explosionPos = transform.position;
        animator = GetComponent<Animator>();
        ragDollBodies = GetComponentsInChildren<Rigidbody>();
        ragDollColliders = GetComponentsInChildren<Collider>();
        EnemyBodyPart = GetComponentsInChildren<EnemyBodyPart>();
       
        ToggleRagDoll(false);
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
       /* if (this.Died && gunHolder.GetComponentInChildren<GunScript>()!=null)
        {
            GunScript gun = gunHolder.GetComponentInChildren<GunScript>();
            gun.DropGunWhenDie();

        }*/
       
    }
    public void shoot()
    {

    }
    public void Move()
    {
        
    }

}
