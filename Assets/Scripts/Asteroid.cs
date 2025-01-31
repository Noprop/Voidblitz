using UnityEngine;

public class Asteroid : HealthBase {
    [SerializeField] private GameObject coinPrefab;

    private GameController gameController;
    private Animator explode;
    private SpriteRenderer rendererComponent;
    private bool hasSpawned = false;

    void Start() {
        explode = GetComponent<Animator>();
        rendererComponent = GetComponent<SpriteRenderer>();
        gameController = GameObject.FindGameObjectWithTag("GameController")?.GetComponent<GameController>();
    }

    void Update() {
        if (!hasSpawned && rendererComponent.isVisible) hasSpawned = true;
        if (hasSpawned && !rendererComponent.isVisible) Destroy(gameObject);
    }

    public override void DestroySelf(GameObject self) {
        Move move = GetComponent<Move>();
        if (move != null) {
            move.speed = new Vector2(0, 0);
        }
        explode.Play("Asteroid_Explode");

        // spawn coin at Asteroid's position
        if (coinPrefab != null) {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        Destroy(self, explode.GetCurrentAnimatorStateInfo(0).length);
    }

    void OnDestroy() {
        gameController.AsteroidDestroyed();
    }
}
