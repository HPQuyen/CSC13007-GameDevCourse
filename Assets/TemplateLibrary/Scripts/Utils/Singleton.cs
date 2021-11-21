using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField]
    private bool verbose = true;
    [SerializeField]
    private bool keepAlive = false;
    [SerializeField]
    private bool duplicateAllow = true;

    private static T _instance;
    public static T Instance
    {
        get
        {
            if (!_instance)
                _instance = GameObject.FindObjectOfType<T>();
            return _instance;
        }
    }

    protected virtual void Awake()
    {
        if (_instance && !duplicateAllow)
        {
            if (verbose)
                Debug.Log("Destroy duplicate singleton");
            Destroy(gameObject);
        }
        if (keepAlive)
            DontDestroyOnLoad(this);
        _instance = GetComponent<T>();
    }
}
