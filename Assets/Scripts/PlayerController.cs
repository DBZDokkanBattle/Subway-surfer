using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float horizontalInput;
    private float lastHorizontalInput;
    // Update is called once per frame
    
    
    void Update()
    {
        if (horizontalInput != 0)
        {
            lastHorizontalInput = horizontalInput; 
        }

        horizontalInput = Input.GetAxis("Horizontal");

        if ((horizontalInput < 0 && lastHorizontalInput > 0) || (horizontalInput > 0 && lastHorizontalInput < 0))
        {
            MoveForward.instance.addTempSpeed();
        }

        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);

        if (transform.position.x > 1) 
        { 
            transform.position = new Vector3(1, transform.position.y, transform.position.z);
        }

        if (transform.position.x < -1)
        {
            transform.position = new Vector3(-1, transform.position.y, transform.position.z);
        }
    }
}
