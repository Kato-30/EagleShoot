using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    private Rigidbody2D rb;

    void Start()
    {
        speed = PlayerPrefs.GetFloat("BulletSpeed", 10f);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void Update()
    {
        if (transform.position.y > Camera.main.orthographicSize)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Eagle")
        {
            Destroy(gameObject);
        }
    }
}
