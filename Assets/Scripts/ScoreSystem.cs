using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier = 1f;

    public const string HighScoreKey = "HighScore";
    
    private float score;

    private void Update()
    {
        score += Time.deltaTime * scoreMultiplier;
        int scoreNum = Mathf.FloorToInt(score);
        scoreText.text = scoreNum.ToString();
    }

    private void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(HighScoreKey, 0);

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HighScoreKey, Mathf.FloorToInt(score));
        }
    }
}
