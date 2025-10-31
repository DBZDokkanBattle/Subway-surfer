using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MoveForward : MonoBehaviour
{
    public float speed;
    public float tempSpeed;
    public static MoveForward instance;
    private GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
        instance = this;
        InvokeRepeating("speedUp", 0, 1);
    }
    // Update is called once per frame
    void Update()
    {
        if (GameManager.GetComponent<GameManager>().isGameActive)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * (speed + tempSpeed));
            if (tempSpeed > 0) { tempSpeed -= 0.05f; }
            if (tempSpeed < 0) { tempSpeed = 0; }
        }
    }

    public void hitObject() {
        speed = -1;
        tempSpeed = 0;
    }

    public void addTempSpeed() {
        if (tempSpeed < speed*1.5f)
        {
            tempSpeed += speed * 1.5f/(tempSpeed+1);
        }
    }

    void speedUp() {
        speed += 1f;
        if(speed >= 0){ speed += 0.5f; }
    }
}
