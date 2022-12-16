using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerPowerPointEffecr : MonoBehaviour
{
    delegate void MultiDelegate();
    MultiDelegate myMultiDelegate_smallBalls;
    MultiDelegate myMultiDelegate_bigBalls;
    public PlayerMovement pickedUP;
    private GameManager gm;
    public float timer;


    public void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        pickedUP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        myMultiDelegate_smallBalls += powerUpEffect;
        myMultiDelegate_bigBalls += resizeBallls;
    }

    void Update()
    {
        if (pickedUP.pickedUpPowerUp && gm.redBallResizeTime<10.5f)
        {
            Debug.Log("small");
            myMultiDelegate_smallBalls();
        }

        if (gm.redBallResizeTime > 10.5f)
        {
            Debug.Log("big");
            myMultiDelegate_bigBalls();
        }

    }

    void powerUpEffect()
    {
        Debug.Log("1");
        this.transform.localScale = new Vector3(.4f, .4f, .4f);
    }

    void resizeBallls()
    {
        Debug.Log("2");
        this.transform.localScale = new Vector3(.75f, .75f, .75f);
    }

}
