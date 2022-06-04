using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GunScript : MonoBehaviour
{   
    [SerializeField]
    private int maxAmmo = 6;
    [SerializeField]
    private GameObject bulletToSpawn = null;
    [SerializeField]
    private float gunAwaitTime = 1f;

    private Rigidbody rb;
    private int currentAmmo=-1;
    private float reloadTime = 1f;
    private bool isReloading=false;

    public bool GunAvailable { get; private set; } = true;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (currentAmmo == -1)
        {
            currentAmmo = maxAmmo;
        }
            
    }
    
    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading.....");
        yield return new WaitForSeconds(reloadTime);
        currentAmmo = maxAmmo;
        isReloading = false;
    }


    private void ReleaseGunWait()
    {
        GunAvailable = true;
    }

    public void shoot(Vector3 pos, Quaternion rot)
    {
        if (!GunAvailable || isReloading || currentAmmo <= 0)
            return;

        GunAvailable = false;
        currentAmmo--;

        //GameObject bullet = Instantiate(PlayerScript.instance.bulletPrefab, pos, rot);
        GameObject bullet = Instantiate(bulletToSpawn, pos, rot);
        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }

        if (currentAmmo <=0)  
            StartCoroutine(Reload());

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true); 
         
       
        transform.DOLocalMoveZ(-.01f, .005f).OnComplete(() => transform.DOLocalMoveZ(0, .2f));


        Invoke("ReleaseGunWait", gunAwaitTime);
    }

}
