using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    [SerializeField]
    private float damage = 0.5f;
    [SerializeField]
    private float speed = 20;

    private void Start()
    {
        Destroy(gameObject, 2.0f);
    }
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
   
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject, 0.5f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        IDamageable damageAbleObj = collision.gameObject.GetComponent<IDamageable>();
        if (damageAbleObj != null)
        {
            damageAbleObj.GetDamaged(damage);
        }
        Destroy(gameObject, 0.5f);
    }
}
