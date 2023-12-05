using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreTracking : MonoBehaviour
{
    //public TMPro.TMP_Text highscoreText;
    public TMPro.TMP_Text scoreText;
    [SerializeField] private int score = 0;
    public TMPro.TMP_Text highscoreText;
    public int highScore = 0;
    public bool victoryScreen = false;
    // Start is called before the first frame update
    
    void Start()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        //highscoreText.text = "HighScore\n" + highScore.ToString();
        getHighScore();
    }

    // Update is called once per frame
    void Update()
    {
        if(!victoryScreen){
            PlayerPrefs.SetInt("HighScore", score);
        }
        
        // highscoreText.text = "HighScore\n" + highScore.ToString();
        scoreText.text = "score:" + score.ToString();
        
        
    }

    public void IncreaseScore(int value)
    {
        score += value;
    }

    public void getHighScore(){
        highscoreText.text = "Score\n" + PlayerPrefs.GetInt("HighScore").ToString();
        
    }
    public void homeScreen(){
        SceneManager.LoadScene(0);
    }
}
