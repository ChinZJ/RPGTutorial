using UnityEngine;

public class PlayerBow : MonoBehaviour
{
    public Transform launchPoint;
    public GameObject arrowPrefab;
    public PlayerMovement playerMovement;

    private Vector2 aimDirection = Vector2.right;
    public float shootCooldown = .5f;
    private float ShootTimer;

    public Animator anim;

    private void OnEnable()
    {
        anim.SetLayerWeight(0, 0); // Base Layer (Knight).
        anim.SetLayerWeight(1, 1); // Archer Layer.
    }

    private void OnDisable()
    {
        anim.SetLayerWeight(0, 1); // Base Layer (Knight).
        anim.SetLayerWeight(1, 0); // Archer Layer.
    }

    void Update()
    {
        ShootTimer -= Time.deltaTime;

        HandleAiming();

        if (Input.GetButtonDown("Shoot") && ShootTimer <= 0)
        {
            playerMovement.isShooting = true;
            anim.SetBool("isShooting", true);
        }
    }

    private void HandleAiming()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (horizontal != 0 || vertical != 0)
        {
            aimDirection = new Vector2(horizontal, vertical).normalized;

            anim.SetFloat("aimX", aimDirection.x);
            anim.SetFloat("aimY", aimDirection.y);
        }
    }

    public void Shoot()
    {
        // Prevents Update() from enabling multiple calls of Shoot method.
        if (ShootTimer <= 0)
        {
            Arrow arrow = Instantiate(arrowPrefab, launchPoint.position, Quaternion.identity)
                    .GetComponent<Arrow>();
            arrow.direction = aimDirection;

            ShootTimer = shootCooldown;
        }
        playerMovement.isShooting = false;
        anim.SetBool("isShooting", false);
    }
}
