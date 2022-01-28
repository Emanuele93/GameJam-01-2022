using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class SoundsTheme : ScriptableObject
{
    //TODO controllo da editor che non ci siano 2 elementi con lo stesso nome tra Musics ed Effects o internamente ad entrambi
    [SerializeField] Sound[] Musics;
    [SerializeField] Sound[] Effects;

    public Sound Effect(string effectName) => Effects.First(clip => clip.Name == effectName);
    public Sound Music(string musicName) => string.IsNullOrEmpty(musicName) ? Musics[0] : Musics.First(clip => clip.Name == musicName);
}

[Serializable]
public class Sound
{
    [SerializeField] string name;
    public string Name => name;

    [SerializeField] AudioClip audioClip;
    public AudioClip AudioClip => audioClip;

    [SerializeField, Range(0f, 1f)] float volume;
    public float Volume => volume;
}