using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using TMPro;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Color hoverColor;
    private TextMeshProUGUI buttonText;
    private Color defaultColor;

    void Start() {
        buttonText = GetComponentInChildren<TextMeshProUGUI>();
        defaultColor = buttonText.color;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        buttonText.color = hoverColor;
    }

    public void OnPointerExit(PointerEventData eventData) {
        buttonText.color = defaultColor;
    }

    public void StartGame() {
        SceneManager.LoadScene("Voidblitz");
    }
}