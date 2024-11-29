using UnityEngine;
using UnityEngine.UI;

public class GameUIManager : MonoBehaviour
{
  [Header("UI Text Component")]
  public Text infoText;

  [Header("Game Components")]
  public HealthComponent playerHealth;

  [Header("Wave and Enemy Tracking")]
  public int currentWave = 1;
  public int enemyCountInWave = 0;

  private int playerPoints = 0;

  void Start()
  {
    // Find player health component if not assigned in inspector
    if (playerHealth == null)
    {
      playerHealth = FindObjectOfType<Player>().GetComponent<HealthComponent>();
    }

    UpdateInfoDisplay();
  }

  void Update()
  {
    // Continuously update health display in case of changes
    UpdateInfoDisplay();
  }

  public void AddPointsFromEnemy(int enemyLevel)
  {
    playerPoints += enemyLevel;
    UpdateInfoDisplay();
  }

  public void UpdateWaveInfo(int wave, int enemyCount)
  {
    currentWave = wave;
    enemyCountInWave = enemyCount;
    UpdateInfoDisplay();
  }

  private void UpdateInfoDisplay()
  {
    if (playerHealth != null)
    {
      infoText.text = $"<b>Health:</b> {Mathf.RoundToInt(playerHealth.Health)}/{Mathf.RoundToInt(playerHealth.MaxHealth)} | " +
                       $"<b>Points:</b> {playerPoints} | " +
                       $"<b>Wave:</b> {currentWave} | " +
                       $"<b>Enemies Left:</b> {enemyCountInWave}";
    }
  }
}