using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    void Awake() {
        // ensure return from menu resets the timescale
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    // stop game when showing buttons 
    // vice versa for hiding the buttons
    public void ShowButtons() {
        if (gameObject != null) gameObject.SetActive(true);
        Time.timeScale = 0f;
    }
    public void HideButtons() {
        if (gameObject != null) gameObject.SetActive(false);
        Time.timeScale = 1f;
    }

    // functions to be called by the buttons
    public void ExitToMenu() {
        SceneManager.LoadScene("Menu");
    }
    public void RestartGame() {
        SceneManager.LoadScene("Voidblitz");
    }
    public void HandlePause() {
        if (!gameObject.activeSelf) ShowButtons();
        else HideButtons();
    }
}
