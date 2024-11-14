using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private float maxHealth = 700f; 
    private float currentHealth; 

    public float Health => currentHealth;

    public float MaxHealth => maxHealth;

    private void Awake()
    {
        currentHealth = maxHealth; 
    }

    public void SubtractHealth(float amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount); 
        
        if (currentHealth <= 0)
        {
            HandleDeath(); 
        }
    }

    private void HandleDeath()
    {
        Debug.Log($"{gameObject.name} has died.");
        gameObject.SetActive(false); 
        Destroy(gameObject); 
    }
}