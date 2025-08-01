using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singletons<T> : MonoBehaviour where T : Component
{
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    GameObject objectSearched = new GameObject();
                    instance = objectSearched.AddComponent<T>();
                }
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        instance = this as T;
    }
}

