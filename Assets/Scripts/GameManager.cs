using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public float timer;
    public int score = 0;
    public Text scoreText;
    [HideInInspector] public bool Win = false;

    public float redBallResizeTime = 0;
    public PlayerMovement player;

    private void Start()
    {
        scoreText = gameObject.transform.Find("ScoreText").GetComponent<Text>();
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>();
        }
        timer += Time.deltaTime;
        if (timer >= 30)
        {
            timer = 0;
            if (SceneManager.GetActiveScene().buildIndex != 2)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                Time.timeScale = 0;
                Win = true;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Additive);

            }
        }
        if (player.pickedUpPowerUp)
        {
            redBallResizeTime += Time.deltaTime;
        }

        scoreText.text = "Score: " + score;

    }
}
