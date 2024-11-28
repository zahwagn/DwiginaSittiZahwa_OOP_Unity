using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
    [Header("UI Text Components")]
    public Text healthText;
    public Text pointsText;
    public Text waveText;
    public Text enemyCountText;

    [Header("Game Components")]
    public HealthComponent playerHealth;
    public LevelManager levelManager;

    [Header("Wave and Enemy Tracking")]
    public int currentWave = 1;
    public int enemyCountInWave = 0;

    private int playerPoints = 0;

    void Start()
    {
        // Find player health component if not assigned in inspector
        if (playerHealth == null)
            playerHealth = FindObjectOfType<Player>().GetComponent<HealthComponent>();

        UpdateHealthDisplay();
        UpdatePointsDisplay();
        UpdateWaveDisplay();
    }

    void Update()
    {
        // Continuously update health display in case of changes
        UpdateHealthDisplay();
    }

    public void UpdateHealthDisplay()
    {
        if (playerHealth != null)
        {
            healthText.text = $"Health: {Mathf.RoundToInt(playerHealth.Health)}/{Mathf.RoundToInt(playerHealth.MaxHealth)}";
        }
    }

    public void AddPointsFromEnemy(Enemy enemy)
    {
        // Use reflection to get level from protected field
        int enemyLevel = (int)enemy.GetType()
            .GetField("level", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .GetValue(enemy);
        
        playerPoints += enemyLevel;
        UpdatePointsDisplay();
    }

    public void UpdatePointsDisplay()
    {
        pointsText.text = $"Points: {playerPoints}";
    }

    public void UpdateWaveDisplay()
    {
        waveText.text = $"Wave: {currentWave}";
        enemyCountText.text = $"Enemies: {enemyCountInWave}";
    }

    // Method to update wave and enemy count
    public void UpdateWaveInfo(int wave, int enemyCount)
    {
        currentWave = wave;
        enemyCountInWave = enemyCount;
        UpdateWaveDisplay();
    }
}