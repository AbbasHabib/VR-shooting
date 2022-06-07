using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    float playerHealth = 3;

    float currHealth = 3;


    // Start is called before the first frame update
    void Start()
    {
        currHealth = playerHealth;
    }

    public void GetDamaged(float n)
    {
        playerHealth -= n;
        PostProcessManager.instance.GoToVignette(0.8f);
    }
}
