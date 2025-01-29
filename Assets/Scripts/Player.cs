using UnityEngine;

public class Player : HealthBase {
    public Vector2 speed = new Vector2(15, 15);
    public GameObject gameMenuPanel;
    public GameObject gameOverPanel;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private GameMenu gameMenu;
    private GameMenu gameOver;

    void Start() {
        gameMenu = gameMenuPanel.GetComponent<GameMenu>();
        gameOver = gameOverPanel.GetComponent<GameMenu>();
    }

    void Update() {
        bool pause = Input.GetKeyDown(KeyCode.Escape);
        if (pause) gameMenu.HandlePause();

        // Obtain the direction from the ship to the player's cursor
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = new Vector2(
            mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y
        );

        // Point ship towards mouse cursor
        // note: ship sprite is facing upwards by default
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        // Fire weapon
        bool shoot = Input.GetKey(KeyCode.Space);
        bool shoot2 = Input.GetMouseButton(0);
        if (shoot || shoot2) {
            Weapon weapon = GetComponentInChildren<Weapon>();
            if (weapon != null) {
                weapon.Attack(true);
            }
        }

        // Only check for ad or left-right arrows
        float verticalInput = Input.GetAxis("Horizontal");
        movement = new Vector2(0, speed.y * verticalInput * -1);
    }

    void FixedUpdate() {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.linearVelocity = movement;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();
        if (enemy != null) {
            Destroy(other.gameObject);
            hp -= 1;
        }
    }

    public override void DestroySelf(GameObject self) {
        if (gameOver != null) gameOver.ShowButtons();
        Time.timeScale = 0f;
        Destroy(self);
    }
}
