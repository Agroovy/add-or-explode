using UnityEngine;

public class MainSceneSingleton : MonoBehaviour
{
    static MainSceneSingleton instance;

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
}