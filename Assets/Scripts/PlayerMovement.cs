using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Variables de movimiento
    [SerializeField] private float moveSpeed = 5f;           // Velocidad de movimiento
    [SerializeField] private float jumpForce = 5f;           // Fuerza del salto
    [SerializeField] private float groundDrag = 0.1f;        // Fricción en el suelo
    [SerializeField] private float airDrag = 0.05f;          // Fricción en el aire
    [SerializeField] private float fallMultiplier = 2.5f;    // Cae más rápido
    [SerializeField] private float lowJumpMultiplier = 2f;   // Salta menos si sueltas rápido

    // Variables de salto
    private int jumpCount = 0;                               // Contador de saltos (0, 1, 2)
    private int maxJumps = 2;                                // Máximo 2 saltos (doble salto)
    private bool isGrounded = false;                         // ¿Está en el suelo?

    // Referencias
    private Rigidbody2D rb;
    private float horizontalInput;

    // Detección de suelo
    [SerializeField] private float groundCheckDistance = 0.1f;
    [SerializeField] private LayerMask groundLayer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Debug.Log("Update se está ejecutando");

        // Detectar entrada del jugador
        HandleInput();

        // Detectar si está en el suelo
        CheckGroundCollision();

        // Aplicar física especial al salto
        HandleJumpPhysics();
    }

    void FixedUpdate()
    {
        // Aplicar movimiento
        MovePlayer();
    }

    /// <summary>
    /// Detecta la entrada del teclado
    /// </summary>
    void HandleInput()
    {
        // Movimiento izquierda/derecha (A/D o Flechas)
        horizontalInput = Keyboard.current.aKey.isPressed ? -1f :
                     Keyboard.current.dKey.isPressed ? 1f :
                     Keyboard.current.leftArrowKey.isPressed ? -1f :
                     Keyboard.current.rightArrowKey.isPressed ? 1f : 0f;

        // Saltar con Espacio
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            Jump();
        }
        // Para testear (eliminar después)
        if (UnityEngine.InputSystem.Keyboard.current.hKey.wasPressedThisFrame)
        {
            GetComponent<HealthSystem>().TakeDamage(1, transform.position);
        }

        if (UnityEngine.InputSystem.Keyboard.current.kKey.wasPressedThisFrame)
        {
            GetComponent<HealthSystem>().Heal(1);
        }

        if (UnityEngine.InputSystem.Keyboard.current.uKey.wasPressedThisFrame)
        {
            GetComponent<HealthSystem>().IncreaseMaxHealth(1);
        }
    }

    /// <summary>
    /// Detecta si el personaje está tocando el suelo
    /// </summary>
    void CheckGroundCollision()
    {
        // Usa ContactPoint para detectar colisiones
        ContactPoint2D[] contacts = new ContactPoint2D[10];
        int contactCount = rb.GetContacts(contacts);

        isGrounded = false;

        // Verifica si hay contacto hacia abajo
        for (int i = 0; i < contactCount; i++)
        {
            Debug.Log($"Contacto {i}: normal = {contacts[i].normal}, normal.y = {contacts[i].normal.y}");

            // Si el contacto está abajo del personaje (normal.y cerca de 1 o -1)
            if (contacts[i].normal.y > 0.5f || contacts[i].normal.y < -0.5f)
            {
                isGrounded = true;
                break;
            }
        }

        Debug.Log($"IsGrounded: {isGrounded}, Contactos: {contactCount}, jumpCount: {jumpCount}");

        // Si está en el suelo, reinicia los saltos
        if (isGrounded)
        {
            jumpCount = 0;
        }
    }

    /// <summary>
    /// Maneja el salto (incluyendo doble salto) con fuerza progresiva
    /// </summary>
    void Jump()
    {
        // Solo puede saltar si tiene saltos disponibles
        if (jumpCount < maxJumps)
        {
            // Reinicia velocidad vertical antes de saltar
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            // Calcula la fuerza del salto: cada salto es un 70% del anterior
            float jumpForceMultiplier = Mathf.Pow(0.7f, jumpCount);
            float adjustedJumpForce = jumpForce * jumpForceMultiplier;

            // Aplica fuerza hacia arriba (más débil en cada salto)
            rb.AddForce(Vector2.up * adjustedJumpForce, ForceMode2D.Impulse);

            Debug.Log($"Salto {jumpCount + 1}: Fuerza = {adjustedJumpForce:F2}");

            // Aumenta el contador
            jumpCount++;
        }
    }
    
    /// <summary>
    /// Mueve el personaje horizontalmente
    /// </summary>
    void MovePlayer()
    {
        // Calcula la velocidad deseada
        float targetVelocityX = horizontalInput * moveSpeed;

        // Aplica la velocidad horizontalmente
        rb.linearVelocity = new Vector2(targetVelocityX, rb.linearVelocity.y);

        // Aplica drag según si está en el suelo o en el aire
        rb.linearDamping = isGrounded ? groundDrag : airDrag;
    }

    /// <summary>
    /// Aplica física especial para hacer el salto más natural
    /// </summary>
    void HandleJumpPhysics()
    {
        // Si está cayendo, cae más rápido
        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        // Si está saltando pero suelta el botón, sube menos
        else if (rb.linearVelocity.y > 0 && !Keyboard.current.spaceKey.isPressed)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }
}