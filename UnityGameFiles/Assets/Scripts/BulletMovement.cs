using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float damage = 0.5f;
    [SerializeField]
    private float speed = 20;

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    private void OnCollisionEnter(Collision collision)
    {

        IDamageable damageAbleObj = collision.gameObject.GetComponent<IDamageable>();

        if (damageAbleObj != null)
        {
            damageAbleObj.GetDamaged(damage);
        }

        //if (collision.gameObject.CompareTag("Enemy"))
        //{
        //    //EnemyBodyPart bp = collision.gameObject.GetComponent<EnemyBodyPart>();
        //    //TimeManager.instance.DoSlowMotion();
        //    //bp.Enemy.Died = true;
        //    //bp.Enemy.ToggleRagDoll(true);
        //    //EnemyScript.RemoveEnemyJunk();
        //}
        Destroy(gameObject, 0.5f);
    }
}
