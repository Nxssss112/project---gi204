using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    public int damageAmount = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem health = other.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.TakeDamage(damageAmount);
            }
        }
    }
}
