using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI enemiesDefeatedText;
    [SerializeField] private TextMeshProUGUI itemsCollectedText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Image healthBar;
    [SerializeField] private TextMeshProUGUI pauseText;

    private GameManager gameManager;
    private HealthSystem playerHealth;
    private PauseManager pauseManager;

    void Start()
    {
        gameManager = GameManager.GetInstance();
        pauseManager = PauseManager.GetInstance();
        playerHealth = FindFirstObjectByType<PlayerMovement>().GetComponent<HealthSystem>();

        if (gameManager != null)
        {
            gameManager.ScoreChanged += UpdateScore;
            gameManager.EnemyDefeated += UpdateEnemiesDefeated;
            gameManager.ItemCollected += UpdateItemsCollected;
        }

        if (playerHealth != null)
        {
            playerHealth.HealthChanged += UpdateHealthBar;
        }

        if (pauseManager != null)
        {
            pauseManager.PauseChanged += OnPauseChanged;
        }

        UpdateUI();
    }

    /// <summary>
    /// Actualiza la UI completa
    /// </summary>
    void UpdateUI()
    {
        if (gameManager != null)
        {
            UpdateScore(gameManager.GetScore());
            UpdateEnemiesDefeated(gameManager.GetEnemiesDefeated());
            UpdateItemsCollected(gameManager.GetItemsCollected());
        }

        if (playerHealth != null)
        {
            UpdateHealthBar(playerHealth.GetCurrentHealth(), playerHealth.GetMaxHealth());
        }
    }

    /// <summary>
    /// Actualiza el texto de puntos
    /// </summary>
    void UpdateScore(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Puntos: {newScore}";
        }
    }

    /// <summary>
    /// Actualiza enemigos derrotados
    /// </summary>
    void UpdateEnemiesDefeated(int total)
    {
        if (enemiesDefeatedText != null)
        {
            enemiesDefeatedText.text = $"Enemigos: {total}";
        }
    }

    /// <summary>
    /// Actualiza items recogidos
    /// </summary>
    void UpdateItemsCollected(int total)
    {
        if (itemsCollectedText != null)
        {
            itemsCollectedText.text = $"Items: {total}";
        }
    }

    /// <summary>
    /// Actualiza la barra de vida
    /// </summary>
    void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (healthText != null)
        {
            healthText.text = $"Vida: {currentHealth}/{maxHealth}";
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = currentHealth / maxHealth;
        }
    }

    /// <summary>
    /// Muestra/oculta texto de pausa
    /// </summary>
    void OnPauseChanged(bool paused)
    {
        if (pauseText != null)
        {
            pauseText.enabled = paused;
            pauseText.text = paused ? "⏸️ PAUSADO - Presiona I para cerrar" : "";
        }
    }

    void OnDestroy()
    {
        if (gameManager != null)
        {
            gameManager.ScoreChanged -= UpdateScore;
            gameManager.EnemyDefeated -= UpdateEnemiesDefeated;
            gameManager.ItemCollected -= UpdateItemsCollected;
        }

        if (playerHealth != null)
        {
            playerHealth.HealthChanged -= UpdateHealthBar;
        }

        if (pauseManager != null)
        {
            pauseManager.PauseChanged -= OnPauseChanged;
        }
    }
}