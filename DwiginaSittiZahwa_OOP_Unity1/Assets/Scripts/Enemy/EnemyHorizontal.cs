using UnityEngine;
public class EnemyHorizontal : Enemy
{
    [SerializeField] private float moveSpeed = 5f;
    private bool movingRight;
    private float boundaryOffset = 1f;

    protected override void Start()
    {
        base.Start();
        InitializePosition();
    }

    private void InitializePosition()
    {
        // Get screen bounds in world units
        float screenRightEdge = mainCamera.orthographicSize * mainCamera.aspect;
        float screenLeftEdge = -screenRightEdge;

        // Randomly choose left or right side
        movingRight = Random.value > 0.5f;
        float spawnX;
        
        if (movingRight)
        {
            // Spawn on left side, move right
            spawnX = screenLeftEdge - boundaryOffset;
        }
        else
        {
            // Spawn on right side, move left
            spawnX = screenRightEdge + boundaryOffset;
        }

        // Set random Y position in middle third of screen
        float screenHeight = mainCamera.orthographicSize * 2;
        float spawnY = Random.Range(-screenHeight/3, screenHeight/3);
        
        // Set position
        transform.position = new Vector3(spawnX, spawnY, 0);

        // Debug log to verify spawn
        Debug.Log($"EnemyHorizontal spawned at: {transform.position}, Moving Right: {movingRight}");
    }

    private void Update()
    {
        if (mainCamera == null) return;

        // Move horizontally
        float direction = movingRight ? 1 : -1;
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);

        // Get current screen bounds
        float screenRightEdge = mainCamera.orthographicSize * mainCamera.aspect;
        float screenLeftEdge = -screenRightEdge;

        // Check screen boundaries and reverse direction
        if (movingRight && transform.position.x > screenRightEdge + boundaryOffset)
        {
            movingRight = false;
            Debug.Log("Reversing direction to left");
        }
        else if (!movingRight && transform.position.x < screenLeftEdge - boundaryOffset)
        {
            movingRight = true;
            Debug.Log("Reversing direction to right");
        }
    }
}