using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MoveForward moveForward;
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Collision")) 
        {
            MoveForward.instance.hitObject();
            Destroy(other.gameObject);
        }
    }
}
