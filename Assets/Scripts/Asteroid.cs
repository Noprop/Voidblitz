using UnityEngine;
using UnityEngine.UI;

public class Asteroid : HealthBase {
    [SerializeField] private Slider slider;
    private Animator explode;

    void Start() {
        explode = GetComponent<Animator>();
    }

    public void UpdateHealth(int hp, int maxHp) {
        float adjustedFill = Mathf.Clamp01((float)hp / maxHp);
        slider.value = adjustedFill;
    }

    public void DestroySelf() {
        Canvas canvas = slider.GetComponentInParent<Canvas>();
        canvas.enabled = false;
        explode.Play("Asteroid_Explode");
        Destroy(gameObject, explode.GetCurrentAnimatorStateInfo(0).length);
    }
}
