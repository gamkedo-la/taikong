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
        SceneManager.LoadScene(activeScene.buildIndex + 1);
    }
}
