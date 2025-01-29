using UnityEngine;

public class HealthBase : MonoBehaviour {
    public int maxHp = 1;
    public int hp = 1;
    public bool isEnemy = true;

    public virtual void DestroySelf(GameObject self) {
        Destroy(self);
    }
    public virtual void OnHealthUpdate(int currentHp, int maxHp) { }

    public void Damage(int damageCount) {
        hp -= damageCount;
        if (hp <= 0) DestroySelf(gameObject);
        OnHealthUpdate(hp, maxHp);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        Shot shot = other.gameObject.GetComponent<Shot>();
        if (shot != null) {
            // prevent friendly fire
            if (shot.fromPlayer && isEnemy || !shot.fromPlayer && !isEnemy) {
                Damage(shot.damage);
                Destroy(shot.gameObject);
            }
        }
    }
}
