using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public int speed = 0;
    private float horizontal;
    private float vertical;
    private Rigidbody2D _rb;
    public Text scoreText;
    public GameObject greenBallPrefab;
    public GameObject RedBallPrefab;
    public GameObject RedBallFollowPrefab;
    public GameObject powerUpPrefab;
    public BoxCollider2D[] spawnArea;
    public GameObject deadScreen;
    public Text deadScreenText;
    public Text highscoreText;
    public int highscore;
    [SerializeField] private Animator anim;

    public float spawnPowerUP_time;
    public bool spawnedPowerUP;
    public bool pickedUpPowerUp;

    GameManager score;

    public Image InvencibleFill;
    public Image InvencibleBorder;
    public float InvencibleDurantion;
    public float InvencibleTimeLeft;
    private bool isInvencible = false;
    void Start()
    {
        Time.timeScale = 1;
        score = GameObject.FindObjectOfType<GameManager>();
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        instantiateBall(greenBallPrefab);
        highscore = PlayerPrefs.GetInt("Highscore");
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        spawnPowerUP_time += Time.deltaTime;
        Mathf.RoundToInt(spawnPowerUP_time);
        Vector2 movement = new Vector2(Mathf.Abs(horizontal), vertical);
        anim.SetFloat("Speed", movement.magnitude);

        if (spawnPowerUP_time > 10 && !spawnedPowerUP)
        {
            instantiateBall(powerUpPrefab);
            spawnedPowerUP = true;
        }

        if (InvencibleTimeLeft > 0)
        {
            isInvencible = true;
            InvencibleTimeLeft -= Time.deltaTime;
            InvencibleFill.fillAmount = Mathf.InverseLerp(0, InvencibleDurantion, InvencibleTimeLeft);
        }
        else
        {
            isInvencible = false;
            InvencibleBorder.gameObject.SetActive(false);
        }
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
        if (collision.CompareTag("GreenBall") /*&& !isInvencible*/)
        {
            score.score++;
            Destroy(collision.gameObject);
            instantiateBall(greenBallPrefab);
            int randomNumber = Random.Range(1, 3);
            if (randomNumber == 1)
            {
                instantiateBall(RedBallPrefab);
            }
            else
            {
                instantiateBall(RedBallFollowPrefab);
            }
            StartInvencibility();
        }
        else if (collision.CompareTag("RedBall") && !isInvencible)
        {
            anim.SetTrigger("Die");
            this.enabled = false;
        }
        else if (collision.CompareTag("PowerUP") && !isInvencible)
        {
            pickedUpPowerUp = true;
            Destroy(collision.gameObject);
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
        Time.timeScale = 0;
        //Destroy(GameObject.Find("MainCanvas"));
        SceneManager.LoadScene(3, LoadSceneMode.Additive);
    }
    void instantiateBall(GameObject ballType)
    {
        int selectRandomArea = Random.Range(0, spawnArea.Length - 1);

        Instantiate(ballType,
            new Vector3(Random.Range(spawnArea[selectRandomArea].bounds.min.x, spawnArea[selectRandomArea].bounds.max.x),
            Random.Range(spawnArea[selectRandomArea].bounds.min.y, spawnArea[selectRandomArea].bounds.max.y),
            Random.Range(spawnArea[selectRandomArea].bounds.min.z, spawnArea[selectRandomArea].bounds.max.z))
                , Quaternion.identity);
    }

    void StartInvencibility()
    {
        InvencibleBorder.gameObject.SetActive(true);
        InvencibleTimeLeft = InvencibleDurantion;
    }
}
