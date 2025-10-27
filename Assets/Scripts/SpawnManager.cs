using System.Runtime.CompilerServices;
using UnityEngine;

public class SpawnManager : MonoBehaviour
  
{
    public GameObject player;
    private float startTime = 4.0f;
    private float delayTime = 2.0f;
    //private Vector3 spawnPos = new Vector3(0,0,-20);
    public GameObject[] ObstaclePrefabs;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("spawnObstaclePrefabs", startTime, delayTime);
    }


    void spawnObstaclePrefabs()
    {

        int lane = Random.Range(-1, 2);
        int ObstacleIndex = Random.Range(0, ObstaclePrefabs.Length);
        Vector3 spawnPos = new Vector3(lane, 0.5f, 40 + player.transform.position.z);

        Instantiate(ObstaclePrefabs[ObstacleIndex], spawnPos , ObstaclePrefabs[ObstacleIndex].transform.rotation);
        
    }

}
