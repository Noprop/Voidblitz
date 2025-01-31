using UnityEngine;
using TMPro;

public class Player : HealthBase {
    public Vector2 speed = new Vector2(15, 15);
    public GameObject gameMenuPanel;
    public GameObject gameOverPanel;
    public GameObject scoreboardObj;
    public TextMeshProUGUI gameOverScore;
    public TextMeshProUGUI gameOverHighScore;
    public int score = 0;
    public GameObject shipWeaponObj;

    private Vector2 movement;
    private Rigidbody2D rigidbodyComponent;
    private GameMenu gameMenu;
    private GameMenu gameOver;
    private TextMeshProUGUI scoreboard;
    private static int highscore = 0;
    private Weapon shipWeapon;

    [Header("Ship Graphics")]
    [SerializeField] private Sprite[] shipSprites;

    void Start() {
        gameMenu = gameMenuPanel.GetComponent<GameMenu>();
        gameOver = gameOverPanel.GetComponent<GameMenu>();
        scoreboard = scoreboardObj.GetComponentInChildren<TextMeshProUGUI>();
        scoreboardObj.SetActive(true);
        shipWeapon = shipWeaponObj.GetComponent<Weapon>();


        if (GameManager.Instance != null) {
            int idx = GameManager.Instance.selectedShipIndex;
            GetComponent<SpriteRenderer>().sprite = shipSprites[idx];
            if (idx == 0) {
                hp = 10;
                maxHp = 10;
                shipWeapon.weapons[0].shootingRate = 0.3f;
            } else {
                hp = 20;
                maxHp = 20;
                shipWeapon.weapons[0].shootingRate = 0.5f;
            }
        }
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
                weapon.Attack(0, true);
            }
        }

        // Check inputs, both WASD and arrow keys
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        movement = new Vector2(speed.x * horizontalInput, speed.y * verticalInput);

        var dist = (transform.position - Camera.main.transform.position).z;
        var leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        var rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        var topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        var bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, leftBorder, rightBorder),
            Mathf.Clamp(transform.position.y, topBorder, bottomBorder),
            transform.position.z
        );
    }

    void FixedUpdate() {
        if (rigidbodyComponent == null) rigidbodyComponent = GetComponent<Rigidbody2D>();
        rigidbodyComponent.linearVelocity = movement;
    }

    public void AdjustScore(int amount) {
        score = score + amount;
        scoreboard.text = $"Score: {score}";
    }

    private void OnCollisionEnter2D(Collision2D other) {
        EnemyScript enemy = other.gameObject.GetComponent<EnemyScript>();
        if (enemy != null) {
            if (enemy.hp > 5) {
                DestroySelf(gameObject);
            } else {
                Destroy(other.gameObject);
                hp -= 1;
            }
        }
    }

    public override void DestroySelf(GameObject self) {
        if (score > highscore) highscore = score;
        scoreboardObj.SetActive(false);
        Time.timeScale = 0f;

        gameOverScore.text = $"Score: {score}";
        gameOverHighScore.text = $"Highscore: {highscore}";

        if (gameOver != null) gameOver.ShowButtons();
        Destroy(self);
    }
}
