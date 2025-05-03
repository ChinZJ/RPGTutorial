using UnityEngine;

/** Controls enemy movement. */
public class EnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Transform player;

    public float speed;
    public bool isChasing;

    // Start is called once before the first execution of Update after the MonoBehaviour is created.
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame.
    void Update()
    {
        if (isChasing == true)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        
    }

    // 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (player == null)
            {
                player = collision.transform;
            }
            isChasing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rb.linearVelocity = Vector2.zero;
            isChasing = false;
        }
    }
}
