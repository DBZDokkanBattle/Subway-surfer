using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private float baseFov = 50;
    private Vector3 offset = new Vector3(0,1.5f,-1.5f);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;

        //Camera.main.fieldOfView = Mathf.Lerp(baseFov, baseFov + player.GetComponent<MoveForward>().tempSpeed/10, 0);
        Camera.main.fieldOfView = baseFov+ player.GetComponent<MoveForward>().speed / 20 + player.GetComponent<MoveForward>().tempSpeed/5;
    }
}
