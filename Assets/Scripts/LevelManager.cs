using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    [SerializeField] private string mainMenuScene = "MainMenu";
    [SerializeField] private string gameScene = "Game";
    [SerializeField] private float loadDelay = 1f;

    private string currentLevel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().name;
        Debug.Log($"🎮 Nivel actual: {currentLevel}");
    }

    /// <summary>
    /// Carga el menú principal
    /// </summary>
    public void LoadMainMenu()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync(mainMenuScene));
    }

    /// <summary>
    /// Carga el juego
    /// </summary>
    public void LoadGame()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync(gameScene));
    }

    /// <summary>
    /// Reinicia el nivel actual
    /// </summary>
    public void RestartCurrentLevel()
    {
        Time.timeScale = 1f;
        StartCoroutine(LoadSceneAsync(currentLevel));
    }

    /// <summary>
    /// Carga una escena de forma asincrónica
    /// </summary>
    System.Collections.IEnumerator LoadSceneAsync(string sceneName)
    {
        Debug.Log($"📂 Cargando escena: {sceneName}");

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        currentLevel = sceneName;
        Debug.Log($"✅ Escena cargada: {sceneName}");
    }

    /// <summary>
    /// Sale del juego
    /// </summary>
    public void QuitGame()
    {
        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif

        Debug.Log("❌ Juego cerrado");
    }

    public string GetCurrentLevel() => currentLevel;
    public static LevelManager GetInstance() => instance;
}