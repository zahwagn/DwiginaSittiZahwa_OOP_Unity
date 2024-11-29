using UnityEngine;
public class EnemyForward : Enemy
{
    [SerializeField] private float moveSpeed = 3f;
    private float topSpawnOffset = 2f;
    private float bottomSpawnOffset = -2f;

    protected override void Start()
    {
        base.Start();
        ResetPosition();
    }

    private void Update()
    {
        transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);

        if (transform.position.y < -screenHeight/2 + bottomSpawnOffset)
        {
            ResetPosition();
        }
    }

    private void ResetPosition()
    {
        float randomX = Random.Range(-screenWidth/2, screenWidth/2);
        transform.position = new Vector3(randomX, screenHeight/2 + topSpawnOffset, 0);
    }
}