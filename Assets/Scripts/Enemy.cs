using UnityEngine;

public class EnemyScript : HealthBase {
    [SerializeField] private Player player;
    private Weapon weapon;
    private SpriteRenderer rendererComponent;
    private bool hasSpawned = false;

    void Start() {
        weapon = GetComponentInChildren<Weapon>(); 
        rendererComponent = GetComponent<SpriteRenderer>();

        if (player == null) player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    void Update() {
        if (!hasSpawned && rendererComponent.isVisible) hasSpawned = true;
        if (!hasSpawned) return;

        if (weapon != null && weapon.CanAttack) {
            weapon.Attack(false);
        }

        if (!rendererComponent.isVisible && hp > 0) 
            Destroy(gameObject);
    }

    private void OnDestroy() {
        if (hp <= 0)
            player.IncreaseScore(1);
    }
}
