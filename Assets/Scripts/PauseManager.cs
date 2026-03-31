using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private static PauseManager instance;
    private bool isPaused = false;

    public delegate void OnPauseChanged(bool paused);
    public event OnPauseChanged PauseChanged;

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

    void Update()
    {
        // I para pausar/despausar
        if (Input.GetKeyDown(KeyCode.I))
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Alterna pausa
    /// </summary>
    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            Debug.Log("⏸️ JUEGO PAUSADO");
        }
        else
        {
            Time.timeScale = 1f;
            Debug.Log("▶️ JUEGO REANUDADO");
        }

        PauseChanged?.Invoke(isPaused);
    }

    /// <summary>
    /// Pausa el juego
    /// </summary>
    public void Pause()
    {
        if (!isPaused)
        {
            TogglePause();
        }
    }

    /// <summary>
    /// Reanuda el juego
    /// </summary>
    public void Resume()
    {
        if (isPaused)
        {
            TogglePause();
        }
    }

    public bool IsPaused() => isPaused;
    public static PauseManager GetInstance() => instance;
}