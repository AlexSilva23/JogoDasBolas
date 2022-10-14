using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreenManager : MonoBehaviour
{
    public GameManager gManager;
    public Text endText, scoreText, highScoreText;
    int highscore = 0;

    void Start()
    {
        gManager = GameObject.FindObjectOfType<GameManager>();
        highscore = PlayerPrefs.GetInt("Highscore");
        if (gManager.score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", gManager.score);
        }

    }

    void Update()
    {
        if (gManager.Win == true)
        {
            endText.text = "YOU WIN!";
        }
        else
        {
            endText.text = "DEAD!";
        }

        scoreText.text = "Score: " + gManager.score;
        highScoreText.text = "Highscore: " + highscore;
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        gManager.score = 0;
        gManager.timer = 0;
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
