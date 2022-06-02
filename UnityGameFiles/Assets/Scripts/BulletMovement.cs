using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    public float speed=20;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Enemy"))
        {
            EnemyBodyPart bp = collision.gameObject.GetComponent<EnemyBodyPart>();
            TimeManager.instance.DoSlowMotion();
            bp.enemy.Died = true;
            bp.enemy.ToggleRagDoll(true);
        }
        Destroy(gameObject);
    }
}
