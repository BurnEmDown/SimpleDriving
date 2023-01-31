using System;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier = 1f;
    
    private float score;

    private void Update()
    {
        score += Time.deltaTime * scoreMultiplier;
        int scoreNum = Mathf.FloorToInt(score);
        scoreText.text = scoreNum.ToString();
    }
}
