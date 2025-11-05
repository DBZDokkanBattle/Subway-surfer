using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float rotationSpeed = 1f;
    public float startRotation = 75f;  
    private float currentRotation;

    void Start()
    {
       
        currentRotation = startRotation;
        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
    }

    void Update()
    {
      
        currentRotation += rotationSpeed * Time.deltaTime;

        
        RenderSettings.skybox.SetFloat("_Rotation", currentRotation);
    }
}