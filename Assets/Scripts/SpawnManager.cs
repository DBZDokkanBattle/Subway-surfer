using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    private float startTime = 2.0f;
    public float delayTime = 1.0f;
    public float coralDelay = 3.5f;

    public GameObject[] ObstaclePrefabs;
    public GameObject coralPrefab;

    private GameManager gm;

    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

        InvokeRepeating(nameof(spawnObstaclePrefabs), startTime, delayTime);
        InvokeRepeating(nameof(SpawnCoral), startTime + 1f, coralDelay);
    }

    void spawnObstaclePrefabs()
    {
        
        if (gm == null || !gm.canSpawn) return;

        int lane = Random.Range(-1, 2);
        int ObstacleIndex = Random.Range(0, ObstaclePrefabs.Length);
        Vector3 spawnPos = new Vector3(lane, 0.5f, 40 + player.transform.position.z);

        Instantiate(ObstaclePrefabs[ObstacleIndex], spawnPos, ObstaclePrefabs[ObstacleIndex].transform.rotation);

        if (Random.Range(0, 3) == 2)
        {
            lane = Random.Range(-1, 2);
            ObstacleIndex = Random.Range(0, ObstaclePrefabs.Length);
            spawnPos = new Vector3(lane, 0.5f, 40 + player.transform.position.z);
            Instantiate(ObstaclePrefabs[ObstacleIndex], spawnPos, ObstaclePrefabs[ObstacleIndex].transform.rotation);
        }
    }

    void SpawnCoral()
    {
        
        if (gm == null || !gm.canSpawn) return;

        if (Random.value < 0.6f)
        {
            int lane = Random.Range(-1, 2);
            float randomOffsetZ = Random.Range(35f, 50f);
            float randomY = Random.Range(0.3f, 0.8f);

            Vector3 spawnPos = new Vector3(lane, randomY, player.transform.position.z + randomOffsetZ);
            Instantiate(coralPrefab, spawnPos, Quaternion.identity);
        }
    }
}