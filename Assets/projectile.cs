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
        // ��Ǩ�ͺ����������ö�ԧ����С������
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            StartCoroutine(FireProjectile());
        }
    }

    private IEnumerator FireProjectile()
    {
        canFire = false;  // ��ش����ԧ�����ҧ��˹�ǧ����

        // �ʴ� ray ����ԧ�ҡ���˹������
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.magenta, 5f);

        RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

        if (hit.collider != null)
        {
            target.transform.position = new Vector2(hit.point.x, hit.point.y);

            // �ӹǳ�������Ǣͧ����ع
            Vector2 projectileVelocity = CalculateProjectileVelocity(shootPoint.position, hit.point, 1f);

            // ���ҧ����ع��С�˹���������
            Rigidbody2D firedBullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            firedBullet.linearVelocity = projectileVelocity;  // �� velocity ᷹ linearVelocity

            // ����¡���ع��ѧ�ҡ 2 �Թҷ�
            Destroy(firedBullet.gameObject, 2f);
        }

        // ˹�ǧ��������ԧ�ء 1.5 �Թҷ�
        yield return new WaitForSeconds(fireRate);
        canFire = true;  // ����Ͷ֧���Ҩ�����ö�ԧ���ա����
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
