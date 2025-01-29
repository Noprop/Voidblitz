using UnityEngine;

public class Shot : MonoBehaviour
{
    public int damage = 1;
    public bool fromPlayer = true;

    void Start() {
        Destroy(gameObject, 20);
    }
    // void Update() {}
}
