using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(15, 15);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    // void Start() {}

    void Update() {
        bool shoot = Input.GetKeyDown(KeyCode.Space);

        if (shoot) {
            WeaponScript weapon = GetComponent<WeaponScript>();
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
}
