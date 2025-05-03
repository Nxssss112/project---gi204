using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision2D : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Win"))
        {   
            SceneManager.LoadScene("win");
        }
    }
}
