using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public GameObject street;
    public GameObject terrain;
    public GameObject endingTerrain; 
    public GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        var gm = GameManager.GetComponent<GameManager>();

        if (gm.generatedTerrains < gm.maxTerrains)
        {
            gm.increaseGeneratedTerrain();
            Instantiate(street, new Vector3(0, 0, 60) + transform.position, Quaternion.identity);
            Instantiate(terrain, new Vector3(0, 0, 80) + transform.position, Quaternion.identity);
        }
        else if (gm.generatedTerrains == gm.maxTerrains)
        {
            
            gm.increaseGeneratedTerrain();
            Instantiate(endingTerrain, transform.position + new Vector3(0, 0, 30), Quaternion.identity);
            gm.canSpawn = false;

        }
    }
}