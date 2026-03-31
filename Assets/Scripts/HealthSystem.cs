using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float maxHealth = 10f;
    [SerializeField] private float currentHealth;
    [SerializeField] private bool invulnerable = false;
    [SerializeField] private float invulnerableDuration = 1f;

    private float invulnerableTimer = 0f;
    private SpriteRenderer spriteRenderer;

    public delegate void OnHealthChanged(float current, float max);
    public event OnHealthChanged HealthChanged;

    public delegate void OnDeath();
    public event OnDeath Death;

    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        Debug.Log($"❤️ {gameObject.name} creado con {currentHealth}/{maxHealth} de vida");
    }

    void Update()
    {
        if (invulnerable)
        {
            invulnerableTimer -= Time.deltaTime;
            if (invulnerableTimer <= 0f)
            {
                invulnerable = false;
                SetNormalColor();
            }
        }
    }

    public void TakeDamage(float damage, Vector3 damageSource)
    {
        if (invulnerable)
        {
            Debug.Log($"🛡️ {gameObject.name} está invulnerable");
            return;
        }

        currentHealth -= damage;
        Debug.Log($"💥 {gameObject.name} recibió {damage} de daño. Vida: {currentHealth}/{maxHealth}");

        SetDamagedColor();
        HealthChanged?.Invoke(currentHealth, maxHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            HealthChanged?.Invoke(currentHealth, maxHealth);
            Die();
        }
    }

    public void Heal(float amount)
    {
        if (currentHealth >= maxHealth)
        {
            Debug.LogWarning($"⚠️ {gameObject.name} ya está con vida máxima");
            return;
        }

        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        Debug.Log($"💚 {gameObject.name} se curó {amount}. Vida: {currentHealth}/{maxHealth}");
        HealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void IncreaseMaxHealth(float amount)
    {
        maxHealth += amount;
        currentHealth = maxHealth;
        Debug.Log($"💪 Vida máxima aumentada a {maxHealth}");
        HealthChanged?.Invoke(currentHealth, maxHealth);
    }

    public void SetInvulnerable(float duration)
    {
        invulnerable = true;
        invulnerableTimer = duration;
        Debug.Log($"🛡️ {gameObject.name} es invulnerable por {duration}s");
        SetInvulnerableColor();
    }

    private void SetDamagedColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.red;
            Invoke(nameof(SetNormalColor), 0.2f);
        }
    }

    private void SetInvulnerableColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.cyan;
        }
    }

    private void SetNormalColor()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = Color.white;
        }
    }

    private void Die()
    {
        Debug.Log($"💀 {gameObject.name} ha muerto");
        Death?.Invoke();

        // Muestra Game Over si es el jugador
        if (CompareTag("Player"))
        {
            GameOverUI gameOverUI = FindFirstObjectByType<GameOverUI>();
            if (gameOverUI != null)
            {
                gameOverUI.ShowGameOverMenu();
            }
        }

        Destroy(gameObject);
    }

    public float GetCurrentHealth() => currentHealth;
    public float GetMaxHealth() => maxHealth;
    public bool IsInvulnerable() => invulnerable;
}