using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class BossController : MonoBehaviour
{
    bool isGrounded;
    public LayerMask groundLayerMask;
    public States state;
    Animator anim;
    Rigidbody2D rb;
    [SerializeField] public float xvel;
    public GameObject attackTarget;

    [Header("boss health")]
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;

    [SerializeField] float leftSpeed = -6;
    [SerializeField] float rightSpeed = 6;

    [Header("jump values")]
    [SerializeField] float jumpForce = 7f;
    [SerializeField] float jumpCooldown = 20f;

    private float jumpTimer;

    [Header("rock throwable")]
    public GameObject rock;
    public Transform rockPos;
    private float timer;
    private GameObject player;
    [SerializeField] float throwCooldown = 6.5f;

    public enum States
    {
        move,
        jump,
        throwRock,
        takeDamage,
        dying
    }

    public void HitPlayer(Transform playerTransform)
    {
        //FindObjectOfType<HealthScript>.TakeDamage;
    }

    void Start()
    {
        groundLayerMask = LayerMask.GetMask("Ground");
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currentHealth = maxHealth;
        xvel = 5;

        //states
        state = States.move;

        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        jumpTimer += Time.deltaTime;

        if (jumpTimer >= jumpCooldown &&
            state != States.dying &&
            state != States.takeDamage &&
            state != States.throwRock)
        {
            jumpTimer = 0;
            Jump();
        }

        switch (state)
        {
            case States.move:
                Move();
                break;

            case States.throwRock:
                break;

            case States.takeDamage:
                break;

            case States.jump:
                break;

            case States.dying:
                Die();
                break;
        }


        float bossDistance = Vector2.Distance(transform.position, player.transform.position);
        //Debug.Log(bossDistance);

        if(bossDistance < 29)
        {
            timer += Time.deltaTime;

            if (timer > throwCooldown)
            {
                timer = 0;
                throwRock();
            }
        } 
    }

    private void Move()
    {
        if (xvel < 0)
        {
            if (ExtendedRayCollisionCheck(-2, 0) == false)
            {
                xvel = rightSpeed;
                gameObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        if (xvel > 0)
        {
            if (ExtendedRayCollisionCheck(2, 0) == false)
            {
                xvel = leftSpeed;
                gameObject.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

        rb.linearVelocity = new Vector2(xvel, rb.linearVelocity.y);

        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("boss_walking_anim"))
        {
            anim.Play("boss_walking_anim");
        }
    }

    void Jump()
    {
        state = States.jump;

        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

        anim.Play("boss_jumping_anim");

        Invoke(nameof(ReturnToMove), 1f);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            state = States.dying;
        }
        else
        {
            state = States.takeDamage;
            anim.Play("boss_hurt_anim");

            //return to move after short delay
            Invoke(nameof(ReturnToMove), 0.45f);
        }
    }

    void throwRock()
    {
        if (state == States.dying || state == States.takeDamage)
            return;

        state = States.throwRock;

        rb.linearVelocity = Vector2.zero;

        anim.Play("boss_throwing_anim");

        Instantiate(rock, rockPos.position, Quaternion.identity);

        Invoke(nameof(ReturnToMove), 0.39f); // match animation length
    }


    void ReturnToMove()
    {
        if (state != States.dying)
            state = States.move;
    }
    

    public void Die()
    {
        anim.Play("boss_die_anim");

        GetComponent<Collider2D>().enabled = false;
        rb.linearVelocity = Vector2.zero;

        // destroy after animation
        Destroy(gameObject, 1.15f);
    }

    public bool ExtendedRayCollisionCheck(float xoffs, float yoffs)
    {
        float rayLength = 2f; // length of raycast 
        bool hitSomething = false;

        //convert x and y offset into a Vector 3
        Vector3 offset = new Vector3(xoffs, yoffs, 1f);

        //cast a ray downwards 
        RaycastHit2D hit;


        hit = Physics2D.Raycast(transform.position + offset, -Vector2.up, rayLength, groundLayerMask);

        Color hitColor = Color.white;

        if (hit.collider != null)
        {
            //print("player has collided with ground layer");
            hitColor = Color.green;
            hitSomething = true;
        }

        Debug.DrawRay(transform.position + offset, Vector2.down * rayLength, hitColor);
        return hitSomething;
    }
}