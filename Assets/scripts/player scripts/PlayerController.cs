using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    private PlayerInput playerInput;
    public Transform groundCheck;
    public LayerMask groundLayer;
    Animator anim;

    public Transform resetPoint;

    private bool isFacingRight = true;
    private float horizontal;

    [Header("player values")]
    [SerializeField] float speed = 8f;
    [SerializeField] float jumpingPower = 16;
    [SerializeField] float maxVertSpeed;

    public int jumpsLeft;

    [Header("dashing values")]
    public float dashForce = 30f;
    public float dashTime = 0.2f;
    public float dashCooldown = 1f;

    public bool isDashing;
    private float dashTimer;
    private float cooldownTimer;
    private bool hasDashedInAir;

    [Header("attack values")]
    public int attackDamage = 50;
    public float attackrate = 2f;
    [SerializeField] float attackDuration = 0.2f;
    [SerializeField] float slashDuration = 1f;
    [SerializeField] float attackCooldown = 0.4f;

    private float nextAttackTime;
    private float attackTimer;
    public GameObject slashEffect;
    public bool isAttacking;

    public LayerMask enemyLayers;

    public PolygonCollider2D attackHitbox;

    private int maxJumps = 2;
    private float originalGravity;

   
    InputAction moveAction;
    InputAction jumpAction;
    InputAction dashAction;
    InputAction attackAction;

    public enum PlayerAnimation
    {
        Idle,
        Walking,
        Jumping,
        Falling,
        Dashing,
        DoubleJumping,
        Attacking
    }

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        moveAction = playerInput.actions["Move"];
        jumpAction = playerInput.actions["Jump"];
        dashAction = playerInput.actions["Dash"];
        attackAction = playerInput.actions["Attack"];

        jumpsLeft = maxJumps;
        originalGravity = rb.gravityScale;

        // Disable attack hitbox at start
        attackHitbox.enabled = false;
    }

    private void FixedUpdate()
    {
        if (rb.linearVelocity.magnitude > maxVertSpeed)
        {
            rb.linearVelocity = rb.linearVelocity.normalized * maxVertSpeed;
        }
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAttacking)
        {
            if (((1 << collision.gameObject.layer) & enemyLayers) != 0)
            {
                Debug.Log("we hit " + collision.name);

                DoEnemyDamage(collision.gameObject);
            }
        }

        if (collision.gameObject.CompareTag("Spike"))
        {
            collision.gameObject.GetComponent<PlayerController>().resetPoint = resetPoint;
        }
    }

    private void Update()
    {
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;

            if (attackTimer <= 0f)
            {
                isAttacking = false;
            }
        }

        Jump();
        Move();
        Dash();

        if (attackAction.WasPressedThisFrame() && Time.time >= nextAttackTime)
        {
            nextAttackTime = Time.time + attackCooldown;
            Attack();
        }

        FlipCheck();
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        PlayerAnimation currentAnim;

        if (isDashing)
        {
            currentAnim = PlayerAnimation.Dashing;
        }
        else if (isAttacking == true)
        {
            currentAnim = PlayerAnimation.Attacking;
        }
        else if (!IsGrounded() &&rb.linearVelocity.y > 0.1f &&jumpsLeft == 0)
        {
            currentAnim = PlayerAnimation.DoubleJumping;
        }
        else if (!IsGrounded() && rb.linearVelocity.y > 0.1f)
        {
            currentAnim = PlayerAnimation.Jumping;
        }
        else if (!IsGrounded() && rb.linearVelocity.y < -0.1f)
        {
            currentAnim = PlayerAnimation.Falling;
        }
        else if (Mathf.Abs(horizontal) > 0.1f)
        {
            currentAnim = PlayerAnimation.Walking;
        }
        else
        {
            currentAnim = PlayerAnimation.Idle;
        }

        anim.Play(GetAnimationName(currentAnim));
    }

    string GetAnimationName(PlayerAnimation animType)
    {
        switch (animType)
        {
            case PlayerAnimation.Idle: return "idle_anim";

            case PlayerAnimation.Walking: return "walking_anim";

            case PlayerAnimation.Jumping: return "jumping_anim";

            case PlayerAnimation.Falling: return "falling_anim";

            case PlayerAnimation.Dashing: return "dash_anim";

            case PlayerAnimation.DoubleJumping: return "double_jump_anim";

            case PlayerAnimation.Attacking: return "attack_anim";

            default: return "idle_anim";
        }
    }

    private void FlipCheck()
    {
        if (isFacingRight && horizontal > 0f ||!isFacingRight && horizontal < 0f)
        {
            Flip();
        }
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position,0.3f,groundLayer);
    }

    public void Move()
    {
        if (isDashing) return;

        horizontal = moveAction.ReadValue<Vector2>().x;

        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);
    }

    public void Attack()
    {
        isAttacking = true;

        attackTimer = attackDuration;

        StartCoroutine(ShowSlash());
        StartCoroutine(EnableAttackHitbox());
    }

    IEnumerator EnableAttackHitbox()
    {
        attackHitbox.enabled = true;
        yield return new WaitForSeconds(attackDuration);
        attackHitbox.enabled = false;
    }

    IEnumerator ShowSlash()
    {
        slashEffect.SetActive(true);
        yield return new WaitForSeconds(slashDuration);
        slashEffect.SetActive(false);
    }

    void DoEnemyDamage(GameObject enemy)
    {
        if (enemy.tag == "enemy1")
        {
            enemy.GetComponent<EnemyControllerOne>().TakeDamage(attackDamage);
        }
        if (enemy.tag == "enemy2")
        {
            enemy.GetComponent<EnemyControllerTwo>().TakeDamage(attackDamage);
        }
        if (enemy.tag == "boss")
        {
            enemy.GetComponent<BossController>().TakeDamage(attackDamage);
        }
    }

    public void Jump()
    {
        if (isDashing) return;

        if (IsGrounded() &&
            jumpAction.WasPressedThisFrame())
        {
            jumpsLeft = maxJumps;
        }

        if (jumpAction.WasPressedThisFrame() &&
            jumpsLeft > 0)
        {
            rb.linearVelocity =new Vector2(rb.linearVelocity.x,jumpingPower);
            jumpsLeft--;
        }

        if (!jumpAction.IsPressed() &&
            rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * 0.5f);
        }
    }

    public void Dash()
    {
        cooldownTimer -= Time.deltaTime;
        bool grounded = IsGrounded();

        if (grounded)
        {
            hasDashedInAir = false;
        }

        if (dashAction.WasPressedThisFrame())
        {
            if (grounded)
            {
                if (cooldownTimer <= 0f)
                {
                    StartDash();
                }
            }
            else
            {
                if (!hasDashedInAir)
                {
                    StartDash();
                    hasDashedInAir = true;
                }
            }
        }

        if (isDashing)
        {
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0f)
            {
                isDashing = false;
                rb.gravityScale = originalGravity;

                rb.linearVelocity =new Vector2(0f,rb.linearVelocity.y);
            }
        }
    }

    void StartDash()
    {
        isDashing = true;
        dashTimer = dashTime;
        cooldownTimer = dashCooldown;

        float direction =Mathf.Sign(transform.localScale.x);

        rb.gravityScale = 0f;
        rb.linearVelocity =new Vector2(-direction * dashForce,0f);
    }
}