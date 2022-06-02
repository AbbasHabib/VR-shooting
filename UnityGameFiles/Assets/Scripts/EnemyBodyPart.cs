using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBodyPart : MonoBehaviour
{
    public Renderer bodyPartRenderer;
    public GameObject bodyPartPrefab;
    public Rigidbody rigidBody;
    public bool isReplaced;
    public EnemyScript enemy;
    
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        enemy = GetComponentInParent<EnemyScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
       
        Rigidbody[] rbs = bodyPart.GetComponentsInChildren<Rigidbody>();
        
        foreach (Rigidbody r in rbs)
        {
            r.interpolation = RigidbodyInterpolation.Interpolate;
            r.AddExplosionForce(15, transform.position, 5);
        }
        rigidBody.AddExplosionForce(15, transform.position, 5);
        
        this.enabled = false;
        isReplaced = true;
        
    }
}
