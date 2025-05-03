using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public float acceleration = 2f;
    public float lifeTime = 5f;

    private float currentSpeed;
    private Vector2 moveDirection;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentSpeed = speed;
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        currentSpeed += acceleration * Time.deltaTime;
        rb.linearVelocity = moveDirection * currentSpeed;
    }

    public void SetDirectionToTarget(Vector2 targetPosition)
    {
        moveDirection = (targetPosition - (Vector2)transform.position).normalized;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1); // Å´ 1 ËÑÇã¨
            }

            Destroy(gameObject);
        }
    }
}
