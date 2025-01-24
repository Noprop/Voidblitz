using UnityEngine;

public class MoveScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(15, 15);
    public Vector2 direction = new Vector2(-1, 0);

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;

    // void Start() {}

    void Update() {
        movement = new Vector2(
            speed.x * direction.x,
            speed.y * direction.y
        );
    }

    void FixedUpdate() {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();

        rigidbodyComponent.linearVelocity = movement;
    }
}
