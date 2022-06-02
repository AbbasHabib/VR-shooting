using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class PlayerScript : MonoBehaviour
{

    public static PlayerScript instance;
    [SerializeField]
    private GameObject bullet;
    [SerializeField]
    private Transform bulletSpawner;
    private float shootingRate = 15f;
    private float nextTimeToFire = 0f;

    [Header("Gun")]
    public GunScript gun;
    public Transform gunHolder;
    public LayerMask GunLayer;

    [Space]
    [Header("UI")]
    public Image indicator;

    [Space]
    [Header("Prefabs")]
    public GameObject bulletPrefab;
    private float time ;
    private float lerpTime;
    private bool action;
    private void Awake() // used to intialize any variable before game start
    {
        instance = this;
        if (gunHolder.GetComponentInChildren<GunScript>() != null)
            gun = gunHolder.GetComponentInChildren<GunScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            if (gun != null)
            {
                nextTimeToFire = Time.time + 1f / shootingRate;
                gun.shoot(SpawnPos(), Camera.main.transform.rotation);
            }
                
        }
        

    }
    
    Vector3 SpawnPos()
    {
        return Camera.main.transform.position + (Camera.main.transform.forward * .5f) + (Camera.main.transform.up * -.02f);
    }
   


}
