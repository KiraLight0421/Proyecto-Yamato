using UnityEngine;

public class Consumable : MonoBehaviour
{
    [SerializeField] protected ItemData consumableData;

    protected virtual void Start()
    {
        if (consumableData == null)
        {
            Debug.LogWarning("⚠️ Consumable sin ItemData asignado");
        }
    }

    /// <summary>
    /// Usa el consumible
    /// </summary>
    public virtual void Use(HealthSystem playerHealth)
    {
        if (playerHealth == null || consumableData == null)
            return;

        switch (consumableData.consumableType)
        {
            case ConsumableType.HealthPotion:
                playerHealth.Heal(consumableData.consumableAmount);
                Debug.Log($"🧪 ¡Bebiste {consumableData.itemName}! +{consumableData.consumableAmount} vida");
                break;

            case ConsumableType.MaxHealthPotion:
                playerHealth.IncreaseMaxHealth(consumableData.consumableAmount);
                Debug.Log($"🧪 ¡Bebiste {consumableData.itemName}! +{consumableData.consumableAmount} vida máxima");
                break;
        }
    }

    /// <summary>
    /// Asigna datos del consumible
    /// </summary>
    public void SetConsumableData(ItemData data)
    {
        consumableData = data;
    }

    public ItemData GetConsumableData() => consumableData;
    public float GetAmount() => consumableData != null ? consumableData.consumableAmount : 0f;
}