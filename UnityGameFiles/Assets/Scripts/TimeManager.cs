using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager instance;

    [SerializeField]
    private float slowdownFactor = 0.05f; //control how much we will slowdown time 
    [SerializeField]
    private float slowdownLenght = 2f; // we will slowdown 2sec

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    private void FixedUpdate()
    {
        Time.timeScale += (1f / slowdownLenght)*Time.unscaledDeltaTime; // after 2 sec it returns to normal 
        Time.timeScale = Mathf.Clamp(Time.timeScale,0.0f,1f);
    }
    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor; 
    }
    


}
