using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private TextMeshProUGUI pauseTitle;

    private PauseManager pauseManager;
    private UIManager uiManager;

    void Start()
    {
        pauseManager = PauseManager.GetInstance();
        uiManager = UIManager.GetInstance();

        if (resumeButton != null)
        {
            resumeButton.onClick.AddListener(OnResumePressed);
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(OnRestartPressed);
        }

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(OnQuitPressed);
        }

        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(false);
        }

        if (pauseManager != null)
        {
            pauseManager.PauseChanged += UpdatePauseMenuUI;
        }
    }

    /// <summary>
    /// Actualiza la UI del menú de pausa
    /// </summary>
    void UpdatePauseMenuUI(bool isPaused)
    {
        if (pauseMenuPanel != null)
        {
            pauseMenuPanel.SetActive(isPaused);
        }

        if (pauseTitle != null)
        {
            pauseTitle.text = isPaused ? "⏸️ PAUSA" : "";
        }
    }

    /// <summary>
    /// Botón: Continuar
    /// </summary>
    void OnResumePressed()
    {
        if (pauseManager != null)
        {
            pauseManager.Resume();
            Debug.Log("▶️ Juego reanudado");
        }
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

    void OnDestroy()
    {
        if (pauseManager != null)
        {
            pauseManager.PauseChanged -= UpdatePauseMenuUI;
        }
    }
}