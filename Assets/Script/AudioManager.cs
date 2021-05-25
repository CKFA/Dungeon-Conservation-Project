using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup bgmMixerGroup;
    public AudioMixerGroup soundMixerGroup;
    public SoundType TypeOfPlayList;
    public Sound[] sounds;
    public static AudioManager instance;
    private static AudioSource bgmIsPlaying;
    private int index;
    private void Awake()
    {
        if(instance ==null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach(Sound s in sounds) // add audiosource
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            if(s.type == SoundType.BGM)
            {
                s.source.outputAudioMixerGroup = bgmMixerGroup;
            }
            else if(s.type == SoundType.Sound)
            {
                s.source.outputAudioMixerGroup = soundMixerGroup;
            }
        }
    }

    public void Start()
    {
        Play(TypeOfPlayList);
    }
    public void Play(string name)
    {
        Sound s =Array.Find(sounds, sound => sound.name == name);
        if(s==null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Play(SoundType type)
    {
        
        Sound[] s = Array.FindAll(sounds, sound => sound.type == type);
        
        if(s == null)
        {
            Debug.LogWarning("Sound type: " + name + " not found!");
        }
        
        if(type == SoundType.BGM)
        {
            
            bgmIsPlaying = s[UnityEngine.Random.Range(0, s.Length)].source;
            index++;
            bgmIsPlaying.Play();
        }
        else
        {
            s[UnityEngine.Random.Range(0, s.Length)].source.Play();
        }
    }

    private void Update()
    {
        if(bgmIsPlaying!=null)
        {
            if(!bgmIsPlaying.isPlaying)
            {
                bgmIsPlaying.Stop();
                Debug.Log(index);
                Play(TypeOfPlayList);
            }
        }
    }
}
