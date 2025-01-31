using UnityEngine;

public class HomingMissile : MonoBehaviour {
    [Header("Missile Settings")]
    public float speed = 5f;
    public float rotateSpeed = 200f;
    public int damage = 3;

    [Header("Ownership")]
    public bool fromPlayer = true;

    private Transform target;

    void Start() {
        // If from the player, we look for enemies
        // If from an enemy, we look for the player
        string targetTag = fromPlayer ? "Enemy" : "Player";
        GameObject nearest = FindNearestTarget(targetTag);
        if (nearest != null) {
            target = nearest.transform;
        }
    }

    void Update() {
        if (target == null) return;

        // Rotate towards the target
        Vector2 direction = (Vector2)target.position - (Vector2)transform.position;
        direction.Normalize();

        // cross.z gives how far we need to rotate 
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        transform.Rotate(0, 0, -rotateAmount * rotateSpeed * Time.deltaTime);

        // Move forward
        transform.Translate(Vector3.up * speed * Time.deltaTime, Space.Self);
    }

    private GameObject FindNearestTarget(string tag) {
        GameObject[] candidates = GameObject.FindGameObjectsWithTag(tag);
        GameObject nearest = null;
        float minDistSqr = Mathf.Infinity;
        Vector3 currentPos = transform.position;

        foreach (var c in candidates) {
            float distSqr = (c.transform.position - currentPos).sqrMagnitude;
            if (distSqr < minDistSqr) {
                nearest = c;
                minDistSqr = distSqr;
            }
        }

        return nearest;
    }
}
