using UnityEngine;

public class PlayerScript : MonoBehaviour {
    public Vector2 speed = new Vector2(15, 15);
    public GameObject gameMenuPanel;
    public GameObject gameOverPanel;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private HealthScript health;
    private GameMenu gameMenu;
    private GameMenu gameOver;

    void Start() {
        if (health == null) health = GetComponent<HealthScript>();

        gameMenu = gameMenuPanel.GetComponent<GameMenu>();
        gameOver = gameOverPanel.GetComponent<GameMenu>();
    }

    // shoot via the space button
    void Update() {
        bool shoot = Input.GetKeyDown(KeyCode.Space);
        bool pause = Input.GetKeyDown(KeyCode.Escape);
        if (pause) gameMenu.HandlePause();

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

    // damage enemies and self on collision and ensure we don't go spinning
    private void OnCollisionEnter2D(Collision2D other) {
        EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();
        if (enemy != null) {
            Destroy(other.gameObject);
            health.hp -= 1;
        }
        transform.rotation = Quaternion.Euler(0, 0, -90);
    }

    // display buttons on death
    private void OnDestroy() {
        if (gameOver != null) gameOver.ShowButtons();
        Time.timeScale = 0f;
    }
}
