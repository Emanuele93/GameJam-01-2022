using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum Scenes
{
    GameCore, HomePage
}

[Serializable]
public class Navigator : ScriptableObject, IOpenAppService, IDestroyService 
{
    [SerializeField] int MaxCachedPages;
    private List<Scene> loadedScenes = new List<Scene>();

    public void OpenApp()
    {
        SceneManager.sceneLoaded += OnSceneAdded;
        loadedScenes.Clear();
    }

    public void Open(Scenes sceneName)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        Scene newScene = SceneManager.GetSceneByName(sceneName.ToString());

        if (currentScene == newScene)
            return;

        if (loadedScenes.Count > 0)
            foreach (var obj in currentScene.GetRootGameObjects())
                obj.SetActive(false);

        if (newScene.isLoaded)
        {
            foreach (var obj in newScene.GetRootGameObjects())
                obj.SetActive(true);
            SceneManager.SetActiveScene(newScene);
            loadedScenes.Remove(newScene);
            loadedScenes.Add(newScene);
        }
        else
            SceneManager.LoadSceneAsync(sceneName.ToString(), LoadSceneMode.Additive);
    }

    private void OnSceneAdded(Scene scene, LoadSceneMode sceneMode)
    {
        SceneManager.SetActiveScene(scene);
        loadedScenes.Add(scene);
        if (MaxCachedPages > 0 && loadedScenes.Count > MaxCachedPages)
        {
            Scene last = loadedScenes[0];
            SceneManager.UnloadSceneAsync(last);
            loadedScenes.Remove(last);
        }
    }

    public void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneAdded;
    }
}
