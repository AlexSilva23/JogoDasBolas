using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBall : MonoBehaviour
{
    private Rigidbody2D _rb;
    public bool isEnemy = false;
    public bool followPlayer = false;

    private Vector3 moveDirection;

    public GameObject target;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = RandomVector(2f, 7f);
        target = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isEnemy && followPlayer)
        {
            if (collision.gameObject.CompareTag("Wall"))
            {
                Vector3 targetdirection = target.transform.position - transform.position;
                float angle = Mathf.Atan2(targetdirection.y, targetdirection.x) * Mathf.Rad2Deg - 90;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 500);
                _rb.AddForce(transform.forward);

            }
        }
    }


    public Vector2 RandomVector(float min, float max)
    {
        var x = Random.Range(min, max);
        var y = Random.Range(min, max);
        return new Vector2(x, y);
    }
}
