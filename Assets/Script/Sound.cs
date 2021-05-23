using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public enum SoundType
{
    Sound,
    BGM
}
[System.Serializable]
public class Sound
{
    public string name;
    public SoundType type;
    public AudioClip clip;
    [Range(0f,1f)]
    public float volume;
    [Range(.1f,3f)]
    public float pitch;
    public bool loop;
    [HideInInspector]
    public AudioSource source;
    [TextArea(10,15)]
    public string credit;
}
