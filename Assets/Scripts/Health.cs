using UnityEngine;

public class HealthBase : MonoBehaviour {
    public int maxHp = 1;
    public int hp = 1;
    public bool isEnemy = true;

    private FloatingHealthBar healthBar;

    void Awake() {
        healthBar = gameObject.GetComponent<FloatingHealthBar>();
    }

    public virtual void DestroySelf(GameObject self) {
        Destroy(self);
    }

    public virtual void OnHealthUpdate(int currentHp, int maxHp) {
        if (healthBar != null) healthBar.OnHealthUpdate(currentHp, maxHp);
    }

    public void Damage(int damageCount) {
        hp -= damageCount;
        if (hp <= 0) DestroySelf(gameObject);
        OnHealthUpdate(hp, maxHp);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if (hp <= 0) return;

        Shot shot = other.gameObject.GetComponent<Shot>();
        if (shot != null) {
            // prevent friendly fire
            if (shot.fromPlayer && isEnemy || !shot.fromPlayer && !isEnemy) {
                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
            return;
        }
        HomingMissile missile = other.gameObject.GetComponent<HomingMissile>();
        if (missile != null) {
            if (missile.fromPlayer && isEnemy || !missile.fromPlayer && !isEnemy) {
                Damage(missile.damage);
                Destroy(missile.gameObject);
            }
        }
    }
}
