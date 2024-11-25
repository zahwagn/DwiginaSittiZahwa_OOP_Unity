using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public EnemySpawner[] enemySpawners;
    public float timer = 0;
    [SerializeField] private float waveInterval = 5f;
    public int waveNumber = 1;
    public int totalEnemies = 0;
    
    private void Start()
    {
        StartNewWave();
    }
    
    private void Update()
    {
        if (totalEnemies <= 0)
        {
            timer += Time.deltaTime;
            
            if (timer >= waveInterval)
            {
                timer = 0;
                StartNewWave();
            }
        }
    }
    
    private void StartNewWave()
    {
        waveNumber++;
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.StartSpawning();
        }
    }
    
    public void OnEnemyDeath()
    {
        totalEnemies--;
        foreach (EnemySpawner spawner in enemySpawners)
        {
            spawner.OnEnemyKilled();
        }
    }
}
