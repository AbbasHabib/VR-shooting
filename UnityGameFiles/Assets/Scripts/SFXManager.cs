using System;
using System.Linq;
using UnityEngine;


[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector] public AudioSource source;
}
public class SFXManager : MonoBehaviour
{
    private AudioSource Audio;
    public static SFXManager Instance;
    public Sound[] sounds;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }
    }
    public void Play(string name)
    {
        Sound snd = Array.Find(sounds, sound => sound.name == name);
        try
        {
            snd.source.Play();
        }
        catch 
        {
            Debug.LogWarning("sound not found");
        }


    }


}
