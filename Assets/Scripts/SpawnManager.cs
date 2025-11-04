using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
  
{
    public GameObject player;
    private float startTime = 4.0f;
    public float delayTime = 1.0f;
    public float coralDelay = 3.5f;
    //private Vector3 spawnPos = new Vector3(0,0,-20);
    public GameObject[] ObstaclePrefabs;
    public GameObject coralPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("spawnObstaclePrefabs", startTime, delayTime);
        InvokeRepeating("SpawnCoral", startTime + 1f, coralDelay);

    }

    private void OnTriggerEnter(Collider other) 
    {
        spawnObstaclePrefabs();
    } 


    void spawnObstaclePrefabs()
    {

        int lane = Random.Range(-1, 2);
        int ObstacleIndex = Random.Range(0, ObstaclePrefabs.Length);
        Vector3 spawnPos = new Vector3(lane, 0.5f, 40 + player.transform.position.z);

        Instantiate(ObstaclePrefabs[ObstacleIndex], spawnPos , ObstaclePrefabs[ObstacleIndex].transform.rotation);

        if (Random.Range(0, 3) == 2) {
             lane = Random.Range(-1, 2);
             ObstacleIndex = Random.Range(0, ObstaclePrefabs.Length);
             spawnPos = new Vector3(lane, 0.5f, 40 + player.transform.position.z);

            Instantiate(ObstaclePrefabs[ObstacleIndex], spawnPos, ObstaclePrefabs[ObstacleIndex].transform.rotation);

        }
        
    }
    
    void SpawnCoral()
    {
        // Random chance to spawn coral (so it doesn’t spawn every time)
        if (Random.value < 0.6f) // 60% chance
        {
            int lane = Random.Range(-1, 2); // same lane setup
            float randomOffsetZ = Random.Range(35f, 50f);
            float randomY = Random.Range(0.3f, 0.8f); // corals appear at slightly different heights

            Vector3 spawnPos = new Vector3(lane, randomY, player.transform.position.z + randomOffsetZ);

            Instantiate(coralPrefab, spawnPos, Quaternion.identity);
        }
    }
   
  

  

}
