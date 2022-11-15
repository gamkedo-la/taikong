using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private UIDocument SettingsUIDocument;
    // private
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void ReturnToMenu() {
        SceneManager.LoadScene(0);
    }

    public void ShareScore() {
        // TODO share high-score button
    }

    public void OpenDebugMenu() {

    }

    public void ViewCredits() {
        SceneManager.LoadScene(6);
    }

    public void ViewSettings()
    {
        SettingsUIDocument.gameObject.SetActive(true);
    }

    public void DebugLoadCity() {
        SceneManager.LoadScene(1);
    }

    public void DebugLoadMountains() {
        SceneManager.LoadScene(2);
    }

    public void DebugLoadSpace() {
        SceneManager.LoadScene(3);
    }

    public void DebugLoadTutorial() {
        SceneManager.LoadScene(4);
    }
}
