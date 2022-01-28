using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class Mixer : ScriptableObject, IService
{
    [SerializeField] SoundsTheme soundsTheme;
    private List<NamedAudioSource> musicAudioSources;
    private List<NamedAudioSource> effectAudioSources;
    private NamedAudioSource currentMusic;

    public void Init(List<NamedAudioSource> musicSources, List<NamedAudioSource> effectSources)
    {
        currentMusic = null;
        musicAudioSources = musicSources;
        effectAudioSources = effectSources;
    }

    public void PlayMusic(string musicdName)
    {
        if (currentMusic != null && musicdName == currentMusic.Sound.Name) return;
        Sound sound = soundsTheme.Music(musicdName);
        if (sound == null) return;

        NamedAudioSource selectedSource = null;
        foreach (NamedAudioSource souce in musicAudioSources)
            if (souce.Sound?.Name == musicdName || selectedSource == null && !souce.AudioSource.isPlaying)
                selectedSource = souce; 
        if(selectedSource == null)
        {
            var audioSource = new GameObject("MusicAudioSource");// { transform = { parent = MusicAudioSources[0].AudioSource.transform } };
            selectedSource = new NamedAudioSource(audioSource.AddComponent<AudioSource>());
            selectedSource.AudioSource.loop = true;
            musicAudioSources.Add(selectedSource);
        }

        if (currentMusic != null) currentMusic.Stop();
        selectedSource.Play(sound);
        currentMusic = selectedSource;
    }

    public void Play(string effectName)
    {
        Sound sound = soundsTheme.Effect(effectName);
        if (sound == null) return;

        NamedAudioSource selectedSource = effectAudioSources.FirstOrDefault(source => !source.AudioSource.isPlaying);

        if (selectedSource == null)
        {
            var audioSource = new GameObject("EffectAudioSource");// { transform = { parent = EffectAudioSources[0].AudioSource.transform } };
            selectedSource = new NamedAudioSource(audioSource.AddComponent<AudioSource>());
            effectAudioSources.Add(selectedSource);
        }

        selectedSource.Play(sound);
    }
}

[Serializable]
public class NamedAudioSource
{
    private Sound sound;
    public Sound Sound => sound;

    [SerializeField] AudioSource source;
    public AudioSource AudioSource => source;

    public NamedAudioSource(AudioSource audioSource)
    {
        source = audioSource;
    }

    public void Play(Sound playSound)
    {
        sound = playSound;

        //TODO fade da volume attuale se sta suonando o 0 a Sound.Volume - Niente fade per effetti
        AudioSource.volume = Sound.Volume;
        source.PlayOneShot(sound.AudioClip, Sound.Volume);
    }

    public void Stop()
    {
        source.Stop(); //TODO fade - Niente fade per effetti
    }
}
