using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Sound> listaSom = new List<Sound>();

    private void Awake() 
    {
        foreach(Sound s in listaSom)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }     
    }

    public void Play(string name)
    {
        Sound s = listaSom.Find(sound => sound.name == name);
        s.source.Play();
    }

}
