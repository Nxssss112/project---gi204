using UnityEngine;

public class Red_Goop : MonoBehaviour
{
    public Transform[] movePoints;  
    public float speed = 2f;  
    private Vector2 velocity;  
    private Rigidbody2D rb;  
  
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        velocity = new Vector2(speed, 0);  
    }

    private void FixedUpdate()
    {
        MoveBetweenPoints();
    }
    private void MoveBetweenPoints()
    {
        
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

       
        if (rb.position.x <= movePoints[0].position.x && velocity.x < 0)
        {
            FlipCharacter();  
        }
        else if (rb.position.x >= movePoints[1].position.x && velocity.x > 0)
        {
            FlipCharacter();  
        }
    }
    
    private void FlipCharacter()
    {
        velocity *= -1;  
        Vector2 scale = transform.localScale;  
        scale.x *= -1;
        transform.localScale = scale;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))  
        {
            HealthSystem playerHealth = other.GetComponent<HealthSystem>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);  
            }
        }
    }

}
    
   

