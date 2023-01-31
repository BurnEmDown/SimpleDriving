using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TMP_Text highScoreText;

    public void Start()
    {
        int highscore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);

        highScoreText.text = "High Score: " + highscore.ToString();
    }

    public void OnPlayButtonClick()
    {
        SceneManager.LoadScene("Game");
    }
}
