using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHearts = 3;
    public int currentHearts;

    public GameObject[] heartObjects; 

    void Start()
    {
        currentHearts = maxHearts;
        UpdateHearts();
    }

    public void TakeDamage(int amount)
    {
        currentHearts -= amount;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
        UpdateHearts();
    }

    public void Heal(int amount)
    {
        currentHearts += amount;
        currentHearts = Mathf.Clamp(currentHearts, 0, maxHearts);
        UpdateHearts();
    }

    void UpdateHearts()
    {
        for (int i = 0; i < heartObjects.Length; i++)
        {
            // เปิดหัวใจเฉพาะอันที่ยังมีเลือดอยู่
            heartObjects[i].SetActive(i < currentHearts);
        }
    }
}
