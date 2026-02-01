using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerControllerScript : MonoBehaviour
{
    // Movement parameters
    public float moveSpeed = 5f;
    public float jumpForce = 5.0f;

    // Ground check parameters
    public Transform groundCheck;
    public float groundCheckWidth = 0.4f;
    public float groundCheckHeight = 0.4f;
    public LayerMask groundMask;
    bool isGrounded;

    //other physics stuff
    private Rigidbody2D rb;
    private Vector2 movement;
    private bool jumpTriggered = false;

    //sprite stuff
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public float afkDelay = 3f;
    private float idleTimer = 0f;
    private const string AnimIsMoving = "isMoving";
    private const string AnimIsAfk = "isAfk";

    //camera reference for culling masks
    public new CameraController camera;

    public bool hasRedMask = false;
    public bool hasGreenMask = false;

    public GameObject RedtoGreenPrefab;
    public GameObject GreentoRedPrefab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        camera = Camera.main.GetComponent<CameraController>();

        //set true projectiles collision off at start
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("TrueProjectile"), LayerMask.NameToLayer("GreenGround"), false);
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("TrueProjectile"), LayerMask.NameToLayer("RedGround"), false);
    }

    private void Update()
    {
        bool isMovingNow = movement.sqrMagnitude > 0.001f;

        if (isMovingNow)
        {
            idleTimer = 0f;
        }
        else
        {
            idleTimer += Time.deltaTime;
        }

        if (animator != null)
        {
            animator.SetBool(AnimIsMoving, isMovingNow);
            animator.SetBool(AnimIsAfk, !isMovingNow && idleTimer >= afkDelay);
        }
    }

    private void FixedUpdate()
    {
        float moveX = movement.x * moveSpeed;
        rb.linearVelocity = new Vector2(moveX, rb.linearVelocity.y);

        if (movement.x != 0f)
        {
            spriteRenderer.flipX = movement.x < 0f;
        }

        //need this for ground check. Using overlapbox for more reliable ground detection
        isGrounded = Physics2D.OverlapBox(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight), 0f, groundMask);
        //isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundDistance, groundMask);


        //add is grounded to only jump on ground duh
        if (jumpTriggered && isGrounded)
        {
            //reset my vertical velocity for consistent jumps
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }   

    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //drawing ground check box
        Gizmos.DrawWireCube(groundCheck.position, new Vector2(groundCheckWidth, groundCheckHeight));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement = context.ReadValue<Vector2>();
        //Debug.Log(movement);
    }

    //For jumping duh
    public void OnJump(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            jumpTriggered = true;
        }
        else if (context.canceled)
        {
            jumpTriggered = false;
        }
    }

    //For changing to red mask
    public void OnRedMask(InputAction.CallbackContext context)
    {
        if (context.started && hasRedMask)
        {
            // Implement red mask functionality here
            spriteRenderer.color = Color.red;
            // Set the player layer to RedPlayer
            gameObject.layer = LayerMask.NameToLayer("RedPlayer");
            // Set ground jump mask to red ground
            groundMask = LayerMask.GetMask("RedGround", "TrueGround");
            //Set layers to cull from camera
            camera.ToggleLayerVisibility(LayerMask.NameToLayer("RedGround"), LayerMask.NameToLayer("GreenGround"));
            camera.ToggleLayerVisibility(LayerMask.NameToLayer("RedProjectile"), LayerMask.NameToLayer("GreenProjectile"));
            //Change TrueProjectile layer to collide with red ground
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("TrueProjectile"), LayerMask.NameToLayer("GreenGround"), true);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("TrueProjectile"), LayerMask.NameToLayer("RedGround"), false);
            Instantiate(GreentoRedPrefab, groundCheck.position + Vector3.up * 0.5f, Quaternion.identity);


        }
    }

    //Same thing but for green mask
    public void OnGreenMask(InputAction.CallbackContext context)
    {
        if (context.started && hasGreenMask)
        {
            // Implement green mask functionality here
            spriteRenderer.color = Color.cyan;
            // Sets the player layer to GreenPlayer
            gameObject.layer = LayerMask.NameToLayer("GreenPlayer");
            // Set ground jump mask to green ground
            groundMask = LayerMask.GetMask("GreenGround", "TrueGround");
            //Set layers to cull from camera
            camera.ToggleLayerVisibility(LayerMask.NameToLayer("GreenGround"), LayerMask.NameToLayer("RedGround"));
            camera.ToggleLayerVisibility(LayerMask.NameToLayer("GreenProjectile"), LayerMask.NameToLayer("RedProjectile"));
            //Change TrueProjectile layer to collide with green ground
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("TrueProjectile"), LayerMask.NameToLayer("RedGround"), true);
            Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("TrueProjectile"), LayerMask.NameToLayer("GreenGround"), false);
            Instantiate(RedtoGreenPrefab, groundCheck.position + Vector3.up * 0.5f, Quaternion.identity);
        }
    }
}
