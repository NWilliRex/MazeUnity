using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SimpleEnemyPatrol : MonoBehaviour
{
    public Transform leftPoint, rightPoint; // Points de patrouille
    public float speed = 2f;
    public int touchDamage = 1;

    private bool toRight = true;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        // animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        float dir = toRight ? 1f : -1f;

        // Déplacement avec velocity
        rb.linearVelocity = new Vector2(dir * speed, rb.linearVelocity.y);

        // Flip du sprite
        sr.flipX = !toRight;

        // Animation (optionnel si tu veux)
        // animator?.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

        // Vérifie les points de patrouille
        if (toRight && transform.position.x >= rightPoint.position.x)
            toRight = false;
        else if (!toRight && transform.position.x <= leftPoint.position.x)
            toRight = true;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.CompareTag("Player"))
        {
            var hp = col.collider.GetComponent<PlayerHealth>();
            if (hp) hp.TakeDamage(touchDamage);
        }
    }
}
