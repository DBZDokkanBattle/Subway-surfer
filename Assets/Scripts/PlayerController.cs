using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    public float speed;
    private float horizontalInput;
    private float lastHorizontalInput;
    private GameObject GameManager;
    // Update is called once per frame

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }


    void Update()
    {
        if (GameManager.GetComponent<GameManager>().isGameActive) { 
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

            if (transform.position.x > 2.5)
            {
                transform.position = new Vector3((float)2.5, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -2.5)
            {
                transform.position = new Vector3((float)-2.5, transform.position.y, transform.position.z);
            }
        }
    }
}
