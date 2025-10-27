using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    public GameObject street;

    private void OnTriggerEnter(Collider other)
    {
       Instantiate(street, new Vector3(0,0,20) + transform.position, Quaternion.identity); 
    }
}
