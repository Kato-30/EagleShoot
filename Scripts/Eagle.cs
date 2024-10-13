using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eagle : MonoBehaviour
{
    public float moveSpeed = 1.3f;
    public float changeInterval = 2f;
    private float timer;
    private Rigidbody2D rb;
    private Vector3 randomDirection;
    public Animator anim;
    float screenWidth;
    float screenHeight;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    void Update()
    {
        screenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        screenHeight = Camera.main.orthographicSize;

        // Di chuyển theo hướng ngẫu nhiên
        transform.Translate(moveSpeed * Time.deltaTime * randomDirection);

        if (transform.position.x > screenWidth || transform.position.x < -screenWidth || transform.position.y > screenHeight || transform.position.y < -screenHeight)
        {
            GameManager.Instance.EagleEscape();
            Destroy(gameObject);
        }

        // Đếm thời gian
        timer += Time.deltaTime;

        // Nếu hết thời gian thay đổi hướng, thì thay đổi hướng mới
        if (timer >= changeInterval)
        {
            ChangeDirection();
            timer = 0f;
        }

        moveSpeed += 0.002f;
    }

    void ChangeDirection()
    {
        // Tạo hướng di chuyển ngẫu nhiên
        randomDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(0.5f, 1f), 0f).normalized;
        if (randomDirection.x >= 0)
        {
            transform.localScale = new Vector3(-0.2f, 0.2f, 1);
        }
        else
        {
            transform.localScale = new Vector3(0.2f, 0.2f, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            rb.gravityScale = 1.0f;
            anim.SetBool("dead", true);
            AudioManager.Instance.PlayEagleDieSound();
            GameManager.Instance.AddScore(10);
            GameManager.Instance.AddCoin(5);
            Destroy(gameObject, .25f);
        }
    }
}
