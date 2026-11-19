using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Settings")]
    [SerializeField] EnemyData[] enemies;

    [Header("Spawn Settings")]
    [SerializeField] Collider2D safeArea;
    [SerializeField] Transform target;
    [SerializeField] private float spawnIntervalSeconds = 5f;
    [SerializeField] private float spawnDistance = 1f;

    private List<EnemyController> spawnedEnemies;
    private float nextSpawnTime;

    void Awake()
    {
        spawnedEnemies = new List<EnemyController>();
        nextSpawnTime = Time.time + spawnIntervalSeconds;
    }

    void Update()
    {
        if (target != null && !GameManager.Instance.IsGameOver)
        {
            if (Time.time >= nextSpawnTime)
            {
                SpawnEnemy();
                nextSpawnTime = Time.time + spawnIntervalSeconds;
            }

            foreach (EnemyController enemy in spawnedEnemies)
                if (enemy != null)
                    enemy.targetPosition = target.position;
        }
    }

    private void SpawnEnemy()
    {
        if (enemies.Length == 0)
            return;

        int randomIndex = Random.Range(0, enemies.Length);
        EnemyData enemyData = enemies[randomIndex];

        Vector2 spawnPosition = GetRandomSpawnPosition();
        GameObject enemyObject = Instantiate(enemyData.spritePrefab, spawnPosition, Quaternion.identity);
        EnemyController enemyController = enemyObject.GetComponent<EnemyController>();

        enemyController.targetPosition = target.position;
        enemyController.OnDeath += OnEnemyDeath;

        spawnedEnemies.Add(enemyController);
    }

    private Vector2 GetRandomSpawnPosition()
    {
        Bounds bounds = safeArea.bounds;

        int side = Random.Range(0, 4);

        float x = 0f;
        float y = 0f;

        switch (side)
        {
            case 0: // Top
                x = Random.Range(bounds.min.x, bounds.max.x);
                y = bounds.max.y + spawnDistance;
                break;

            case 1: // Bottom
                x = Random.Range(bounds.min.x, bounds.max.x);
                y = bounds.min.y - spawnDistance;
                break;

            case 2: // Left
                x = bounds.min.x - spawnDistance;
                y = Random.Range(bounds.min.y, bounds.max.y);
                break;

            case 3: // Right
                x = bounds.max.x + spawnDistance;
                y = Random.Range(bounds.min.y, bounds.max.y);
                break;
        }

        return new Vector2(x, y);
    }

    private void OnEnemyDeath(EnemyController enemyController)
    {
        spawnedEnemies.Remove(enemyController);
        GameManager.Instance.AddScore(1);

        enemyController.OnDeath -= OnEnemyDeath;
    }
}
