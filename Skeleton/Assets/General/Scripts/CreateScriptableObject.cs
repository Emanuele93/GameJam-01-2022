using System;
using UnityEditor;
using UnityEngine;

public class CreateScriptableObject
{
    const string MenuPath = "Assets/Create/ScriptableObject";

    [MenuItem(MenuPath, true)]
    static bool Validate()
    {
        MonoScript script = Selection.activeObject as MonoScript;
        if (script == null) 
            return false;

        Type type = script.GetClass();
        return typeof(ScriptableObject).IsAssignableFrom(type) && !type.IsAbstract && !type.IsGenericType;
    }

    [MenuItem(MenuPath)]
    static void Create()
    {
        var obj = Selection.activeObject;
        Type type = (obj as MonoScript).GetClass();
        string path = AssetDatabase.GetAssetPath(obj.GetInstanceID()).Replace(".cs", ".asset");
        Debug.LogError(path + " --- " + obj.name);
        if(!string.IsNullOrEmpty(path))
            AssetDatabase.CreateAsset(ScriptableObject.CreateInstance(type), path);
    }
}
