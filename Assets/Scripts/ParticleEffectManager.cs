using UnityEngine;

public class ParticleEffectManager : MonoBehaviour
{
    private static ParticleEffectManager instance;

    [SerializeField] private GameObject hitEffectPrefab;
    [SerializeField] private GameObject bloodEffectPrefab;
    [SerializeField] private GameObject healEffectPrefab;
    [SerializeField] private GameObject lootEffectPrefab;

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

    /// <summary>
    /// Efecto de golpe
    /// </summary>
    public void PlayHitEffect(Vector3 position)
    {
        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 1f);
            Debug.Log("💥 Efecto de golpe");
        }
    }

    /// <summary>
    /// Efecto de sangre
    /// </summary>
    public void PlayBloodEffect(Vector3 position)
    {
        if (bloodEffectPrefab != null)
        {
            GameObject effect = Instantiate(bloodEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 1f);
            Debug.Log("🩸 Efecto de sangre");
        }
    }

    /// <summary>
    /// Efecto de curación
    /// </summary>
    public void PlayHealEffect(Vector3 position)
    {
        if (healEffectPrefab != null)
        {
            GameObject effect = Instantiate(healEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 1f);
            Debug.Log("💚 Efecto de curación");
        }
    }

    /// <summary>
    /// Efecto de loot
    /// </summary>
    public void PlayLootEffect(Vector3 position)
    {
        if (lootEffectPrefab != null)
        {
            GameObject effect = Instantiate(lootEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 1f);
            Debug.Log("💰 Efecto de loot");
        }
    }

    public static ParticleEffectManager GetInstance() => instance;
}