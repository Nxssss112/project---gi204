using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadProjectScenes()
    {
        SceneManager.LoadScene("Project_Scenes");
    }
}
