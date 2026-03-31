using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private TextMeshProUGUI enemiesText;
    [SerializeField] private TextMeshProUGUI itemsText;
    [SerializeField] private Image healthBar;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gameOverPanel;

    private GameManager gameManager;
    private PauseManager pauseManager;
    private PlayerMovement player;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        gameManager = GameManager.GetInstance();
        pauseManager = PauseManager.GetInstance();
        player = FindFirstObjectByType<PlayerMovement>();

        if (gameManager != null)
        {
            gameManager.ScoreChanged += UpdateScore;
        }

        if (pauseManager != null)
        {
            pauseManager.PauseChanged += UpdatePausePanel;
        }

        UpdateScore(gameManager?.GetScore() ?? 0);
    }

    /// <summary>
    /// Actualiza puntuación
    /// </summary>
    void UpdateScore(int newScore)
    {
        if (scoreText != null)
        {
            scoreText.text = $"Puntos: {newScore}";
        }
    }

    /// <summary>
    /// Actualiza vida
    /// </summary>
    public void UpdateHealth(float current, float max)
    {
        if (healthText != null)
        {
            healthText.text = $"Vida: {current}/{max}";
        }

        if (healthBar != null)
        {
            healthBar.fillAmount = current / max;
        }
    }

    /// <summary>
    /// Actualiza contador de enemigos
    /// </summary>
    public void UpdateEnemies(int count)
    {
        if (enemiesText != null)
        {
            enemiesText.text = $"Enemigos: {count}";
        }
    }

    /// <summary>
    /// Actualiza contador de items
    /// </summary>
    public void UpdateItems(int count)
    {
        if (itemsText != null)
        {
            itemsText.text = $"Items: {count}";
        }
    }

    /// <summary>
    /// Muestra/oculta panel de pausa
    /// </summary>
    void UpdatePausePanel(bool isPaused)
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(isPaused);
        }
    }

    /// <summary>
    /// Muestra Game Over
    /// </summary>
    public void ShowGameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
    }

    /// <summary>
    /// Botón: Continuar
    /// </summary>
    public void OnResumePressed()
    {
        if (pauseManager != null)
        {
            pauseManager.Resume();
        }
    }

    /// <summary>
    /// Botón: Reiniciar
    /// </summary>
    public void OnRestartPressed()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// Botón: Salir
    /// </summary>
    public void OnQuitPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public static UIManager GetInstance() => instance;
}