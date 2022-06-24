using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private bool respawn = true;
    [SerializeField]
    private float respawnDelay = 0f;
    [SerializeField]
    private GameObject enemyPrefab;
    [SerializeField]
    private List<EnemyAI> enemies;

    private List<Vector2> spawnPoints = new();
    private int enemyCount = 0;
    private Vector3 scale;

    public UnityEvent OnAllEnemiesDefeated;


    // Start is called before the first frame update
    void Awake()
    {
        foreach (EnemyAI enemy in  enemies)
        {
            if (enemy)
            {
                spawnPoints.Add(enemy.transform.position);
                enemy.GetComponent<HealthComponent>().OnDeath.AddListener(OnEnemyKilled);
                ++enemyCount;
            }
        }
        if (enemyCount > 0)
        {
            scale = enemies[0].transform.localScale;
        }
    }

    void OnEnemyKilled()
    {
        --enemyCount;
        if (respawn)
        {
            ++enemyCount;
            Invoke("RespawnEnemy", respawnDelay);
        }
        else if (enemyCount == 0)
        {
            OnAllEnemiesDefeated.Invoke();
        }
    }

    void RespawnEnemy()
    {
        Vector2 position = FindSpawnPointOutOfPlayerRange();
        GameObject enemy = Instantiate(enemyPrefab, position, new Quaternion());
        enemy.GetComponent<HealthComponent>().OnDeath.AddListener(OnEnemyKilled);
        enemy.transform.localScale = scale;
    }

    Vector3 FindSpawnPointOutOfPlayerRange()
    {
        Vector3 spawnPoint = new Vector3();
        if (!GameManager.Player)
        {
            return spawnPoint;
        }
        float distance = 0f;
        foreach (Vector3 point in spawnPoints)
        {
            float currDistance = (point - GameManager.Player.transform.position).magnitude;
            if (currDistance > distance)
            {
                spawnPoint = point;
                distance = currDistance;
            }
        }
        return spawnPoint;
    }
}
