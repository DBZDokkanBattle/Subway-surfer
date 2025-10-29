using UnityEngine;

public class DespawnOutOfBounds : MonoBehaviour
{
    public GameObject player;

    // Update is called once per frame

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (transform.position.z - player.transform.position.z < -20) { Destroy(gameObject); }
    }
}
