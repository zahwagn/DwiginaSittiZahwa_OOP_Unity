using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy Prefabs")]
    public Enemy spawnedEnemy;                                    
    [SerializeField] private int minimumKillsToIncreaseSpawnCount = 6;  
    public int totalKill = 0;                                     
    private int totalKillWave = 0;                               // Total kill per wave
    [SerializeField] private float spawnInterval = 3f;           

    [Header("Spawned Enemies Counter")]
    public int spawnCount = 2;                                   // Jumlah enemy yang di-spawn per interval
    public int defaultSpawnCount = 1;                            // Jumlah spawn default
    public int spawnCountMultiplier = 1;                         
    public int multiplierIncreaseCount = 1;                      // Jumlah penambahan spawn count

    public CombatManager combatManager;                          // Referensi ke Combat Manager
    public bool isSpawning = false;                             // Status spawning aktif/tidak

    private void Start()
    {
        // Inisialisasi spawn count dengan nilai default
        spawnCount = defaultSpawnCount;
    }

    public void StartSpawning()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnEnemyRoutine());
        }
    }

    private IEnumerator SpawnEnemyRoutine()
    {
        while (isSpawning)
        {
            // Spawn enemy sejumlah spawnCount
            for (int i = 0; i < spawnCount; i++)
            {
                SpawnSingleEnemy();
                yield return new WaitForSeconds(0.5f); // Delay kecil antar spawn individual
            }
            
            // Tunggu sesuai spawn interval sebelum wave spawn berikutnya
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnSingleEnemy()
    {
        if (spawnedEnemy != null)
        {
            // Generate posisi random di sekitar spawner
            Vector3 randomPosition = transform.position + Random.insideUnitSphere * 5f;
            randomPosition.y = transform.position.y; // Tetap di ketinggian yang sama
            
            // Instantiate enemy dan tambah total enemy di combat manager
            Enemy enemy = Instantiate(spawnedEnemy, randomPosition, Quaternion.identity);
            combatManager.totalEnemies++;
        }
    }

    public void OnEnemyKilled()
    {
        totalKill++;
        totalKillWave++;

        // Cek apakah sudah mencapai minimum kills untuk menaikkan spawn count
        if (totalKillWave >= minimumKillsToIncreaseSpawnCount)
        {
            totalKillWave = 0;
            // Tambah spawn count sesuai multiplier
            spawnCount += multiplierIncreaseCount * spawnCountMultiplier;
        }
    }

    public void StopSpawning()
    {
        isSpawning = false;
    }

    // Method untuk reset spawner saat wave baru
    public void ResetForNewWave()
    {
        totalKillWave = 0;
        spawnCount = defaultSpawnCount * (spawnCountMultiplier + combatManager.waveNumber - 1);
    }
}