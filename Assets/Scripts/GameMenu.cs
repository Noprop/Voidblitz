using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {
    private CanvasGroup canvasGroup;

    void Awake() {
        Time.timeScale = 1f;
        canvasGroup = GetComponent<CanvasGroup>();
        HideButtons();
    }

    public void ShowButtons() {
        canvasGroup.alpha = 1f;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        Time.timeScale = 0f;
    }

    public void HideButtons() {
        canvasGroup.alpha = 0f;
        canvasGroup.interactable = false;
        Time.timeScale = 1f;
        canvasGroup.blocksRaycasts = false;
    }

    // functions to be called by the buttons
    public void ExitToMenu() {
        SceneManager.LoadScene("Menu");
    }
    public void RestartGame() {
        SceneManager.LoadScene("Voidblitz");
    }
    public void HandlePause() {
        if (canvasGroup.alpha < 0.1f) ShowButtons();
        else HideButtons();
    }
}
