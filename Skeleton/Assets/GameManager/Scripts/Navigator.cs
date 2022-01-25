using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable, CreateAssetMenu(fileName = "Navigator", menuName = "ScriptableObjects/Navigator")]
public class Navigator : ScriptableObject, IService
{
    public int test;
}
