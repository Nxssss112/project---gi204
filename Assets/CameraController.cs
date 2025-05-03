using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform obj;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (obj != null)
        {
            transform.position = new Vector3(obj.position.x, transform.position.y, transform.position.z);
        }
    }
}
