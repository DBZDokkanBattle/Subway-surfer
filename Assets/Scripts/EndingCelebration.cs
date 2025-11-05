using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingCelebration : MonoBehaviour
{
    private float delay = 3f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        StartCoroutine(SwitchSceneAfterDelay());
    }

    // Update is called once per frame
    private System.Collections.IEnumerator SwitchSceneAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("LeaderBoard");
    }
   
        
    
        
    
}
