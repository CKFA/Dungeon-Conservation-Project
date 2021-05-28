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
    public SoundType TowerBGMPlayList;
    public SoundType TownBGMPlayList;
    public Sound[] sounds;
    public static AudioManager instance;
    private static AudioSource bgmIsPlaying;
    public SoundType currentList;
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
            if(s.type == SoundType.TowerBGM)
            {
                s.source.outputAudioMixerGroup = bgmMixerGroup;
            }
            else if (s.type == SoundType.TownBGM)
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
        if(bgmIsPlaying == null)
        {
            Play(SoundType.TowerBGM);
        }
    }
    public void Play(string name) // for sounds
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
        //if(bgmIsPlaying.isPlaying) // initialisation
        //{
        //    bgmIsPlaying.Stop();
        //}
        currentList = type;
        Sound[] s = Array.FindAll(sounds, sound => sound.type == type);
        
        if(s == null)
        {
            Debug.LogWarning("Sound type: " + name + " not found!");
        }

        if (type == SoundType.TowerBGM)
        {

            bgmIsPlaying = s[UnityEngine.Random.Range(0, s.Length)].source;
            index++;
            bgmIsPlaying.Play();
        }
        else if (type == SoundType.TownBGM)
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

    public void SwitchToPlayTowerBGM()
    {
        if(bgmIsPlaying != null)
            if (bgmIsPlaying.isPlaying)
                bgmIsPlaying.Stop();
        Play(SoundType.TowerBGM);
        
    }

    public void SwitchToPlayTownBGM()
    {
        if(bgmIsPlaying != null)
            if(bgmIsPlaying.isPlaying)
                bgmIsPlaying.Stop();
        Play(SoundType.TownBGM);
    }

    private void Update()
    {
        if (bgmIsPlaying != null) 
        {
            if(!bgmIsPlaying.isPlaying)
            {
                bgmIsPlaying.Stop();
                Debug.Log(index);
                Play(currentList);
            }
        }
        
    }
}
