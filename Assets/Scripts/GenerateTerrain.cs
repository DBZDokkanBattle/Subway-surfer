using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    [Header("Terrain Prefabs")]
    public GameObject street;
    public GameObject terrain;
    public GameObject caveStart;     
    public GameObject caveTerrain;   
    public GameObject endingTerrain;

    [Header("References")]
    public GameObject GameManager;

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
        }
        else if (count == gm.maxTerrains)
        {
            SpawnEndingTerrain();
            gm.canSpawn = false;
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
    }

    private void SpawnCaveTerrain()
    {
        Instantiate(street, transform.position + new Vector3(0, 0, 60), Quaternion.identity);
        Instantiate(caveTerrain, transform.position + new Vector3(-35, -4, 25), Quaternion.identity);
    }

    private void SpawnEndingTerrain()
    {
        Instantiate(endingTerrain, transform.position + new Vector3(0, 0, 30), Quaternion.identity);
    }
}
