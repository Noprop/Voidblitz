using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour {
    [SerializeField] private Canvas healthBarCanvas;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Transform target;

    private Camera mainCamera;

    void Start() {
        mainCamera = Camera.main;
    }

    public void OnHealthUpdate(int hp, int maxHp) {
        if (hp == 0 && healthBarCanvas.enabled) {
            healthBarCanvas.enabled = false;
            return;
        }
        float adjustedFill = Mathf.Clamp01((float)hp / maxHp);
        healthSlider.value = adjustedFill;
    }

    void LateUpdate() {
        healthBarCanvas.transform.rotation = mainCamera.transform.rotation;
    }
}
