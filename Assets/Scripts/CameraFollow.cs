using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraFollow : MonoBehaviour
{
    public GameObject player;
    private float baseFov = 50;
    public Vector3 offset = new Vector3(0,1.5f,-2.5f);

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(player.transform.position + offset, player.transform.position + offset - Vector3.forward * Mathf.Sqrt(player.GetComponent<MoveForward>().tempSpeed*0.7f),0.2f);

        //Camera.main.fieldOfView = Mathf.Lerp(baseFov, baseFov + player.GetComponent<MoveForward>().tempSpeed/10, 0);
        Camera.main.fieldOfView = baseFov+ player.GetComponent<MoveForward>().speed / 20 + player.GetComponent<MoveForward>().tempSpeed/5;
    }
}
