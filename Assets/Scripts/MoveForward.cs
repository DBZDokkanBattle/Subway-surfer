using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    public float tempSpeed;
    public static MoveForward instance;

    [Header("References")]
    public Animator fishAnim; 

    private void Start()
    {
        instance = this;
        InvokeRepeating("speedUp", 0, 1);
    }

    void Update()
    {
        float currentSpeed = speed + tempSpeed;
        transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);

        // Reduce tempSpeed gradually
        if (tempSpeed > 0)
            tempSpeed -= 0.05f;
        if (tempSpeed < 0)
            tempSpeed = 0;

        
        if(fishAnim != null)
            fishAnim.speed = Mathf.Clamp(currentSpeed / 10f, 0.1f, 3f); 
    }

    public void hitObject()
    {
        speed = -1;
        tempSpeed = 0;
    }

    public void addTempSpeed()
    {
        if (tempSpeed < speed * 1.5f)
        {
            tempSpeed += speed * 1.5f / 10f + 10;
        }
    }

    void speedUp()
    {
        speed += 0.5f;
        if (speed >= 0)
            speed += 2;
    }
}