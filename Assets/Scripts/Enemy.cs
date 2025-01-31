using UnityEngine;
using System.Collections;

public class EnemyScript : HealthBase {
    [SerializeField] private Player player;
    private Weapon weapon;
    private SpriteRenderer rendererComponent;
    private bool hasSpawned = false;

    public bool canDodge = true;
    public float dodgeCooldown = 1f;

    private Move enemyMove;
    private float lastDodgeTime = -999f;
    private Vector2 originalSpeed;

    void Start() {
        weapon = GetComponentInChildren<Weapon>(); 
        rendererComponent = GetComponent<SpriteRenderer>();
        enemyMove = GetComponent<Move>();

        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        }
    }

    void Update() {
        // Wait for the enemy to appear within view
        if (!hasSpawned && rendererComponent.isVisible) {
            hasSpawned = true;
        }
        if (!hasSpawned) return;

        if (canDodge) {
            // don't let our boss move off screen
            float leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
            float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;

            if (transform.position.x - 0.5 < leftEdge) {
                enemyMove.direction = new Vector2(1, 0);
            }
            if (transform.position.x + 0.5 > rightEdge) {
                enemyMove.direction = new Vector2(-1, 0);
            }
        }

        if (weapon != null) {
            if (weapon.CanAttack(0)) { // normal shot
                weapon.Attack(0, false);
            }
            if (weapon.CanAttack(1)) { // homing missile
                weapon.Attack(1, false);
            }
        }

        // Destroy out of screen if still alive
        if (!rendererComponent.isVisible && hp > 0) {
            Destroy(gameObject);
        }

        // -- Dodge Logic --
        if (canDodge && Time.time - lastDodgeTime >= dodgeCooldown) {
            Shot closestShot = FindClosestPlayerShot();
            if (closestShot != null && IsShotHeadingTowardMe(closestShot)) {
                StartCoroutine(DodgeRoutine());
            }
        }
    }

    private void OnDestroy() {
        if (hp <= 0) {
            player.IncreaseScore(1);
        }
    }

    // Find all player's shots
    private Shot FindClosestPlayerShot() {
        Shot[] allShots = FindObjectsByType<Shot>(FindObjectsSortMode.None);
        Shot closest = null;
        float minDist = float.MaxValue;

        foreach (var shot in allShots) {
            if (!shot.fromPlayer) continue; 
            
            float dist = Vector3.Distance(transform.position, shot.transform.position);
            if (dist < minDist) {
                minDist = dist;
                closest = shot;
            }
        }
        return closest;
    }

    // Check to see if a player's shot is on the way
    private bool IsShotHeadingTowardMe(Shot shot) {
        Move shotMove = shot.GetComponent<Move>();
        if (shotMove == null) return false;

        Vector3 shotDirection = shotMove.direction.normalized;
        Vector3 vectorToEnemy = (transform.position - shot.transform.position).normalized;

        // Dot > 0 => traveling roughly toward the enemy
        float dot = Vector3.Dot(shotDirection, vectorToEnemy);
        return dot > 0.99f;
    }

    // Change speed, choose direction, then back to normal
    private IEnumerator DodgeRoutine() {
        lastDodgeTime = Time.time;
        if (enemyMove == null) yield break;
        originalSpeed = enemyMove.speed;

        // get the location of the right edge of the screen
        float leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        float rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x;
        float topEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0)).y;
        float bottomEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).y;
        float currentX = transform.position.x;
        float currentY = transform.position.y;

        // choose a random direction to dodge; however check if it'll still be in bounds
        int dodgeDirX = (Random.value < 0.05f) ? -1 : 1;
        int dodgeDirY = (Random.value < 0.5f) ? -1 : 1;
        if (dodgeDirX == 1 && currentX - 8 < leftEdge) dodgeDirX = 1;
        if (dodgeDirX == 1 && currentX + 8 > rightEdge) dodgeDirX = -1;
        if (dodgeDirY == 1 && currentY + 4 > topEdge) dodgeDirY = -1;
        if (dodgeDirY == -1 && currentY - 4 < bottomEdge) dodgeDirY = 1;

        // horizontal speed is a bit faster than vertical
        enemyMove.speed = new Vector2(8, 4);
        enemyMove.direction = new Vector2(dodgeDirX, dodgeDirY);

        // dodge for 1 second
        yield return new WaitForSeconds(1f);

        enemyMove.speed = originalSpeed;
        enemyMove.direction = new Vector2(-1, 0);
    }
}
