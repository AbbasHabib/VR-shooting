using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GunScript : MonoBehaviour
{
    //[SerializeField]
    //private float damage = 10f;
    [SerializeField]
    private float range = 50f;
    [SerializeField]
    private Camera fpsCam = null;
    
    [SerializeField]
    private TimeManager timeManager;
    private int maxAmmo = 6;
    private Rigidbody rb;
   
    private int currentAmmo=-1;
    private float reloadTime = 1f;
    private bool isReloading=false;

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
    
    public void shoot(Vector3 pos, Quaternion rot)
    {

        if (isReloading)
            return;
        if (currentAmmo <= 0)
            return;

        currentAmmo--;
        
        GameObject bullet = Instantiate(PlayerScript.instance.bulletPrefab, pos, rot);
        if (GetComponentInChildren<ParticleSystem>() != null)
        {
            GetComponentInChildren<ParticleSystem>().Play();
        }

        if (PlayerScript.instance.gun == this && currentAmmo <=0)
            StartCoroutine(Reload()); //coroutine pauses execution and automatically resumes at the next frame

        Camera.main.transform.DOComplete();
        Camera.main.transform.DOShakePosition(.2f, .01f, 10, 90, false, true).SetUpdate(true); // shakes a camera's position 

        if (PlayerScript.instance.gun == this)
            transform.DOLocalMoveZ(-.1f, .05f).OnComplete(() => transform.DOLocalMoveZ(0, .2f)); //Moves the target's position to the given value

        RaycastHit hit; // information is stored here whenever we hit something
        EnemyScript target;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))// return true if we hit something
        {
            target = hit.transform.GetComponent<EnemyScript>();
            ///*if (target != null)
            //{
            //    target.Die();
            //    //timeManager.DoSlowMotion();
            //}*/
            //Debug.Log(hit.transform.name);
        } 
    }

}
