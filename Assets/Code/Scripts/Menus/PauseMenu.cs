using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool isPause = false;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] private GameObject playerUI;
    [SerializeField] public string mainMenuName;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void OnDisable()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Pause") || Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPause)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        isPause = false;
        playerUI.SetActive(true);
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }
    
    void Pause()
    {
        isPause = true;
        playerUI.SetActive(false);
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0;
    }

    public void LoadMenu()
    {
        Debug.Log("Load main menu!");
        Resume();
        SceneManager.LoadScene(mainMenuName);
    }
    public void Quit()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }

}
