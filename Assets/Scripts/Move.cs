using UnityEngine;

public class Move : MonoBehaviour

    
{
    public float speed;
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        speed = -player.GetComponent<MoveForward>().totalSpeed;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
