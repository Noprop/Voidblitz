using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
    public int enemyCount = 20;
    public int asteroidCount = 5;

    // prefabs
    public GameObject enemy;
    public GameObject asteroid;

    private int enemiesSpawned = 0;
    private int asteroidsSpawned = 0;
    private Transform foreground; // where to place our entities

    void Start() {
        // find the Foreground object by tag
        foreground = GameObject.FindGameObjectWithTag("Foreground")?.transform;

        if (foreground == null) {
            Debug.LogError("Foreground object not found!");
            return;
        }

        // begin spawning of entities
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnEnemies() {
        // spawn an enemy every 0.5 seconds, at a random y position
        while (enemiesSpawned < enemyCount) {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(enemy, spawnPosition, Quaternion.identity, foreground);
            enemiesSpawned++;
            yield return new WaitForSeconds(0.5f);
        }
    }

    IEnumerator SpawnAsteroids() {
        // spawn an asteroid every 2 seconds, at a random y position
        while (asteroidsSpawned < asteroidCount) {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(asteroid, spawnPosition, Quaternion.identity, foreground);
            asteroidsSpawned++;
            yield return new WaitForSeconds(2f);
        }
    }

    Vector3 GetSpawnPosition() {
        // obtain a spawn position to the right of the camera, at a random y
        float randomY = Random.Range(0f, 1f);
        Vector3 viewportPosition = new Vector3(1.1f, randomY, 0);
        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
        worldPosition.z = 0;

        return worldPosition;
    }
}