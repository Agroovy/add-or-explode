using UnityEngine;

public class SFXPlayer : MonoBehaviour
{
    public static AudioSource audioSource;
    static SFXPlayer instance;
    static bool paused = false;

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

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public static void TogglePause()
    {
        if (paused)
        {
            audioSource.Play();
            paused = false;
        } else
        {
            audioSource.Pause();
            paused = true;
        }
    }    

    public static void Play(string clip, float volume = 1)
    {
        AudioClip sound = (AudioClip)Resources.Load($"Audio/{clip}");
        audioSource.PlayOneShot(sound, volume);
    }
}
