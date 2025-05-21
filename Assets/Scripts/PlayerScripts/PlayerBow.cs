using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;

    private Vector2 aimDirection = Vector2.right;
    public float shootCooldown = .5f;
    private float ShootTimer;

    void Update()
    {
        ShootTimer -= Time.deltaTime;

        HandleAiming();

        if (Input.GetButtonDown("Shoot") && ShootTimer <= 0)
        {
            Shoot();
        }
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;
        }
    }

    public void Shoot()
    {
        Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity)
                .GetComponent<Arrow>();
        arrow.direction = aimDirection;

        ShootTimer = shootCooldown;
    }
}
