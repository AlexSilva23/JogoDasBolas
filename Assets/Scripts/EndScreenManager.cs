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
    private bool newHighscore;

    void Start()
    {
        gManager = GameObject.FindObjectOfType<GameManager>();
        highscore = PlayerPrefs.GetInt("Highscore");
        if (gManager.score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", gManager.score);
            newHighscore = true;
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
        if (newHighscore)
        {
            highScoreText.text = "New Highscore!!";
        }
        else
        {
            highScoreText.text = "";
        }
    }

    public void PlayAgain()
    {
        gManager.player = FindObjectOfType<PlayerMovement>();
        gManager.redBallResizeTime = 0;
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
