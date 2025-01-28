using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    // the ammo itself and adjustable shooting rate
    public Transform shotPrefab;
    public float shootingRate = 0.1f;

    // track time between shots
    private float shootCooldown;

    void Start() { 
        shootCooldown = 0f;
    }
    // reduce shootCooldown by a constant amount
    void Update() {
        if (shootCooldown > 0) {
            shootCooldown -= Time.deltaTime;
        }
    }

    // any object who inherits this WeaponScript can call Attack to fire their weapon
    public void Attack(bool isPlayer) {
        if (CanAttack) {
            shootCooldown = shootingRate;

            var shotTransform = Instantiate(shotPrefab);
            shotTransform.position = this.transform.position;

            // initialize our ammo
            ShotScript shot = shotTransform.GetComponent<ShotScript>();
            if (shot != null) {
                shot.fromPlayer = isPlayer;
            }

            // initialize direction of the ammo
            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null) {
                if (isPlayer)
                    move.direction = new Vector2(1, 0);
                else
                    move.direction = new Vector2(-1, 0);
            }
        }
    }

    public bool CanAttack {
        get { return shootCooldown <= 0; }
    }
}
