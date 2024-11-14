using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class AttackComponent : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private int damage = 10;

    private Transform bulletSpawnPoint;

    private void Awake()
    {
        bulletSpawnPoint = transform.Find("BulletSpawnPoint");
        if (bulletSpawnPoint == null)
        {
            GameObject spawnPoint = new GameObject("BulletSpawnPoint");
            bulletSpawnPoint = spawnPoint.transform;
            bulletSpawnPoint.SetParent(transform);
            bulletSpawnPoint.localPosition = Vector3.zero;
        }
    }

    public void Shoot()
    {
        if (bulletPrefab != null)
        {
            Bullet bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.damage = damage;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        HitboxComponent hitbox = other.GetComponent<HitboxComponent>();
        
        if (hitbox != null)
        {
            hitbox.Damage(damage);
            Debug.Log($"{gameObject.name} dealt {damage} damage to {other.gameObject.name}");

            InvincibilityComponent invincibility = other.GetComponent<InvincibilityComponent>();
            if (invincibility != null)
            {
                invincibility.TriggerInvincibility(); 
                Debug.Log($"{other.gameObject.name} is now invincible");
            }
        }
    }
}