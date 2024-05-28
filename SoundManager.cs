using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private AudioSource source;

    private void Awake() 
    {
       
        source = GetComponent<AudioSource>();

        //keeps music in loop in from scene to scene
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        //destroy dups
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        
    }

    public void PlaySound(AudioClip _sound)
    {
        source.PlayOneShot(_sound);
    }
}
