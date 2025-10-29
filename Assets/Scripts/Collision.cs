using System;
using UnityEngine;

public class Collision : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public MoveForward moveForward;
    
    [SerializeField]
    private ParticleSystem headSpinningVFX;
    // Update is called once per frame
    void Update()
    {

    }

    private void Awake()
    {
        headSpinningVFX = gameObject.GetComponentInChildren<ParticleSystem>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Collision")) return;
        
        headSpinningVFX.Play();
        MoveForward.instance.hitObject();
        Destroy(other.gameObject);
    }
}
