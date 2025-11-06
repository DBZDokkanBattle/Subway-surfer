using UnityEngine;

public class Collision : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private ParticleSystem swimmingVFX;     // plays while swimming
    [SerializeField] private ParticleSystem headSpinningVFX; // plays on collision

    [Header("Animation")]
    [SerializeField] private Animator fishAnim;

    [Header("Sound")]
    [SerializeField] private AudioClip hitSound;      // the sound effect
    [SerializeField] private AudioSource audioSource; // reference to AudioSource

    private void Awake()
    {
        // Auto-assigns if not manually set
        if (fishAnim == null)
            fishAnim = GetComponentInChildren<Animator>();

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        // start swimming particles
        if (swimmingVFX != null)
            swimmingVFX.Play();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collision")) return;
        
        if (audioSource != null && hitSound != null)
            audioSource.PlayOneShot(hitSound);
        // stop swimming particles completely
        if (swimmingVFX != null)
            swimmingVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        // play head spinning particles
        if (headSpinningVFX != null)
        {
            headSpinningVFX.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
            headSpinningVFX.Play();
            Invoke(nameof(ResumeSwimming), 2f);
        }
        

        if (fishAnim != null)
        {
            fishAnim.SetTrigger("Hitting_Obstacle");
            Invoke(nameof(ResetHitAnimation), 0.8f);
        }

        MoveForward.instance.hitObject();
        Destroy(other.gameObject);
    }

    private void ResetHitAnimation()
    {
        if (fishAnim != null)
            fishAnim.ResetTrigger("Hitting_Obstacle");
    }

    private void ResumeSwimming()
    {
        if (swimmingVFX != null)
            swimmingVFX.Play();
    }
}
