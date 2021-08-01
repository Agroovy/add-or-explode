using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandaloneScreenManager : MonoBehaviour
{
    static StandaloneScreenManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(this);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void OnApplicationQuit()
    {
#if UNITY_STANDALONE

#endif
    }
}
