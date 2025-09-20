using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 move;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Rigidbody2D rb;

    [Header("Audio - Pas")]
    public AudioSource footstepsAudio;
    public AudioClip footstepsClip;

    [Header("Audio - Attaque")]
    public AudioSource attackAudio;
    public AudioClip attackClip;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        if (footstepsAudio == null)
            footstepsAudio = gameObject.AddComponent<AudioSource>();
        footstepsAudio.clip = footstepsClip;
        footstepsAudio.loop = true;
        footstepsAudio.playOnAwake = false;

        if (attackAudio == null)
            attackAudio = gameObject.AddComponent<AudioSource>();
        attackAudio.clip = attackClip;
        attackAudio.loop = false;
        attackAudio.playOnAwake = false;
    }

    void Update()
    {
        // Déplacement complet
        move.x = Input.GetAxisRaw("Horizontal");
        move.y = Input.GetAxisRaw("Vertical");

        move.Normalize(); // diagonale pas plus rapide

        // Animation horizontal (tu peux ajouter vertical si tu veux)
        animator.SetFloat("x", Mathf.Abs(move.x));

        // Orientation
        if (move.x > 0) spriteRenderer.flipX = false;
        if (move.x < 0) spriteRenderer.flipX = true;

        // Attaque
        if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetBool("Attack", true);
            if (attackClip != null) attackAudio.PlayOneShot(attackClip);
        }
        else if (Input.GetKeyUp(KeyCode.Space))
            animator.SetBool("Attack", false);

        // Pas
        if (move.magnitude > 0)
        {
            if (!footstepsAudio.isPlaying) footstepsAudio.Play();
        }
        else
        {
            if (footstepsAudio.isPlaying) footstepsAudio.Stop();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = move * speed;
        rb.rotation = 0f;
    }
}
