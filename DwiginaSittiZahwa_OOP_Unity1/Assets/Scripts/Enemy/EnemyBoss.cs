using UnityEngine;

public class EnemyBoss : Enemy
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private Weapon weaponPrefab;
    private Weapon currentWeapon;
    private Vector3 targetPosition;
    private float moveTimer = 0f;
    private float moveDuration = 3f;

    protected override void Start()
    {
        base.Start();
        
        // Spawn at top center of screen
        transform.position = new Vector3(0, screenHeight / 2 - 2f, 0);
        
        // Initialize weapon
        if (weaponPrefab != null)
        {
            currentWeapon = Instantiate(weaponPrefab, transform);
            currentWeapon.parentTransform = transform;
        }
        
        SetNewTargetPosition();
    }

    private void Update()
    {
        moveTimer += Time.deltaTime;
        if (moveTimer >= moveDuration)
        {
            SetNewTargetPosition();
            moveTimer = 0f;
        }

        transform.position = Vector3.Lerp(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {
            Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
            Vector3 direction = playerPosition - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }

        Debug.Log("EnemyBoss Position: " + transform.position);
    }

    private void SetNewTargetPosition()
    {
        float randomX = Random.Range(-screenWidth / 3, screenWidth / 3);
        float randomY = Random.Range(screenHeight / 4, screenHeight / 2 - 2f);
        targetPosition = new Vector3(randomX, randomY, 0);
    }
}