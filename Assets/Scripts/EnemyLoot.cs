using UnityEngine;

public class EnemyLoot : MonoBehaviour
{
    [SerializeField] private ItemData[] lootTable;
    [SerializeField] private float dropChance = 0.5f;  // 50%
    [SerializeField] private GameObject itemPickupPrefab;

    /// <summary>
    /// Dropea item al morir
    /// </summary>
    public void DropLoot(Vector3 dropPosition)
    {
        // 50% de probabilidad de dropear
        if (Random.value > dropChance)
        {
            Debug.Log("💨 Enemigo no dropeó nada");
            return;
        }

        if (lootTable == null || lootTable.Length == 0)
        {
            Debug.LogWarning("⚠️ Tabla de loot vacía");
            return;
        }

        // Selecciona item random de la tabla
        ItemData randomItem = lootTable[Random.Range(0, lootTable.Length)];

        // Crea el pickup en el suelo
        if (itemPickupPrefab != null)
        {
            GameObject pickupGO = Instantiate(itemPickupPrefab, dropPosition, Quaternion.identity);
            ItemPickup pickup = pickupGO.GetComponent<ItemPickup>();
            if (pickup != null)
            {
                pickup.SetItemData(randomItem);
            }
        }
        else
        {
            // Si no hay prefab, crea uno genérico
            GameObject pickupGO = new GameObject("ItemPickup_" + randomItem.itemName);
            pickupGO.transform.position = dropPosition;

            SpriteRenderer sr = pickupGO.AddComponent<SpriteRenderer>();
            sr.sprite = randomItem.itemSprite;

            ItemPickup pickup = pickupGO.AddComponent<ItemPickup>();
            pickup.SetItemData(randomItem);
        }

        Debug.Log($"🎁 ¡{randomItem.itemName} dropeado!");
    }

    /// <summary>
    /// Configura la tabla de loot
    /// </summary>
    public void SetLootTable(ItemData[] items)
    {
        lootTable = items;
    }
}