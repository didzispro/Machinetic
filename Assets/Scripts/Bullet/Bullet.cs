using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Space(5)]
    public float speed = 10f;
    [Space(5)]
    public float explosionPower = 100f; 

    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        Destroy(gameObject, 3.0f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (playerRb != null)
            {
                float dir = Mathf.Sign(playerRb.position.x - transform.position.x);
                playerRb.AddForce(new Vector2(dir, 0f) * explosionPower, ForceMode2D.Impulse);
                Destroy(gameObject);
            } 
            
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (enemyRb != null)
            {
                float dir = Mathf.Sign(enemyRb.position.x - transform.position.x);
                enemyRb.AddForce(new Vector2(dir, 0f) * explosionPower, ForceMode2D.Impulse);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Jump"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Lava"))
        {
            Destroy(gameObject);
        }
    }
}