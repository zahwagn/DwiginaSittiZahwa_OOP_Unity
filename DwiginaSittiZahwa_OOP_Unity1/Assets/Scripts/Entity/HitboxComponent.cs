using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class HitboxComponent : MonoBehaviour
{
    [SerializeField] private HealthComponent health; 
    private void Awake()
    {
        if (health == null)
        {
            health = GetComponent<HealthComponent>();
        }

        if (health == null)
        {
            Debug.LogError("HitboxComponent requires a HealthComponent! Please assign it in the inspector or add it to the same GameObject.", this);
        }
    }

    public void Damage(int damageAmount)
    {
        if (health != null)
        {
            health.SubtractHealth(damageAmount); // Call SubtractHealth on HealthComponent
            Debug.Log($"{gameObject.name} took {damageAmount} damage");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bullet bullet = other.GetComponent<Bullet>();
        
        if (bullet != null)
        {
            Damage(bullet.damage); 
        }
    }
}