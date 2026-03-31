using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private int maxEnemies = 10;
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private float spawnRangeX = 2f;
    [SerializeField] private float spawnRangeY = 1f;

    private float nextSpawnTime = 0f;
    private int currentEnemyCount = 0;

    void Start()
    {
        if (enemyPrefab == null)
        {
            Debug.LogWarning("⚠️ Enemy prefab no asignado");
        }

        if (spawnPoints.Length == 0)
        {
            Debug.LogWarning("⚠️ No hay puntos de spawn asignados");
        }

        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (Time.time >= nextSpawnTime && currentEnemyCount < maxEnemies)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnInterval;
        }
    }

    /// <summary>
    /// Spawnea un enemigo
    /// </summary>
    void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0)
            return;

        Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Vector3 spawnPosition = randomSpawnPoint.position + new Vector3(
            Random.Range(-spawnRangeX, spawnRangeX),
            Random.Range(-spawnRangeY, spawnRangeY),
            0
        );

        GameObject newEnemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        currentEnemyCount++;

        Debug.Log($"👹 Enemigo spawneado. Total: {currentEnemyCount}/{maxEnemies}");

        Destroy(newEnemy, 60f);
    }

    /// <summary>
    /// Decrementa contador cuando un enemigo muere
    /// </summary>
    public void OnEnemyDeath()
    {
        currentEnemyCount = Mathf.Max(0, currentEnemyCount - 1);
        Debug.Log($"💀 Enemigo eliminado. Total: {currentEnemyCount}/{maxEnemies}");
    }

    public int GetCurrentEnemyCount() => currentEnemyCount;
    public int GetMaxEnemies() => maxEnemies;
}