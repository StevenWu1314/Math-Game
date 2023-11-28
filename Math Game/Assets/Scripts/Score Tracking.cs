using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreTracking : MonoBehaviour
{
    //public TMPro.TMP_Text highscoreText;
    public TMPro.TMP_Text scoreText;
    [SerializeField] private int score = 0;
    public int highScore = 0;
    // Start is called before the first frame update
    
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        //highscoreText.text = "HighScore\n" + highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (score > highScore)
        {
            highScore = score;
           // highscoreText.text = "HighScore\n" + highScore.ToString();
        }
        scoreText.text = "score: " + score.ToString();
        
        
    }

    public void IncreaseScore(int value)
    {
        score += value;
    }
}
