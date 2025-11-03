using UnityEngine;

public class WaterFlow : MonoBehaviour
{
    public Material waterMat;
    public Vector2 flowDirection = new Vector2(1f, 0f);
    public float flowSpeed = 0.1f;

    void Update()
    {
        Vector2 offset = flowDirection * Time.time * flowSpeed;
        waterMat.mainTextureOffset = offset;
    }
}