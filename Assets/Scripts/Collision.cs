using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MoveForward moveForward;
    public ParticleSystem headSpinningVFX;
    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Collision")) 
        {
			headSpinningVFX.Play();
            MoveForward.instance.hitObject();
            Destroy(other.gameObject, 0.1f);
        }
    }
}
