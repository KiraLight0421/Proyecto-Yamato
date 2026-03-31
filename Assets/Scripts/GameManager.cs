using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField] private int score = 0;
    [SerializeField] private int enemiesDefeated = 0;
    [SerializeField] private int itemsCollected = 0;

    public delegate void OnScoreChanged(int newScore);
    public event OnScoreChanged ScoreChanged;

    public delegate void OnEnemyDefeated(int totalDefeated);
    public event OnEnemyDefeated EnemyDefeated;

    public delegate void OnItemCollected(int totalCollected);
    public event OnItemCollected ItemCollected;

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
        Debug.Log("✅ GameManager inicializado");
    }

    /// <summary>
    /// Añade puntos
    /// </summary>
    public void AddScore(int points)
    {
        score += points;
        Debug.Log($"⭐ Puntos ganados: +{points}. Total: {score}");
        ScoreChanged?.Invoke(score);
    }

    /// <summary>
    /// Registra enemigo derrotado
    /// </summary>
    public void EnemyKilled(int pointsReward = 10)
    {
        enemiesDefeated++;
        AddScore(pointsReward);
        Debug.Log($"💀 Enemigos derrotados: {enemiesDefeated}");
        EnemyDefeated?.Invoke(enemiesDefeated);
    }

    /// <summary>
    /// Registra item recogido
    /// </summary>
    public void ItemPickedUp(int pointsReward = 5)
    {
        itemsCollected++;
        AddScore(pointsReward);
        Debug.Log($"📦 Items recogidos: {itemsCollected}");
        ItemCollected?.Invoke(itemsCollected);
    }

    /// <summary>
    /// Resetea el juego
    /// </summary>
    public void ResetGame()
    {
        score = 0;
        enemiesDefeated = 0;
        itemsCollected = 0;
        Time.timeScale = 1f;
        Debug.Log("🔄 Juego reseteado");
    }

    public int GetScore() => score;
    public int GetEnemiesDefeated() => enemiesDefeated;
    public int GetItemsCollected() => itemsCollected;
    public static GameManager GetInstance() => instance;
}