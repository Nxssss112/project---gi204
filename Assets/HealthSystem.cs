using UnityEngine;
using UnityEngine.SceneManagement;  // ������ SceneManager

public class HealthSystem : MonoBehaviour
{
    public int maxHearts = 3;
    public int currentHearts;

    public GameObject[] heartObjects;

    public string sceneOnDeath = "YourSceneName"; // ��˹����ͫչ����ͧ�����Ŵ����ͼ����蹵��

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

        // ��Ǩ�ͺ�ҡ�������
        if (currentHearts == 0)
        {
            // ��Ŵ Scene ����˹�������������
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