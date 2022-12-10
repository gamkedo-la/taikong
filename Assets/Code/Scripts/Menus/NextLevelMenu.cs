using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelMenu : MonoBehaviour
{
    private Scene activeScene; 
    // Start is called before the first frame update
    void Start()
    {
        activeScene = SceneManager.GetActiveScene();
    }

    public void OnLevelComplete() {
        Debug.Log("Next level please");
        // If the player is on level 1, 2 or 3, load the next level
        // Else, show the gameover level for their final score
        if (activeScene.buildIndex < 2) {
            SceneManager.LoadScene(activeScene.buildIndex + 1);
        } else {
            SceneManager.LoadScene(4);
        }
    }
}
