using UnityEngine;

public class Shot : MonoBehaviour {
    public int damage = 1;
    public bool fromPlayer = true;

    void Start() {
        Destroy(gameObject, 20);
    }

    void OnTriggerEnter(Collider other) {
        Shot otherShot = other.GetComponent<Shot>();
        HomingMissile otherMissile = other.GetComponent<HomingMissile>();

        if (otherShot != null || otherMissile != null) {
            // Destroy both shots
            Destroy(gameObject);
            Destroy(otherShot.gameObject);
        }
    }
}
