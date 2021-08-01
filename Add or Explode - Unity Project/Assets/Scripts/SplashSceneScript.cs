using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using TMPro;

public class SplashSceneScript : MonoBehaviour
{
    public TextMeshProUGUI loadingText;

    private IEnumerator Start()
    {
        SceneManager.LoadSceneAsync("Title Scene");
        while (!SplashScreen.isFinished)
        {
            SplashScreen.Draw();
            yield return null;
        }
        StartCoroutine(DotDotDot());
    }

    IEnumerator DotDotDot()
    {
        int i = 0;
        while (true)
        {
            loadingText.text = $"Loading{new string('.', i)}";
            if (i == 3)
            {
                i = 0;
                yield return new WaitForSeconds(0.2f);
            } else
            {
                i++;
                yield return new WaitForSeconds(0.1f);
            }            
        }
    }
}
