using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f; // Time between shots

    [Header("Bullets")]
    public Bullet bullet; // Public field for Bullet prefab
    [SerializeField] private Transform bulletSpawnPoint; // Private field for spawn point

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool; // Object pool for bullets
    private readonly bool collectionCheck = false; // No collection check
    private readonly int defaultCapacity = 30; // Default capacity of the pool
    private readonly int maxSize = 100; // Maximum size of the pool
    private float timer; // Timer for shooting
    public Transform parentTransform; // Parent transform for bullets

    private void Awake()
    {
        objectPool = new ObjectPool<Bullet>(
            CreateBullet,
            OnTakeBulletFromPool,
            OnReturnBulletToPool,
            OnDestroyBullet,
            collectionCheck,
            defaultCapacity,
            maxSize);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds)
        {
            TryShoot(); // Attempt to shoot if interval has passed
            timer = 0f; // Reset timer
        }
    }

    private Bullet CreateBullet()
{
    if (bullet == null)
    {
        Debug.LogError("Bullet prefab is not assigned!"); // Log error if null
        return null;
    }
    Bullet bulletInstance = Instantiate(bullet);
    bulletInstance.SetPool(objectPool);
    return bulletInstance;
}

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position; // Set position to spawn point
        bullet.transform.rotation = bulletSpawnPoint.rotation; // Set rotation to spawn point
        bullet.gameObject.SetActive(true); // Activate the bullet
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); // Deactivate the bullet when returning to pool
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject); // Destroy bullet if no longer needed
    }

    public void TryShoot()
    {
        Bullet bulletInstance = objectPool.Get(); // Get a bullet from the pool
        if (bulletInstance != null)
        {
            bulletInstance.SetSpeed(20f); // Example speed for the bullet
        }
        else
        {
            Debug.LogError("Failed to get a bullet from the pool."); // Log error if null
        }
    }
}