using UnityEngine;

public class Red_Phantom : MonoBehaviour
{
    public float speed = 2f;
    public float shootingRange = 5f;
    public float fireRate = 1f;
    public GameObject bullet;
    public GameObject bulletParent;
    private Transform player;
    private float nextFireTime;
    private Rigidbody2D rb;
    private Vector2 velocity;
    public Transform[] movePoints;
    private int currentPointIndex = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        MoveBetweenPoints();
        TryShoot();
    }

    private void MoveBetweenPoints()
    {
        if (movePoints.Length > 1)
        {
            Vector2 targetPosition = movePoints[currentPointIndex].position;
            velocity = (targetPosition - (Vector2)transform.position).normalized * speed;

            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

            // เช็คว่าถึงจุดที่ต้องการหรือยัง
            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                // เปลี่ยนไปยังจุดถัดไป
                currentPointIndex = (currentPointIndex + 1) % movePoints.Length;
                FlipCharacter();
            }
        }
    }

    private void TryShoot()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);

        if (distanceFromPlayer < shootingRange && Time.time > nextFireTime)
        {
            GameObject bulletInstance = Instantiate(bullet, bulletParent.transform.position, Quaternion.identity);
            Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
            bulletScript.SetDirectionToTarget(player.position);

            nextFireTime = Time.time + fireRate;
        }
    }

    private void FlipCharacter()
    {
        velocity *= -1;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
