using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    [Header("Terrain Prefabs")]
    public GameObject street;
    public GameObject terrain;
    public GameObject caveStart;
    public GameObject caveTerrain;
    public GameObject endingTerrain;

    [Header("Special Effects")]
    public GameObject firefliesPrefab;   // Fireflies particle system prefab

    [Header("References")]
    public GameObject GameManager;

    // Internals
    private GameObject activeFireflies;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        var gm = GameManager.GetComponent<GameManager>();
        gm.increaseGeneratedTerrain();

        int count = gm.generatedTerrains;

        if (count <= 5)
        {
            SpawnNormalTerrain();
        }
        else if (count == 6)
        {
            SpawnCaveStart();
        }
        else if (count > 6 && count <= 10)
        {
            SpawnCaveTerrain();
        }
        else if (count > 10 && count < gm.maxTerrains)
        {
            SpawnNormalTerrain();
            StopFireflies(); // stop spawning fireflies once cave ends
        }
        else if (count == gm.maxTerrains)
        {
            SpawnEndingTerrain();
            gm.canSpawn = false;
            StopFireflies();
        }
    }

    private void SpawnNormalTerrain()
    {
        Instantiate(street, transform.position + new Vector3(0, 0, 60), Quaternion.identity);
        Instantiate(terrain, transform.position + new Vector3(0, 0, 80), Quaternion.identity);
    }

    private void SpawnCaveStart()
    {
        Instantiate(street, transform.position + new Vector3(0, 0, 60), Quaternion.identity);
        Instantiate(caveStart, transform.position + new Vector3(-35, -4, 25), Quaternion.identity);

        SpawnFirefliesInView();
    }

    private void SpawnCaveTerrain()
    {
        Instantiate(street, transform.position + new Vector3(0, 0, 60), Quaternion.identity);
        Instantiate(caveTerrain, transform.position + new Vector3(-35, -4, 25), Quaternion.identity);

        // Keep the same fireflies active if already spawned
        if (activeFireflies == null)
        {
            SpawnFirefliesInView();
        }
    }

    private void SpawnEndingTerrain()
    {
        Instantiate(endingTerrain, transform.position + new Vector3(0, 0, 30), Quaternion.identity);
    }

    private void SpawnFirefliesInView()
    {
        if (firefliesPrefab == null) return;

        // Find the main camera
        Camera mainCam = Camera.main;
        if (mainCam == null) return;

        // Spawn about 10 units in front of the camera, centered in the view
        Vector3 spawnPos = mainCam.transform.position + mainCam.transform.forward * 10f;

        activeFireflies = Instantiate(firefliesPrefab, spawnPos, Quaternion.identity);
    }

    private void StopFireflies()
    {
        if (activeFireflies != null)
        {
            Destroy(activeFireflies);
            activeFireflies = null;
        }
    }
}

