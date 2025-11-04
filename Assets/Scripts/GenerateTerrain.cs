using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public GameObject street;
    public GameObject finish;
    public GameObject terrain;
    public GameObject GameManager;
    private void Start()
    {
        GameManager = GameObject.Find("GameManager");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.GetComponent<GameManager>().generatedTerrains < GameManager.GetComponent<GameManager>().maxTerrains)
        {
            GameManager.GetComponent<GameManager>().increaseGeneratedTerrain();
            Instantiate(street, new Vector3(0, 0, 60) + transform.position, Quaternion.identity);
            Instantiate(terrain, new Vector3(0, 0, 80) + transform.position, Quaternion.identity);
        }
        else if(GameManager.GetComponent<GameManager>().generatedTerrains == GameManager.GetComponent<GameManager>().maxTerrains)
        {
            GameManager.GetComponent<GameManager>().increaseGeneratedTerrain();
            Instantiate(finish, new Vector3(0, 0, 20) + transform.position, transform.rotation); 
        }
    }
}
