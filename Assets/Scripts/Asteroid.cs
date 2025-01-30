using UnityEngine;
using UnityEngine.UI;

public class Asteroid : HealthBase {
    [SerializeField] private GameObject coinPrefab;
    private Animator explode;

    void Start() {
        explode = GetComponent<Animator>();
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
