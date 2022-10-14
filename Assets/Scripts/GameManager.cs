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
        timer += Time.deltaTime;
        Debug.Log(timer);
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

        scoreText.text = "Score: " + score;

    }
}
