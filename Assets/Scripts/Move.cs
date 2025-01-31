using UnityEngine;

public class Move : MonoBehaviour {
    // speed is the magnitude, direction is a unit vector
    public Vector2 speed = new Vector2(15, 15);
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    void Start() {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();
    }

    void Update() {
        movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y
        );
    }

    void FixedUpdate() {
        rigidbodyComponent.linearVelocity = movement;
    }
}
