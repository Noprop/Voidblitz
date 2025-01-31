using UnityEngine;

public class EnemyScript : HealthBase {
    [SerializeField] private Player player;
    private Weapon weapon;
    private SpriteRenderer rendererComponent;
    private bool hasSpawned = false;

    // index 0 = normal shot
    // index 1 = homing missile
    void Start() {
        weapon = GetComponentInChildren<Weapon>(); 
        rendererComponent = GetComponent<SpriteRenderer>();

        if (player == null) 
            player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        // Wait for the enemy to appear within view
        if (!hasSpawned && rendererComponent.isVisible)
            hasSpawned = true;
        if (!hasSpawned) return;

        if (weapon != null) {
            if (weapon.CanAttack(0)) // normal shot
                weapon.Attack(0, false);

            if (weapon.CanAttack(1)) // homing missile
                weapon.Attack(1, false);
        }

        // Destroy out of screen if still alive:
        if (!rendererComponent.isVisible && hp > 0)
            Destroy(gameObject);
    }

    private void OnDestroy() {
        if (hp <= 0) {
            player.IncreaseScore(1);
        }
    }
}
