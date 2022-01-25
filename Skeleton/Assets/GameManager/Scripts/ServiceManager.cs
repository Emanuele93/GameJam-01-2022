using System;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager: MonoBehaviour
{
    //TODO obbliga che siano IService
    [SerializeField] UnityEngine.Object[] services;

    private void Start()
    {
        foreach (var s in services)
            Services.Add(s.GetType(), (IService)s);
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