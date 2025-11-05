using UnityEngine;

public class RiverbankDecorationSpawner : MonoBehaviour
{
    public GameObject player;                    // your fish/player
    public GameObject[] decorationPrefabs;       // list of bank decorations
    public float spawnInterval = 0.5f;           // how often to spawn
    public int decorationsPerBank = 2;           // number of decorations per bank each interval
    public float bankY = 1.8f;                   // fixed Y height for decorations
    public float bankXOffset = 8f;               // distance from center to bank
    public float zStartOffset = 100f;             // starting distance in front of player
    public float zSpacing = 5f;                  // spacing between decorations
    public bool canSpawn = true;                 // controlled by GameManager

    void Start()
    {
        InvokeRepeating(nameof(SpawnDecorations), 0.5f, spawnInterval);
    }

    void SpawnDecorations()
    {
        if (!canSpawn || decorationPrefabs.Length == 0 || player == null)
            return;

        float[] bankPositions = { -bankXOffset, bankXOffset }; // left & right banks

        foreach (float x in bankPositions)
        {
            for (int i = 0; i < decorationsPerBank; i++)
            {
                GameObject decorPrefab = decorationPrefabs[Random.Range(0, decorationPrefabs.Length)];

                // Z position spaced evenly in front of player
                float spawnZ = player.transform.position.z + zStartOffset + i * zSpacing;

                // Slight random X offset to make it look natural
                float spawnX = x + Random.Range(-0.2f, 0.2f);

                Vector3 spawnPos = new Vector3(spawnX, bankY, spawnZ);

                // Keep rotation fixed
                Instantiate(decorPrefab, spawnPos, decorPrefab.transform.rotation);
            }
        }
    }
}