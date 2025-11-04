using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed;
    public float tempSpeed;
    public float totalSpeed;
    public float maxTempSpeed;

    public float tempSpeedIncrease;
    public float tempSpeedDecrease;

    public float normalSpeedIncrease;
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
        totalSpeed = speed + tempSpeed;
        //transform.Translate(Vector3.forward * Time.deltaTime * currentSpeed);

        
        if (tempSpeed > 0)
            tempSpeed -= 0.05f;
        if (tempSpeed < 0)
            tempSpeed = 0;

        
        if (fishAnim != null)
        {
            fishAnim.speed = Mathf.Clamp(totalSpeed / 10f, 0.2f, 2f);

            
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                fishAnim.SetTrigger("Turn_Left");
            else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                fishAnim.SetTrigger("Turn_Right");
        }
    }

    public void hitObject()
    {
        speed = -1;
        tempSpeed = 0;
    }

    public void addTempSpeed()
    {
        if (tempSpeed < maxTempSpeed) tempSpeed += tempSpeedIncrease;
    }

    void speedUp()
    {
        if(speed < 0) speed += normalSpeedIncrease;
        if (speed >= 0) speed += normalSpeedIncrease;
    }
}