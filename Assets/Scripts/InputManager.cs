using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager instance;
    private PauseManager pauseManager;

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
        pauseManager = PauseManager.GetInstance();
    }

    void Update()
    {
        HandlePause();
    }

    /// <summary>
    /// Maneja la pausa
    /// </summary>
    void HandlePause()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (pauseManager != null)
            {
                pauseManager.TogglePause();
            }
        }
    }

    public static InputManager GetInstance() => instance;
}