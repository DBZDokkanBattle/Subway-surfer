using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    [Header("Terrain Prefabs")]
    public GameObject street;
    public GameObject terrain;
    public GameObject caveStart;     // The first cave piece (entrance)
    public GameObject caveTerrain;   // The repeating cave pieces
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

        if (count <= 10)
        {
            // 🟩 Phase 1: normal terrain
            SpawnNormalTerrain();
        }
        else if (count == 11)
        {
            // 🟫 Phase 2 start: cave entrance
            SpawnCaveStart();
        }
        else if (count > 11 && count <= 20)
        {
            // ⚫ Phase 2 continuation: cave terrain
            SpawnCaveTerrain();
        }
        else if (count > 20 && count < gm.maxTerrains)
        {
            // 🟩 Phase 3: normal terrain again
            SpawnNormalTerrain();
        }
        else if (count == gm.maxTerrains)
        {
            // 🏁 End
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
