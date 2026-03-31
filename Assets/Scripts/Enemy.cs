using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string enemyName = "Enemigo";
    [SerializeField] private float maxHealth = 3f;
    [SerializeField] private float currentHealth;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float patrolDistance = 5f;
    [SerializeField] private float detectionRange = 8f;
    [SerializeField] private float attackRange = 1.5f;
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private float attackCooldown = 2f;

    private Vector2 patrolDirection = Vector2.right;
    private Vector2 startPosition;
    private float lastAttackTime = -1f;
    private bool isChasing = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private PlayerMovement player;
    private EnemyLoot enemyLoot;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
        currentHealth = maxHealth;

        // Busca al jugador en la escena
        player = FindFirstObjectByType<PlayerMovement>();

        // Obtiene o crea el componente de loot
        enemyLoot = GetComponent<EnemyLoot>();
        if (enemyLoot == null)
        {
            enemyLoot = gameObject.AddComponent<EnemyLoot>();
        }

        Debug.Log($"✅ {enemyName} creado con {currentHealth} de vida");
    }

    void Update()
    {
        if (player == null || !player.gameObject.activeSelf)
            return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= detectionRange)
        {
            isChasing = true;
            ChasePlayer();
        }
        else
        {
            isChasing = false;
            Patrol();
        }
    }

    /// <summary>
    /// Patrulla
    /// </summary>
    void Patrol()
    {
        float distanceFromStart = Vector2.Distance(transform.position, startPosition);

        if (distanceFromStart >= patrolDistance)
        {
            patrolDirection *= -1;
        }

        rb.linearVelocity = new Vector2(patrolDirection.x * speed, rb.linearVelocity.y);
        FlipSprite(patrolDirection.x);
    }

    /// <summary>
    /// Persigue al jugador
    /// </summary>
    void ChasePlayer()
    {
        Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
        rb.linearVelocity = new Vector2(directionToPlayer.x * speed, rb.linearVelocity.y);
        FlipSprite(directionToPlayer.x);

        float distanceToPlayer = Vector2.Distance(transform.position, player.transform.position);

        if (distanceToPlayer <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            AttackPlayer();
        }
    }

    /// <summary>
    /// Ataca al jugador
    /// </summary>
    void AttackPlayer()
    {
        HealthSystem playerHealth = player.GetComponent<HealthSystem>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(attackDamage, transform.position);
            Debug.Log($"💥 ¡{enemyName} atacó al jugador por {attackDamage} de daño!");
        }

        lastAttackTime = Time.time;
    }

    /// <summary>
    /// Recibe daño
    /// </summary>
    public void TakeDamage(float damage, Vector3 damageSource)
    {
        currentHealth -= damage;
        Debug.Log($"🩹 {enemyName} recibió {damage} de daño. Vida: {currentHealth}/{maxHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// Muere y dropea loot
    /// </summary>
    void Die()
    {
        Debug.Log($"💀 ¡{enemyName} ha muerto!");

        // Dropea loot antes de destruirse
        if (enemyLoot != null)
        {
            enemyLoot.DropLoot(transform.position);
        }

        Destroy(gameObject);
    }

    /// <summary>
    /// Voltea el sprite
    /// </summary>
    void FlipSprite(float directionX)
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = directionX < 0;
        }
    }

    public string GetEnemyName() => enemyName;
    public bool IsChasing() => isChasing;
}