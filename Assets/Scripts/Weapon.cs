using UnityEngine;

public class Weapon : MonoBehaviour {
    public Transform shotPrefab;
    public float shootingRate = 0.3f;

    // track time between shots
    private float shootCooldown;

    void Start() { 
        shootCooldown = 0f;
    }
    void Update() {
        if (shootCooldown > 0) {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(bool isPlayer) {
        if (CanAttack) {
            // slightly randommize the shooting rate for non-players
            if (!isPlayer) {
                shootCooldown = shootingRate + Random.Range(0f, 0.5f);
            } else {
                shootCooldown = shootingRate;
            }

            var shotTransform = Instantiate(shotPrefab);
            shotTransform.position = transform.position;

            // initialize our ammo
            Shot shot = shotTransform.GetComponent<Shot>();
            if (shot != null) {
                shot.fromPlayer = isPlayer;
            }

            // initialize direction of the ammo
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null) {
                if (isPlayer) {
                    // Rotate the direction by an additional -90 degrees for correct bullet direction
                    Vector2 shootDirection = transform.up;
                    float shootAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg - 90;
                    Vector2 rotatedDirection = new Vector2(
                        Mathf.Cos(shootAngle * Mathf.Deg2Rad),
                        Mathf.Sin(shootAngle * Mathf.Deg2Rad)
                    );
                    move.direction = rotatedDirection;
                } else
                    move.direction = new Vector2(-1, 0);
            }
        }
    }

    public bool CanAttack {
        get { return shootCooldown <= 0; }
    }
}
