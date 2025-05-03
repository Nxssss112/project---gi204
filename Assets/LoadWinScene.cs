using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadWinScene : MonoBehaviour
{
    public string win = "YourSceneName";
    // เรียกใช้เมื่อชนวัตถุที่มี Trigger
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.name); // ดูว่าทำงานหรือไม่

        if (other.CompareTag("Player"))
        {
            Debug.Log("Loading scene: " + win);
            SceneManager.LoadScene(win);
        }
    }

}
