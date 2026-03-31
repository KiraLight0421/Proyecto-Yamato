using UnityEngine;
using System.Collections.Generic;

public class AchievementSystem : MonoBehaviour
{
    [System.Serializable]
    public class Achievement
    {
        public string achievementName;
        public string description;
        public int targetValue;
        public int currentValue;
        public bool unlocked;

        public Achievement(string name, string desc, int target)
        {
            achievementName = name;
            description = desc;
            targetValue = target;
            currentValue = 0;
            unlocked = false;
        }
    }

    private static AchievementSystem instance;

    [SerializeField] private List<Achievement> achievements = new List<Achievement>();

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
        InitializeAchievements();
    }

    /// <summary>
    /// Inicializa los logros
    /// </summary>
    void InitializeAchievements()
    {
        achievements.Clear();

        achievements.Add(new Achievement("Primera Sangre", "Derrota tu primer enemigo", 1));
        achievements.Add(new Achievement("Cazador", "Derrota 10 enemigos", 10));
        achievements.Add(new Achievement("Asesino", "Derrota 50 enemigos", 50));
        achievements.Add(new Achievement("Coleccionista", "Recoge 20 items", 20));
        achievements.Add(new Achievement("Superviviente", "Mantén 75% de vida máxima", 75));

        Debug.Log($"🏆 {achievements.Count} logros inicializados");
    }

    /// <summary>
    /// Actualiza progreso de un logro
    /// </summary>
    public void UpdateAchievementProgress(string achievementName, int value)
    {
        foreach (Achievement achievement in achievements)
        {
            if (achievement.achievementName == achievementName)
            {
                achievement.currentValue = value;

                if (!achievement.unlocked && achievement.currentValue >= achievement.targetValue)
                {
                    UnlockAchievement(achievementName);
                }

                return;
            }
        }
    }

    /// <summary>
    /// Desbloquea un logro
    /// </summary>
    void UnlockAchievement(string achievementName)
    {
        foreach (Achievement achievement in achievements)
        {
            if (achievement.achievementName == achievementName && !achievement.unlocked)
            {
                achievement.unlocked = true;
                Debug.Log($"🏆 ¡LOGRO DESBLOQUEADO! {achievementName}");
                return;
            }
        }
    }

    /// <summary>
    /// Obtiene estado de un logro
    /// </summary>
    public Achievement GetAchievement(string name)
    {
        foreach (Achievement achievement in achievements)
        {
            if (achievement.achievementName == name)
            {
                return achievement;
            }
        }

        return null;
    }

    public List<Achievement> GetAllAchievements() => achievements;
    public static AchievementSystem GetInstance() => instance;
}