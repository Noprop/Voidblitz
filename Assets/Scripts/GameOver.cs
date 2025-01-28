using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    private Button[] buttons;

    void Awake() {
        // get the buttons and disable them
        buttons = GetComponentsInChildren<Button>();
        HideButtons();
    }

    // change visibility of the buttons
    public void HideButtons() {
        foreach (var b in buttons) {
            b.gameObject.SetActive(false);
        }
    }
    public void ShowButtons() {
        foreach (var b in buttons) {
            b.gameObject.SetActive(true);
        }
    }

    // functions to be called by the buttons
    public void ExitToMenu() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
    public void RestartGame() {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Voidblitz");
    }
}
