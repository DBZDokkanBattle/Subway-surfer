using UnityEngine;

public class Coral : MonoBehaviour
{
    public float turnSpeed = 90f;
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        
        ScoreManager.Instance.AddScore(1);
        
        
        Destroy(gameObject);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0,0, turnSpeed * Time.deltaTime);
    }
}
