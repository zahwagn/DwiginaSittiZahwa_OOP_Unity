using UnityEngine;

public class EnemyTargeting : Enemy
{
    [SerializeField] private float moveSpeed = 4f;
    private Transform playerTransform;

    protected override void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        
        float angle = Random.Range(0f, 360f);
        float spawnRadius = Mathf.Max(screenWidth, screenHeight) + 2f;
        float x = Mathf.Cos(angle * Mathf.Deg2Rad) * spawnRadius;
        float y = Mathf.Sin(angle * Mathf.Deg2Rad) * spawnRadius;
        transform.position = new Vector3(x, y, 0);
    }

    private void Update()
    {
        if (playerTransform != null)
        {
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
            
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}