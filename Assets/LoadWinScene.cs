using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWinScene : MonoBehaviour
{
    public string win = "YourSceneName";
    // ���¡������ͪ��ѵ�ط���� Trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name); // ����ҷӧҹ�������

        if (other.CompareTag("Player"))
        {
            Debug.Log("Loading scene: " + win);
            SceneManager.LoadScene(win);
        }
    }

}
