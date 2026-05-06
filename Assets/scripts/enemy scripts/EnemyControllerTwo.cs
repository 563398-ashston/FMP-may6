using UnityEngine;

public class EnemyControllerTwo : MonoBehaviour
{
    [SerializeField] private float speed = 1.5f;
    [SerializeField] private float activationRange = 5f;

    private GameObject player;

    public States state;
    

    private Animator anim;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    // health
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;

    public enum States
    {
        idle,
        move,
        takeDamage,
        dying
    }

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentHealth = maxHealth;

        // enemy starts idle
        state = States.idle;
    }

    private void Update()
    {
        // state behavior
        switch (state)
        {
            case States.idle:
                Idle();
                break;

            case States.move:
                Move();
                break;

            case States.takeDamage:
                
                break;

            case States.dying:
                Die();
                break;
        }
    }

    private void Idle()
    {
        float distance = Vector2.Distance(transform.position,player.transform.position);
        anim.Play("enemy_2_idle");

        if (distance <= activationRange)
        {
            state = States.move;
        }
    }

    private void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position,speed * Time.deltaTime);
        anim.Play("enemy_2_walking");

        FlipSprite();
    }

    private void FlipSprite()
    {
        if (player.transform.position.x > transform.position.x)
        {
            sr.flipX = false;
        }
        else if (player.transform.position.x < transform.position.x)
        {
            sr.flipX = true;
        }
    }

    public void TakeDamage(int damage)
    {
        anim.Play("enemy_2_hurt");

        if (state == States.dying)
            return;

        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            state = States.dying;
        }
        else
        {
            state = States.takeDamage;

            Invoke(nameof(ReturnToMove), 0.25f);
        }
    }

    private void ReturnToMove()
    {
        if (state != States.dying)
        {
            state = States.move;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            currentHealth = 0;
            state = States.dying;
        }
    }

    private bool hasDied = false;

    private void Die()
    {
        // prevents Die() from running every frame
        if (hasDied)
            return;

        hasDied = true;

        GetComponent<Collider2D>().enabled = false;

        rb.linearVelocity = Vector2.zero;

        anim.Play("enemy_2_dying");
        Destroy(gameObject, 0.35f);
    }
}