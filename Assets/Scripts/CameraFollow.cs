using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset = new Vector3(0,1,-2);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
        
    }
}
