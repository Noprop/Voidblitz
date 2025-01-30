using UnityEngine;

public class Asteroid : HealthBase {
    [SerializeField] private GameObject coinPrefab;
    private Animator explode;
    private SpriteRenderer rendererComponent;
    private bool hasSpawned = false;

    void Start() {
        explode = GetComponent<Animator>();
        rendererComponent = GetComponent<SpriteRenderer>();
    }

    void Update() {
        if (!hasSpawned && rendererComponent.isVisible) hasSpawned = true;
        if (hasSpawned && !rendererComponent.isVisible) Destroy(gameObject);
    }

    public override void DestroySelf(GameObject self) {
        MoveScript moveScript = GetComponent<MoveScript>();
        if (moveScript != null) {
            moveScript.speed = new Vector2(0, 0);
        }
        explode.Play("Asteroid_Explode");

        // spawn coin at Asteroid's position
        if (coinPrefab != null) {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }

        Destroy(self, explode.GetCurrentAnimatorStateInfo(0).length);
    }
}
