using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 0;
    private float horizontal;
    private float vertical;
    private Rigidbody2D _rb;
    int score = 0;
    public Text scoreText;
    public GameObject greenBallPrefab;
    public GameObject RedBallPrefab;
    public GameObject RedBallFollowPrefab;
    public BoxCollider2D spawnArea;
    public GameObject deadScreen;
    public Text deadScreenText;
    public Text highscoreText;
    public int highscore;
    [SerializeField] private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        updateText(score);
        instantiateBall(greenBallPrefab);
        highscore = PlayerPrefs.GetInt("Highscore");
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(Mathf.Abs(horizontal), vertical);

        anim.SetFloat("Speed", movement.magnitude);
    }

    private void FixedUpdate()
    {
        Vector2 pos = transform.position;
        pos.y += vertical * speed * Time.deltaTime;
        pos.x += horizontal * speed * Time.deltaTime;
        _rb.MovePosition(pos);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("GreenBall"))
        {
            score++;
            updateText(score);
            Destroy(collision.gameObject);
            instantiateBall(greenBallPrefab);
            int randomNumber = Random.Range(1, 3);
            Debug.Log(randomNumber);
            if (randomNumber == 1)
            {
                instantiateBall(RedBallPrefab);
            }
            else
            {
                instantiateBall(RedBallFollowPrefab);
            }
        }
        else if (collision.CompareTag("RedBall"))
        {
            Debug.Log("Die");
            anim.SetTrigger("Die");
            this.enabled = false;
        }

    }

    public static Vector3 RandomPointInSpawnArea(Bounds bounds)
    {
        return new Vector3(
            Random.Range(bounds.min.x, bounds.max.x),
            Random.Range(bounds.min.y, bounds.max.y),
            Random.Range(bounds.min.z, bounds.max.z)
        );
    }

    void updateText(int score)
    {
        scoreText.text = "SCORE: " + score;
    }

    public void Die()
    {
        _rb.velocity = Vector2.zero;
        if (score > highscore)
        {
            PlayerPrefs.SetInt("Highscore", score);
            highscore = score;
        }
        deadScreenText.text = "SCORE: " + score;
        highscoreText.text = "HIGHSCORE: " + highscore;
        deadScreen.SetActive(true);
    }
    void instantiateBall(GameObject ballType)
    {
        Instantiate(ballType,
            new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),
            Random.Range(spawnArea.bounds.min.y, spawnArea.bounds.max.y),
            Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z))
                , Quaternion.identity);
    }
}
