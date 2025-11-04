using UnityEngine;

public class generateObstacle : MonoBehaviour
{
    public GameObject player;
    public GameObject[] ObstaclePrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        int lane = Random.Range(-1, 2);
        int ObstacleIndex = Random.Range(0, ObstaclePrefabs.Length);
        Vector3 spawnPos = new Vector3(lane, 0.5f, 60 + player.transform.position.z);

        Instantiate(ObstaclePrefabs[ObstacleIndex], spawnPos, ObstaclePrefabs[ObstacleIndex].transform.rotation);

        if (Random.Range(1, 3) == 1)
        {
            lane = Random.Range(-1, 2);
            ObstacleIndex = Random.Range(0, ObstaclePrefabs.Length);
            spawnPos = new Vector3(lane, 0.5f, 60 + player.transform.position.z);

            Instantiate(ObstaclePrefabs[ObstacleIndex], spawnPos, ObstaclePrefabs[ObstacleIndex].transform.rotation);
        }
    }

    void spawnObstaclePrefabs()
    {

        int lane = Random.Range(-1, 2);
        int ObstacleIndex = Random.Range(0, ObstaclePrefabs.Length);
        Vector3 spawnPos = new Vector3(lane, 0.5f, 40 + player.transform.position.z);

        Instantiate(ObstaclePrefabs[ObstacleIndex], spawnPos, ObstaclePrefabs[ObstacleIndex].transform.rotation);

    }
}
