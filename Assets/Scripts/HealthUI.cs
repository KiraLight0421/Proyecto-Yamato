using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Sprite heartFull;
    [SerializeField] private Sprite heartHalf;
    [SerializeField] private Sprite heartEmpty;
    [SerializeField] private Transform heartsContainer;
    [SerializeField] private float spacing = 10f;

    private Image[] heartImages;
    private HealthSystem playerHealth;

    void Awake()
    {
        Debug.LogError("⚠️ HEALTHUI AWAKE EJECUTADO");
    }

    void Start()
    {
        Debug.LogError("⚠️ START INICIO");

        playerHealth = GetComponent<HealthSystem>();
        Debug.LogError($"⚠️ playerHealth = {playerHealth}");

        if (playerHealth == null)
        {
            Debug.LogError("❌ No encontré HealthSystem");
            return;
        }

        Debug.LogError("✅ HealthSystem encontrado");

        Debug.LogError("⚠️ Antes de playerHealth.HealthChanged");
        playerHealth.HealthChanged += UpdateHealthUI;
        Debug.LogError("✅ HealthChanged asignado");

        Debug.LogError("⚠️ Antes de CreateHearts()");
        CreateHearts();
        Debug.LogError("✅ CreateHearts() completado");

        Debug.LogError($"✅ {heartImages.Length} corazones creados");

        Debug.LogError($"⚠️ Verificando corazones después de CreateHearts:");
        foreach (Transform child in heartsContainer)
        {
            Debug.LogError($"  - {child.name}: activo={child.gameObject.activeSelf}");
        }
    }

    void CreateHearts()
    {
        int heartCount = (int)playerHealth.GetMaxHealth();
        Debug.LogError($"CreateHearts: Creando {heartCount} corazones");
        Debug.LogError($"CreateHearts: Hearts Container = {heartsContainer}");
        Debug.LogError($"CreateHearts: Hearts Container activo = {heartsContainer.gameObject.activeSelf}");

        // Limpia corazones anteriores
        foreach (Transform child in heartsContainer)
        {
            Destroy(child.gameObject);
        }

        heartImages = new Image[heartCount];

        for (int i = 0; i < heartCount; i++)
        {
            GameObject heartGO = new GameObject($"Heart_{i}");
            heartGO.transform.SetParent(heartsContainer);
            heartGO.transform.localScale = Vector3.one;

            Image heartImage = heartGO.AddComponent<Image>();
            heartImage.sprite = heartFull;
            heartImage.color = Color.white;

            RectTransform rect = heartGO.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(i * spacing, 0);
            rect.sizeDelta = new Vector2(32, 32);

            heartGO.SetActive(true);  // ← DENTRO del loop
            Debug.LogError($"❤️ Heart {i} activo: {heartGO.activeSelf}");

            heartImages[i] = heartImage;
        }

        Debug.LogError($"CreateHearts: Terminado. {heartImages.Length} corazones en array");
    }

    void UpdateHealthUI(float currentHealth, float maxHealth)
    {
        if (heartImages.Length != (int)maxHealth)
        {
            CreateHearts();
        }

        for (int i = 0; i < heartImages.Length; i++)
        {
            float healthValue = i + 1;

            if (currentHealth >= healthValue)
            {
                heartImages[i].sprite = heartFull;
            }
            else if (currentHealth > i && currentHealth < healthValue)
            {
                heartImages[i].sprite = heartHalf;
            }
            else
            {
                heartImages[i].sprite = heartEmpty;
            }
        }
    }

    void OnDestroy()
    {
        if (playerHealth != null)
        {
            playerHealth.HealthChanged -= UpdateHealthUI;
        }
    }
}