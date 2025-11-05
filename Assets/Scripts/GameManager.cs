using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxTerrains = 5;
    public int generatedTerrains;
    public bool isGameActive = true;
    public bool canSpawn = true; 


    public void increaseGeneratedTerrain() { 
        generatedTerrains++;
    }

    public void endGame() { 
        isGameActive = false;
    }
}
