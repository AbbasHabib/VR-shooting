using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyScript : MonoBehaviour, IDamageable
{
    //[SerializeField]
    //private float health = 50f;
    private Animator animator;
    private Rigidbody[] ragDollBodies;
    [SerializeField]
    private Transform GunHolder;
    [SerializeField]
    private float shootingInterval = 1.0f;
    private EnemyBodyPart[] EnemyBodyPart;
    public bool Died { get; private set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ragDollBodies = GetComponentsInChildren<Rigidbody>();
        EnemyBodyPart = GetComponentsInChildren<EnemyBodyPart>();
        InvokeRepeating("ShootAtPlayer", shootingInterval, shootingInterval); 
    }


    private void ShootAtPlayer()
    {
        if (!Died)
        {
            animator.SetTrigger("shoot");
            Debug.Log("Enemy shooting!!");
        }
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

    public static void RemoveEnemyJunk()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "New Game Object");
        foreach(GameObject o in objects)
        {
            Destroy(o);
        }
    }

    public void GetDamaged(float damage)
    {
        TimeManager.instance.DoSlowMotion();
        Died = true;
        ToggleRagDoll(true);
        RemoveEnemyJunk();
    }
}
