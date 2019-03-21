using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DDOLSingleton<T> : MonoBehaviour where T : DDOLSingleton<T>
{
    protected static T _instance = null;
    public static T GetInstance()
    {

        if (_instance == null)
        {
            GameObject go = GameObject.Find("DDOL");
            if (go == null)
            {
                go = new GameObject("DDOL");
                DontDestroyOnLoad(go);
            }
            _instance = go.GetComponent<T>();
            if (_instance == null)
            {
                _instance = go.AddComponent<T>();
            }
        }
        return _instance;
    }    
}
