using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] public static ScoreSystem scoreSystem;

    [SerializeField] TextMeshProUGUI HighScoreText;
    [SerializeField] TextMeshProUGUI ScoreText;
    [SerializeField] TextMeshProUGUI MultiplierText;

    private double highScore = 0;
    private double score = 0;
    [SerializeField] int multiplier = 1;

    void Awake()
    {
        if (scoreSystem == null)
        {
            scoreSystem = this.GetComponent<ScoreSystem>();
        }
        else
        {
            Destroy(this.gameObject);
        }
        MultiplierText.text = "";
        highScore = 0;
        score = 0;
    }

    public void AddPoint()
    {
        score += 1 * multiplier;
        ScoreText.text = "Score: " + FormatScoreText(score);
    }

    public int GetMultiplier()
    {
        return multiplier;
    }

    public void SetMultiplier(int newMultiplier)
    {
        multiplier = newMultiplier;
        MultiplierText.text = multiplier + "x Multiplier!";
    }

    public void ResetMultiplier()
    {
        multiplier = 1;
        MultiplierText.text = "";
    }

    public void ResetScore()
    {
        SetHighScore(score);
        score = 0;
        ResetMultiplier();
        ScoreText.text = "Score: " + FormatScoreText(score);
        ResetScoreColor();
    }

    void SetHighScore(double newHighScore)
    {
        if(newHighScore > highScore)
        {
            highScore = newHighScore;
            HighScoreText.text = "High Score: " + FormatScoreText(highScore);
        }
    }

    string FormatScoreText(double score)
    {
        string scoreString = score.ToString();

        if (scoreString.Length > 3)
        {
            for (int i = scoreString.Length - 3; i > 0; i-=3)
            {
                scoreString = scoreString.Insert(i, ",");
            }
        }
        return scoreString;
    }

    public void SetScoreColor(Color c)
    {
        HighScoreText.color = c;
        ScoreText.color = c;
        MultiplierText.color = c;
    }

    void ResetScoreColor()
    {
        HighScoreText.color = Color.white;
        ScoreText.color = Color.white;
    }
}
