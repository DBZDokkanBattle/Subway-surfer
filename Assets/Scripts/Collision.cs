using UnityEngine;

public class Collision : MonoBehaviour
{
    [SerializeField] private ParticleSystem headSpinningVFX;
    [SerializeField] private Animator fishAnim;

    private void Awake()
    {
        headSpinningVFX = gameObject.GetComponentInChildren<ParticleSystem>();
        fishAnim = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collision")) return;

        headSpinningVFX.Play();
        if(fishAnim != null)
            fishAnim.SetTrigger("Hitting_Obstacle");

        MoveForward.instance.hitObject();
        Destroy(other.gameObject);
    }
}