using UnityEngine;

public class Health : MonoBehaviour
{
    public int healAmount = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HealthSystem health = other.GetComponent<HealthSystem>();
            if (health != null)
            {
                health.Heal(healAmount);
                Destroy(gameObject);
            }
        }
    }
}
