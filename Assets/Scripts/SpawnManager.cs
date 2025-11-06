using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    private float startTime = 2.0f;
    public float delayTime = 1.0f;
    public float coralDelay = 3.5f;
    public float decorationDelay = 0.1f;
    public int range;
    private bool spawnable;
    private int obstacledistance = 40;
    private float lastPosition;

    public GameObject[] ObstaclePrefabs;
    public GameObject coralPrefab;



    private GameManager gm;

    void Start()
    {
        lastPosition = transform.position.z;
        obstacledistance = 5;
        spawnable = true;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (lastPosition - transform.position.z > obstacledistance && transform.position.z > 10) { 
            spawnObstaclePrefabs(); 
            SpawnCoral();
            lastPosition = transform.position.z;
            Debug.Log(transform.position.z - lastPosition);
        }
        Debug.Log(transform.position.z);
        Debug.Log(lastPosition);
    }

    void spawnObstaclePrefabs()
    {
        if (gm == null || !gm.canSpawn) return;

        int lane = Random.Range(-range, range+1);
        int index = Random.Range(0, ObstaclePrefabs.Length);
        Vector3 spawnPos = new Vector3(lane, 0.5f, 80 + player.transform.position.z);

        Instantiate(ObstaclePrefabs[index], spawnPos, ObstaclePrefabs[index].transform.rotation);

        // occasional second obstacle
        if (Random.Range(0, 3) == 2)
        {
            lane = Random.Range(-range, range + 1);
            index = Random.Range(0, ObstaclePrefabs.Length);
            spawnPos = new Vector3(lane, 0.5f, 90 + player.transform.position.z);
            Instantiate(ObstaclePrefabs[index], spawnPos, ObstaclePrefabs[index].transform.rotation);
        }
    }

    void SpawnCoral()
    {
        if (gm == null || !gm.canSpawn) return;

        if (Random.value < 0.6f)
        {
            int lane = Random.Range(-range, range + 1);
            float randomOffsetZ = Random.Range(35f, 50f);
            float randomY = Random.Range(0.3f, 0.8f);

            Vector3 spawnPos = new Vector3(lane, randomY, player.transform.position.z + randomOffsetZ);
            Instantiate(coralPrefab, spawnPos, Quaternion.identity);
        }
    }

  
    
    

}
