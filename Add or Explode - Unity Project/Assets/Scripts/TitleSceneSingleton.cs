using UnityEngine;

public class TitleSceneSingleton : MonoBehaviour
{
    static TitleSceneSingleton instance;

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