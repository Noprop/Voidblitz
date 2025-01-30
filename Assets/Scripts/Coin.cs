using UnityEngine;

public class Coin : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null) {
            player.IncreaseScore(5);
            Destroy(gameObject);
        }
    }
}
