using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMusicScript : MonoBehaviour
{
    public static BGMusicScript Instance;
    private void Awake()
    {
       if(Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
    }
}
