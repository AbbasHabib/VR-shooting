using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
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

    GameObject FindChildWithTag(GameObject parent, string tag)
    {
        GameObject child = null;

        foreach (Transform transform in parent.transform)
        {
            if (transform.CompareTag(tag))
            {
                child = transform.gameObject;
                break;
            }
        }

        return child;
    }
    private void Start()
    {
        gunScript = shootingGun.GetComponent<GunScript>();
        spawnPoint = FindChildWithTag(shootingGun, "PlayerBulletSpawnPoint");
    }
    void Update()
    {
        while (UDPReceive.actionQueue.TryDequeue(out currAction))
        {
            if (currAction.Length > 0)
            {
                if (currAction[0] == 'r')
                    rotateObj(currAction);
                else if (currAction[0] == 'f')
                    triggerFlex(currAction);
            }
        }
    }

    private void FixedUpdate()
    {
            rotationObject.transform.localRotation = Quaternion.Lerp(rotationObject.transform.localRotation, goToQuaterion, Time.deltaTime * smoothness);
    }

    public void triggerFlex(string data)
    {
        string[] values = data.Split('/');
        if (values.Length == 3)
        {
            float bendAngle1 = int.Parse(values[1]);
            float bendAngle2 = int.Parse(values[2]);

            //print("f1 > " +bendAngle1 +" f2 > " + bendAngle2); //  121292 f2 > 63279
            if (bendAngle1 >= 121292)
            {
                try
                {
                    gunScript.shoot(spawnPoint.transform.position, spawnPoint.transform.rotation);
                }
                catch { }
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
            goToQuaterion = new Quaternion(w  + offsetQuaterion.w, y + offsetQuaterion.y, x + offsetQuaterion.x, z  + offsetQuaterion.z);
        }
        else if (values.Length != 5)
        {
            Debug.LogWarning(data);
        }
    }
}
