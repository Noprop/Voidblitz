using UnityEngine;

public class Shot : MonoBehaviour {
    public int damage = 1;
    public bool fromPlayer = true;

    void Start() {
        Destroy(gameObject, 20);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        Shot otherShot = other.GetComponent<Shot>();
        HomingMissile otherMissile = other.GetComponent<HomingMissile>();

        if (otherShot != null || otherMissile != null) {
            // Destroy both shots
            Destroy(gameObject);
            if (otherShot) Destroy(otherShot.gameObject);
            if (otherMissile) Destroy(otherMissile.gameObject);
        }
    }
}
