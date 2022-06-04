using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UDPActionsReceiver : MonoBehaviour
{
    private String currAction = "";
    public static UDPActionsReceiver Instance { get; private set; }
    [SerializeField]
    private GameObject rotationObject = null;
    [SerializeField]
    private float smoothness = 15.0f;

    private Quaternion goToQuaterion = new Quaternion(0, 0, 0, 0);
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
            float bendAngle = float.Parse(values[1]);
            print(">>>>>" + bendAngle);
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
            goToQuaterion = new Quaternion(w, y, x, z);
        }
        else if (values.Length != 5)
        {
            Debug.LogWarning(data);
        }
    }
}
