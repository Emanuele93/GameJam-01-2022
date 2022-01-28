using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager: MonoBehaviour
{
    //TODO limit to only scrptObj that implements IService
    [SerializeField] GameObject startingObj;
    [SerializeField] List<NamedAudioSource> musicAudioSources;
    [SerializeField] List<NamedAudioSource> effectAudioSources;
    [SerializeField] UnityEngine.Object[] services;


    private void Start()
    {
        foreach (var s in services)
        {
            if (s is IOpenAppService)
                ((IOpenAppService)s).OpenApp();
            Services.Add(s.GetType(), (IService)s);
        }

        Services.Get<Navigator>().Open(Scenes.HomePage);
        var mixer = Services.Get<Mixer>();
        mixer.Init(musicAudioSources, effectAudioSources);
        mixer.PlayMusic(null);
        startingObj.SetActive(false);
    }

    private void OnDestroy()
    {
        foreach (var s in services)
            if (s is IDestroyService)
                ((IDestroyService)s).OnDestroy();
    }
}

public static class Services
{
    private static Dictionary<Type, IService> services = new Dictionary<Type, IService>();

    internal static void Add(Type type, IService service)
    {
        services[type] = service;
    }

    public static T Get<T>()
    {
        if (services.TryGetValue(typeof(T), out var service))
            return (T)service;
        throw new ArgumentException($"No Service of type {typeof(T).Name} found");
    }
}

public interface IService
{

}

public interface IOpenAppService : IService
{
    public void OpenApp();
}

public interface IDestroyService : IService
{
    public void OnDestroy();
}