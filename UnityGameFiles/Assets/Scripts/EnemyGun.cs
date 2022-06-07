using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGun : MonoBehaviour
{
    [SerializeField]
    private GameObject bulletToSpawn = null;

    [SerializeField]
    private Transform targetPoint =null;


    private Vector3 targetPointPosition;
    private Vector3 direction;
    private void Start()
    {
        if(targetPoint == null)
        {
            targetPoint = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    public void ShootAtPlayer(Vector3 pos)
    {
        targetPointPosition = targetPoint.position;
        direction = (targetPointPosition - pos);

        GameObject bullet = Instantiate(bulletToSpawn, this.transform.position, transform.rotation);
        bullet.transform.forward = direction;
        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }
        SFXManager.Instance.Play("enemyShoot");
    }
}
