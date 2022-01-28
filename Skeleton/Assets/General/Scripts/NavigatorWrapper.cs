using System;
using UnityEngine;

[Serializable]
public class NavigatorWrapper : ScriptableObject
{
    public void Open(string sceneName)
    {
        if (Enum.TryParse<Scenes>(sceneName, out var scene))
            Services.Get<Navigator>().Open(scene);
        else
            throw new ArgumentException($"No Scene found with name {sceneName}");
    }

    public void Open(Scenes sceneName)
    {
        Services.Get<Navigator>().Open(sceneName);
    }
}