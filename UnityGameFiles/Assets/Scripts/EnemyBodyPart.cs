using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPart : MonoBehaviour
{
    [HideInInspector]
    public Renderer bodyPartRenderer = null;
    [HideInInspector]
    public GameObject bodyPartPrefab = null;
    [HideInInspector]
    public Rigidbody rigidBody;
    private bool isReplaced;
    public EnemyScript Enemy { get; private set; }
    
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        Enemy = GetComponentInParent<EnemyScript>();
    }

    public void DestroyBodyPart()
    {
        if (isReplaced)
            return;

        if (bodyPartRenderer != null)
            bodyPartRenderer.enabled = false;

        
        GameObject bodyPart = new GameObject();
        if (bodyPartPrefab != null)
            bodyPart = Instantiate(bodyPartPrefab, transform.position, transform.rotation);

        bodyPart.name = "EnemyPart";

        Rigidbody[] rbs = bodyPart.GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody r in rbs)
        {
            r.interpolation = RigidbodyInterpolation.Interpolate;
            r.AddExplosionForce(15, transform.position, 5);
        }
        rigidBody.AddExplosionForce(15, transform.position, 5);



        isReplaced = true;
        Destroy(bodyPart, 1f);

    }
}
