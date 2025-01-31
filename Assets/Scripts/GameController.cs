using UnityEngine;
using System.Collections;
using TMPro;

public class GameController : MonoBehaviour {
    [Header("Spawn Settings")]
    public int enemyCount = 15;
    public int asteroidCount = 5;

    [Header("Prefabs")]
    public GameObject enemy;
    public GameObject enemyBoss;
    public GameObject asteroid;

    [Header("UI References")]
    public TextMeshProUGUI levelIndicator;

    private Transform foreground; // where to place our entities
    private Player player;

    // Tracking spawns
    private int enemiesSpawned = 0;
    private int asteroidsSpawned = 0;

    // Tracking deaths
    private int enemiesDestroyed = 0;
    private int asteroidsDestroyed = 0;

    private int level = 1;

    void Start() {
        StartCoroutine(ShowLevelIndicator(1, 2));

        // find the Foreground object by tag
        foreground = GameObject.FindGameObjectWithTag("Foreground")?.transform;
        if (foreground == null) {
            Debug.LogError("Foreground object not found!");
            return;
        }

        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<Player>();
        }

        // begin spawning of entities
        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnAsteroids());
    }

    IEnumerator SpawnEnemies() {
        while (enemiesSpawned < enemyCount && foreground != null) {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(enemy, spawnPosition, Quaternion.identity, foreground);
            enemiesSpawned++;
            yield return new WaitForSeconds(2f);
        }
    }

    IEnumerator SpawnAsteroids() {
        while (asteroidsSpawned < asteroidCount && foreground != null) {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(asteroid, spawnPosition, Quaternion.identity, foreground);
            asteroidsSpawned++;
            yield return new WaitForSeconds(5f);
        }
    }
    private void SpawnBoss() {
        if (foreground != null) {
            Vector3 spawnPosition = GetSpawnPosition();
            Instantiate(enemyBoss, spawnPosition, Quaternion.identity, foreground);
            Debug.Log("spawned a boss");
        }
    }

    Vector3 GetSpawnPosition() {
        float randomY = Random.Range(0.05f, 0.95f);
        Vector3 viewportPosition = new Vector3(1.1f, randomY, 0);
        Vector3 worldPosition = Camera.main.ViewportToWorldPoint(viewportPosition);
        worldPosition.z = 0;
        return worldPosition;
    }

    // will have to refactor to handle more than 2 levels elegantly
    public void EnemyDestroyed() {
        enemiesDestroyed++;
        if (level == 1)
            CheckLevelCompletionLevel1();
        else
            CheckLevelCompletionLevel2();
    }
    public void AsteroidDestroyed() {
        asteroidsDestroyed++;
        if (level == 2)
            CheckLevelCompletionLevel1();
        else
            CheckLevelCompletionLevel2();
    }
    public void BossDestroyed() {
        Debug.Log($"boss destroyed, level {level}");
        if (level == 1) {
            level++;
            StartCoroutine(ShowLevelIndicator(2, 4));
        } else
            player.DestroySelf(player.gameObject);
    }

    private void CheckLevelCompletionLevel1() {
        bool allEnemiesDone = enemiesDestroyed >= enemyCount;
        bool allAsteroidsDone = asteroidsDestroyed >= asteroidCount;

        if (allEnemiesDone && allAsteroidsDone) {
            SpawnBoss();
            enemiesDestroyed = 0;
            asteroidsDestroyed = 0;

        }
    }

    private void CheckLevelCompletionLevel2() {
        bool allEnemiesDone = enemiesDestroyed >= enemyCount;
        bool allAsteroidsDone = asteroidsDestroyed >= asteroidCount;

        if (allEnemiesDone && allAsteroidsDone) {
            SpawnBoss();
            enemiesDestroyed = 0;
            asteroidsDestroyed = 0;
        }
    }

    private IEnumerator ShowLevelIndicator(int level, int time) {
        levelIndicator.text = $"Level {level}";
        levelIndicator.gameObject.SetActive(true);

        yield return new WaitForSeconds(time);

        levelIndicator.gameObject.SetActive(false);
        ResetLevel();
    }

    private void ResetLevel() {
        enemyCount = 3;

        enemiesSpawned = 0;
        asteroidsSpawned = 0;

        enemiesDestroyed = 0;
        asteroidsDestroyed = 0;

        StartCoroutine(SpawnEnemies());
        StartCoroutine(SpawnAsteroids());
    }
}
