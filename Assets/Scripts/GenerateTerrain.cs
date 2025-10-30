using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public GameObject street;
    public GameObject finish;
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
            Instantiate(street, new Vector3(0, 0, 20) + transform.position, Quaternion.identity);
        }
        else { Instantiate(finish, new Vector3(0, 0, 20) + transform.position, Quaternion.identity); }
    }
}
