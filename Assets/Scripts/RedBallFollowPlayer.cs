using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBallFollowPlayer : MonoBehaviour
{
    private Rigidbody2D _rb;
    private GameObject target;

    void Start()
    {

        target = GameObject.FindGameObjectWithTag("Player");
        _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = new Vector2(5, 5);
    }

    // Update is called once per frame
    void Update()
    {
        //_rb.AddForce(transform.up);
    }

    private void FixedUpdate()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Vector3 targetdirection = target.transform.position - transform.position;

            _rb.velocity = targetdirection.normalized * 7;
        }

    }

}
