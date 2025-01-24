using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(15, 15);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private HealthScript health;


    void Start() {
        if (health == null) health = GetComponent<HealthScript>();
    }

    void Update() {
        bool shoot = Input.GetKeyDown(KeyCode.Space);
        transform.rotation = Quaternion.Euler(0, 0, -90);

        if (shoot) {
            WeaponScript weapon = GetComponentInChildren<WeaponScript>();
            if (weapon != null) {
                weapon.Attack(true);
            }
        }

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(
            speed.x * inputX,
            speed.y * inputY
        );
    }

    void FixedUpdate() {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.linearVelocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();
        if (enemy != null) {
            Destroy(other.gameObject);
            health.hp -= 1;
        }
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }
}
