using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI gameOverTitle;
    [SerializeField] private TextMeshProUGUI finalScoreText;
    [SerializeField] private TextMeshProUGUI enemiesKilledText;
    [SerializeField] private TextMeshProUGUI itemsCollectedText;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitButton;

    private GameManager gameManager;

    void Start()
    {
        gameManager = GameManager.GetInstance();

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartPressed);
        }

        if (mainMenuButton != null)
        {
            mainMenuButton.onClick.AddListener(OnMainMenuPressed);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitPressed);
        }
    }

    /// <summary>
    /// Muestra el menú de Game Over
    /// </summary>
    public void ShowGameOverMenu()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }

        if (gameOverTitle != null)
        {
            gameOverTitle.text = "💀 ¡GAME OVER!";
        }

        if (gameManager != null)
        {
            if (finalScoreText != null)
            {
                finalScoreText.text = $"Puntuación Final: {gameManager.GetScore()}";
            }

            if (enemiesKilledText != null)
            {
                enemiesKilledText.text = $"Enemigos Derrotados: {gameManager.GetEnemiesDefeated()}";
            }

            if (itemsCollectedText != null)
            {
                itemsCollectedText.text = $"Items Recogidos: {gameManager.GetItemsCollected()}";
            }
        }

        Debug.Log("💀 Menú de Game Over mostrado");
    }

    /// <summary>
    /// Botón: Reiniciar
    /// </summary>
    void OnRestartPressed()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
        Debug.Log("🔄 Juego reiniciado");
    }

    /// <summary>
    /// Botón: Menú Principal
    /// </summary>
    void OnMainMenuPressed()
    {
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        Debug.Log("🏠 Ir a Menú Principal");
    }

    /// <summary>
    /// Botón: Salir
    /// </summary>
    void OnQuitPressed()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        Debug.Log("❌ Juego cerrado");
    }
}