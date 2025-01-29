using UnityEngine;
using UnityEngine.UI;

public class Asteroid : HealthBase {
    [SerializeField] private Slider slider;
    private Animator explode;

    void Start() {
        explode = GetComponent<Animator>();
    }

    public override void OnHealthUpdate(int hp, int maxHp) {
        float adjustedFill = Mathf.Clamp01((float)hp / maxHp);
        slider.value = adjustedFill;
    }

    public override void DestroySelf(GameObject self) {
        Canvas canvas = slider.GetComponentInParent<Canvas>();
        canvas.enabled = false;
        explode.Play("Asteroid_Explode");
        Destroy(self, explode.GetCurrentAnimatorStateInfo(0).length);
    }
}
