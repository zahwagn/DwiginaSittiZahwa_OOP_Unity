using UnityEngine;
public class Enemy : MonoBehaviour 
{
    [SerializeField] protected int level = 1;
    protected Camera mainCamera;
    protected float screenWidth;
    protected float screenHeight;

    protected virtual void Start()
    {
        mainCamera = Camera.main;
        // Get screen boundaries in world coordinates
        screenHeight = mainCamera.orthographicSize * 2;
        screenWidth = screenHeight * mainCamera.aspect;
        
        // Make enemy face the player
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 direction = playerPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle + 90, Vector3.forward);
    }
}