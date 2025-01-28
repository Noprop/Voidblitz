using UnityEngine;

public class ShotScript : MonoBehaviour
{
    public int damage = 1;
    public bool fromPlayer = true;

    void Start() {
        Destroy(gameObject, 20);
    }
    // void Update() {}
}
