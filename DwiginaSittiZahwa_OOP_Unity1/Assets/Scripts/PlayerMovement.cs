using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    [SerializeField] Vector2 maxSpeed = new Vector2(7, 5);
    [SerializeField] Vector2 timeToFullSpeed = new Vector2(1, 1);
    [SerializeField] Vector2 timeToStop = new Vector2(0.5f, 0.5f);
    [SerializeField] Vector2 stopClamp = new Vector2(2.5f, 2.5f);

    private Vector2 moveDirection;
    private Vector2 moveVelocity;
    private Vector2 moveFriction;
    private Vector2 stopFriction;
    private Rigidbody2D rb;
    private Camera mainCamera; // Referensi ke main camera

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main; // Mendapatkan referensi main camera

        moveVelocity = new Vector2(
            2 * maxSpeed.x / timeToFullSpeed.x,
            2 * maxSpeed.y / timeToFullSpeed.y
        );

        moveFriction = new Vector2(
            -2 * maxSpeed.x / (timeToFullSpeed.x * timeToFullSpeed.x),
            -2 * maxSpeed.y / (timeToFullSpeed.y * timeToFullSpeed.y)
        );

        stopFriction = new Vector2(
            -2 * maxSpeed.x / (timeToStop.x * timeToStop.x),
            -2 * maxSpeed.y / (timeToStop.y * timeToStop.y)
        );
    }

    public void Move()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        Vector2 currentVelocity = rb.velocity;
        Vector2 newVelocity = currentVelocity;

        if (horizontalInput != 0)
        {
            newVelocity.x += moveDirection.x * moveVelocity.x * Time.fixedDeltaTime;
        }
        else
        {
            Vector2 friction = GetFriction();
            if (Mathf.Abs(currentVelocity.x) > stopClamp.x)
            {
                newVelocity.x += friction.x * Time.fixedDeltaTime;
            }
            else
            {
                newVelocity.x = 0;
            }
        }

        if (verticalInput != 0)
        {
            newVelocity.y += moveDirection.y * moveVelocity.y * Time.fixedDeltaTime;
        }
        else
        {
            Vector2 friction = GetFriction();
            if (Mathf.Abs(currentVelocity.y) > stopClamp.y)
            {
                newVelocity.y += friction.y * Time.fixedDeltaTime;
            }
            else
            {
                newVelocity.y = 0;
            }
        }

        newVelocity.x = Mathf.Clamp(newVelocity.x, -maxSpeed.x, maxSpeed.x);
        newVelocity.y = Mathf.Clamp(newVelocity.y, -maxSpeed.y, maxSpeed.y);

        rb.velocity = newVelocity;

        MoveBound();
    }

    private Vector2 GetFriction()
    {
        return moveDirection != Vector2.zero ? moveFriction : stopFriction;
    }

    private void MoveBound()
    {
        if (mainCamera != null)
        {
            Vector2 screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
            
            float objectWidth = transform.localScale.x / 2;
            float objectHeight = transform.localScale.y / 2;

            Vector3 currentPosition = transform.position;

            float newX = Mathf.Clamp(currentPosition.x, -screenBounds.x + objectWidth, screenBounds.x - objectWidth);
            
            float newY = Mathf.Clamp(currentPosition.y, -screenBounds.y + objectHeight, screenBounds.y - objectHeight);

            transform.position = new Vector3(newX, newY, currentPosition.z);
        }
    }

    public bool IsMoving()
    {
        return Mathf.Abs(rb.velocity.x) > stopClamp.x || Mathf.Abs(rb.velocity.y) > stopClamp.y;
    }
}