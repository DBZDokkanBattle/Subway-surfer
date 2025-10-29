using System;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MoveForward moveForward;
    
    [SerializeField] private ParticleSystem headSpinningVFX;
    [SerializeField] private Animator fishAnim;
    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        headSpinningVFX = gameObject.GetComponentInChildren<ParticleSystem>();
        fishAnim = GetComponentInChildren<Animator>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collision")) return;
        
        headSpinningVFX.Play();
        fishAnim.SetTrigger("Hitting_Obstacle");
        
        MoveForward.instance.hitObject();
        Destroy(other.gameObject);
    }
}
