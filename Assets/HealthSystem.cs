using UnityEngine;
using UnityEngine.SceneManagement;  // เพื่อใช้ SceneManager

public class HealthSystem : MonoBehaviour
{
    public int maxHearts = 3;
    public int currentHearts;

    public GameObject[] heartObjects;

    public string sceneOnDeath = "YourSceneName"; // กำหนดชื่อซีนที่ต้องการโหลดเมื่อผู้เล่นตาย

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

        // ตรวจสอบหากหัวใจหมด
        if (currentHearts == 0)
        {
            // โหลด Scene ที่กำหนดเมื่อหัวใจหมด
            SceneManager.LoadScene(sceneOnDeath);
        }
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
            heartObjects[i].SetActive(i < currentHearts);
        }
    }
}