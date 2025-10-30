using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int maxTerrains = 30;
    public int generatedTerrains;
    public bool isGameActive = true;


    public void increaseGeneratedTerrain() { 
        generatedTerrains++;
    }

    public void endGame() { 
        isGameActive = false;
    }
}
