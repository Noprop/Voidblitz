using UnityEngine;

public class WeaponScript : MonoBehaviour
{
    public Transform shotPrefab;
    public float shootingRate = 0.1f;

    private float shootCooldown;

    void Start() { 
        shootCooldown = 0f;
    }
    void Update() {
        if (shootCooldown > 0) {
            shootCooldown -= Time.deltaTime;
        }
    }

    public void Attack(bool isEnemy) {
        if (CanAttack) {
            shootCooldown = shootingRate;

            var shotTransform = Instantiate(shotPrefab);
            shotTransform.position = this.transform.position;
            ShotScript shot = shotTransform.GetComponent<ShotScript>();
            if (shot != null) {
                shot.forEnemy = !isEnemy;
            }

            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null) {
                move.direction = new Vector2(1, 0);
            }
        }
    }

    public bool CanAttack {
        get { return shootCooldown <= 0; }
    }
}
