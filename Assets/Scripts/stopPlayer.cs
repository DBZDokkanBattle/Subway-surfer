using UnityEngine;

public class stopPlayer : MonoBehaviour
{
    GameObject GameManager;

    private void Start()
    {
        GameManager = GameObject.Find("GameManager");   
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        GameManager.GetComponent<GameManager>().endGame();
    }
}
