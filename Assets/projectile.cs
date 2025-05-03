using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject target;
    public Rigidbody2D bulletPrefab;
    public float fireRate = 1.5f;
    private bool canFire = true;

    void Update()
    {
        // ตรวจสอบว่าเราสามารถยิงได้และกดเมาส์
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            StartCoroutine(FireProjectile());
        }
    }

    private IEnumerator FireProjectile()
    {
        canFire = false;  // หยุดการยิงระหว่างรอหน่วงเวลา

        // แสดง ray ที่ยิงจากตำแหน่งเมาส์
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta, 5f);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider != null)
        {
            target.transform.position = new Vector2(hit.point.x, hit.point.y);

            // คำนวณความเร็วของกระสุน
            Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);

            // สร้างกระสุนและกำหนดความเร็ว
            Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            firedBullet.linearVelocity = projectileVelocity;  // ใช้ velocity แทน linearVelocity

            // ทำลายกระสุนหลังจาก 2 วินาที
            Destroy(firedBullet.gameObject, 2f);
        }

        // หน่วงเวลาให้ยิงทุก 1.5 วินาที
        yield return new WaitForSeconds(fireRate);
        canFire = true;  // เมื่อถึงเวลาจะสามารถยิงได้อีกครั้ง
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        return new Vector2(velocityX, velocityY);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
