using UnityEngine;

public class HealthScript : MonoBehaviour {
    public int maxHp = 1;
    public int hp = 1;
    public bool isEnemy = true;

    private Asteroid asteroid;

    void Start() {
        asteroid = gameObject.GetComponent<Asteroid>();
    }

    public void Damage(int damageCount){
        hp -= damageCount;
        // if (hp <= 0) Destroy(gameObject);
        if (hp <= 0 && asteroid != null) asteroid.DestroyAsteroid();
        if (asteroid != null) asteroid.UpdateHealth(hp, maxHp);
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
