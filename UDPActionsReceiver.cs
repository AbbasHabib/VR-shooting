using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UDPActionsReceiver : MonoBehaviour
{

    // Update is called once per frame
    private String currAction = "";
    public static UDPActionsReceiver Instance { get; private set; }
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
            print(">>>>> " + currAction);
    }


    public String getGyroscopeVals()
    {
        return "";
    }

}
