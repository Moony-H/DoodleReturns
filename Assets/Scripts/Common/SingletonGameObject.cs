using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonGameObject<T> : MonoBehaviour where T : UnityEngine.Component{
    protected static T _instance = null;
    public static T Instance
    {
        get
        {
            if (!_instance)
            {
                _instance = FindObjectOfType<T>();
                if (null == _instance)
                {
                    var ins = new GameObject(typeof(T).Name);
                    _instance = ins.AddComponent<T>();
                }
            }
            return _instance;
        }
        set
        {
            if (!_instance)
                _instance = value;
        }
    }
	
}
