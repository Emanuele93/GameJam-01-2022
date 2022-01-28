using System;
using UnityEngine;

[Serializable]
public class MixerWrapper : ScriptableObject
{
    public void Play(string effectName)
    {
        Services.Get<Mixer>().Play(effectName);
    }

    public void PlayMusic(string musicName)
    {
        Services.Get<Mixer>().PlayMusic(musicName);
    }
}