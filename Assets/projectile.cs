using UnityEngine;
using System.Collections;
public class Projectile : MonoBehaviour
{
    public Transform shootPoint;
    public GameObject target;
    public Rigidbody2D bulletPrefab;
    public float fireRate = 3f; 
    private bool canFire = true; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            StartCoroutine(FireProjectile());
        }
    }

    private IEnumerator FireProjectile()
    {
        canFire = false; 

        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta, 5f);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider != null)
        {
           
            target.transform.position = new Vector2(hit.point.x, hit.point.y);

            
            Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);

            
            Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);

            
            firedBullet.linearVelocity = projectileVelocity;

           
            Destroy(firedBullet.gameObject, 3f);
        }

        
        yield return new WaitForSeconds(fireRate);
        canFire = true;
    }

    Vector2 CalculateProjectileVelocity(Vector2 origin, Vector2 target, float time)
    {
        Vector2 distance = target - origin;

        float velocityX = distance.x / time;
        float velocityY = distance.y / time + 0.5f * Mathf.Abs(Physics2D.gravity.y) * time;

        return new Vector2(velocityX, velocityY);
    }
}
