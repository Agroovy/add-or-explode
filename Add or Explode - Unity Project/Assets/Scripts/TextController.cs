using System;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    public Text scoreText;
    public Text coordsText;

    int highScore;

    private void Awake() => highScore = PlayerPrefs.GetInt("highscore");

    public void UpdateText(int currentScore, Vector2 coords)
    {
        if (currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("highscore", currentScore);
            PlayerPrefs.Save();
        }
        scoreText.text = $"Highscore: {highScore}";
        coordsText.text = coords.ToString();
    }
}
