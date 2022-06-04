using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UDPActionsReceiver : MonoBehaviour
{
    private String currAction = "";
    public static UDPActionsReceiver Instance { get; private set; }
    [Header("Rotation Object [Gun|Hand]")]
    [SerializeField]
    private GameObject rotationObject = null;
    [SerializeField]
    private Quaternion offsetQuaterion = new Quaternion(0,0,0,0);
    [SerializeField]
    private float smoothness = 15.0f;


    [Header("shooting Object [Gun]")]
    [SerializeField]
    private GameObject shootingGun = null;
    private GunScript gunScript = null;
    private GameObject spawnPoint = null;

    private Quaternion goToQuaterion = new Quaternion(0, 260, 180, 0);


    /*
     * 
     * 
     *     
     *     [SerializeField]GunScript gun;
    [SerializeField]GameObject spawnObj;
    void Start()
    {
        InvokeRepeating("ShootTest", 2.0f, 1.3f);
    }

    // Update is called once per frame
    void ShootTest()
    {
        gun.shoot(spawnObj.transform.position, spawnObj.transform.rotation);
    }
}
     * */




    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        gunScript = shootingGun.GetComponent<GunScript>();
        spawnPoint = GameObject.FindGameObjectWithTag("PlayerBulletSpawnPoint");
    }
    void Update()
    {
        while (UDPReceive.actionQueue.TryDequeue(out currAction))
        {
            if (currAction[0] == 'r')
                rotateObj(currAction);
            else if (currAction[0] == 'f')
                triggerFlex(currAction);
        }
    }

    private void FixedUpdate()
    {
            rotationObject.transform.localRotation = Quaternion.Lerp(rotationObject.transform.localRotation, goToQuaterion, Time.deltaTime * smoothness);
    }

    public void triggerFlex(string data)
    {
        string[] values = data.Split('/');
        if (values.Length == 2)
        {
            float bendAngle = int.Parse(values[1]);
            if(bendAngle == 45 || bendAngle == 90)
            {
                gunScript.shoot(spawnPoint.transform.position, spawnPoint.transform.rotation);
            }
        }
        else if (values.Length != 2)
        {
            Debug.LogWarning(data);
        }
    }
    public void rotateObj(string data)
    {
        string[] values = data.Split('/');
        if (values.Length == 5) 
        {
            float w = float.Parse(values[1]);
            float x = float.Parse(values[2]);
            float y = float.Parse(values[3]);
            float z = float.Parse(values[4]);
            goToQuaterion = new Quaternion(w + offsetQuaterion.w, y + offsetQuaterion.y, x + offsetQuaterion.x, z + offsetQuaterion.z);
        }
        else if (values.Length != 5)
        {
            Debug.LogWarning(data);
        }
    }
}
