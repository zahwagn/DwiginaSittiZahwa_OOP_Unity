using UnityEngine;
using UnityEngine.Pool;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f; 

    [Header("Bullets")]
    public Bullet bullet; 
    [SerializeField] private Transform bulletSpawnPoint; 
    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool; 
    private readonly bool collectionCheck = false; 
    private readonly int defaultCapacity = 30; 
    private readonly int maxSize = 100; 
    private float timer; 
    public Transform parentTransform; 

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
            TryShoot(); 
            timer = 0f; 
        }
    }

    private Bullet CreateBullet()
{
    if (bullet == null)
    {
        Debug.LogError("Bullet prefab is not assigned!"); 
        return null;
    }
    Bullet bulletInstance = Instantiate(bullet);
    bulletInstance.SetPool(objectPool);
    return bulletInstance;
}

    private void OnTakeBulletFromPool(Bullet bullet)
    {
        bullet.transform.position = bulletSpawnPoint.position; 
        bullet.transform.rotation = bulletSpawnPoint.rotation; 
        bullet.gameObject.SetActive(true); 
    }

    private void OnReturnBulletToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); 
    }

    private void OnDestroyBullet(Bullet bullet)
    {
        Destroy(bullet.gameObject); 
    }

    public void TryShoot()
    {
        Bullet bulletInstance = objectPool.Get(); 
        if (bulletInstance != null)
        {
            bulletInstance.SetSpeed(20f); 
        }
        else
        {
            Debug.LogError("Failed to get a bullet from the pool."); 
        }
    }
}