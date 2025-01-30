using UnityEngine;

public class Coin : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.gameObject.GetComponent<Player>();
        if (player != null) {
            player.IncreaseScore(1);
            Debug.Log("Player score: " + player.score);
            Destroy(gameObject);
        }
    }
}
