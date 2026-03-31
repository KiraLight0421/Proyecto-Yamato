using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private Shield shieldScript;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        shieldScript = GetComponent<Shield>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        if (animator == null)
        {
            Debug.LogWarning("⚠️ Animator no encontrado");
        }
    }

    void Update()
    {
        if (animator == null)
            return;

        HandleMovementAnimation();
        HandleCombatAnimation();
    }

    /// <summary>
    /// Anima el movimiento
    /// </summary>
    void HandleMovementAnimation()
    {
        // Detecta entrada de movimiento
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Parámetros de animación
        animator.SetFloat("MoveX", horizontal);
        animator.SetFloat("MoveY", vertical);
        animator.SetBool("IsMoving", horizontal != 0 || vertical != 0);

        // Voltea el sprite según la dirección
        if (horizontal > 0)
        {
            spriteRenderer.flipX = false;
        }
        else if (horizontal < 0)
        {
            spriteRenderer.flipX = true;
        }
    }

    /// <summary>
    /// Anima el combate
    /// </summary>
    void HandleCombatAnimation()
    {
        if (shieldScript == null)
            return;

        bool isDefending = shieldScript.IsDefending();
        animator.SetBool("IsDefending", isDefending);
    }

    public void TriggerAttackAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Attack");
        }
    }

    public void TriggerHurtAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Hurt");
        }
    }

    public void TriggerDeathAnimation()
    {
        if (animator != null)
        {
            animator.SetTrigger("Death");
        }
    }
}