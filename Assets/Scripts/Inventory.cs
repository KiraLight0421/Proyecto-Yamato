using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int maxInventorySlots = 20;

    // 4 Slots equipados
    private ItemData equippedMelee;      // Espada, Lanza, Mazo
    private ItemData equippedRanged;     // Vara Mágica
    private ItemData equippedShield;     // Escudo
    private ItemData equippedConsumable; // Poción, comida

    // Inventario expandido
    private List<ItemData> inventory = new List<ItemData>();

    // Eventos
    public delegate void OnInventoryChanged(ItemData melee, ItemData ranged, ItemData shield, ItemData consumable);
    public event OnInventoryChanged InventoryChanged;

    public delegate void OnInventoryUpdated();
    public event OnInventoryUpdated InventoryUpdated;

    void Start()
    {
        Debug.Log("✅ Inventario creado");
    }

    /// <summary>
    /// Añade item al inventario
    /// </summary>
    public bool AddItem(ItemData item)
    {
        if (inventory.Count >= maxInventorySlots)
        {
            Debug.LogWarning("❌ Inventario lleno");
            return false;
        }

        inventory.Add(item);
        Debug.Log($"✅ {item.itemName} añadido al inventario");
        InventoryUpdated?.Invoke();
        return true;
    }

    /// <summary>
    /// Equipa un arma melee
    /// </summary>
    public void EquipMelee(ItemData weapon)
    {
        if (weapon.itemType == ItemType.Weapon || weapon.itemType == ItemType.Ranged)
        {
            equippedMelee = weapon;
            inventory.Remove(weapon);
            Debug.Log($"⚔️ {weapon.itemName} equipado en slot Melee");
            InventoryChanged?.Invoke(equippedMelee, equippedRanged, equippedShield, equippedConsumable);
            InventoryUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Equipa un arma a distancia
    /// </summary>
    public void EquipRanged(ItemData weapon)
    {
        if (weapon.itemType == ItemType.Ranged)
        {
            equippedRanged = weapon;
            inventory.Remove(weapon);
            Debug.Log($"🪄 {weapon.itemName} equipado en slot Distancia");
            InventoryChanged?.Invoke(equippedMelee, equippedRanged, equippedShield, equippedConsumable);
            InventoryUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Equipa un escudo
    /// </summary>
    public void EquipShield(ItemData shield)
    {
        if (shield.itemType == ItemType.Shield)
        {
            equippedShield = shield;
            inventory.Remove(shield);
            Debug.Log($"🛡️ {shield.itemName} equipado en slot Defensa");
            InventoryChanged?.Invoke(equippedMelee, equippedRanged, equippedShield, equippedConsumable);
            InventoryUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Equipa un consumible
    /// </summary>
    public void EquipConsumable(ItemData consumable)
    {
        if (consumable.itemType == ItemType.Consumable)
        {
            equippedConsumable = consumable;
            inventory.Remove(consumable);
            Debug.Log($"🧪 {consumable.itemName} equipado en slot Consumible");
            InventoryChanged?.Invoke(equippedMelee, equippedRanged, equippedShield, equippedConsumable);
            InventoryUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Desequipa item de slot y lo devuelve al inventario
    /// </summary>
    public void UnequipSlot(string slotType)
    {
        ItemData unequipped = null;

        switch (slotType.ToLower())
        {
            case "melee":
                unequipped = equippedMelee;
                equippedMelee = null;
                break;
            case "ranged":
                unequipped = equippedRanged;
                equippedRanged = null;
                break;
            case "shield":
                unequipped = equippedShield;
                equippedShield = null;
                break;
            case "consumable":
                unequipped = equippedConsumable;
                equippedConsumable = null;
                break;
        }

        if (unequipped != null)
        {
            inventory.Add(unequipped);
            Debug.Log($"🔄 {unequipped.itemName} desequipado");
            InventoryChanged?.Invoke(equippedMelee, equippedRanged, equippedShield, equippedConsumable);
            InventoryUpdated?.Invoke();
        }
    }

    /// <summary>
    /// Usa consumible equipado
    /// </summary>
    public void UseConsumable(HealthSystem playerHealth)
    {
        if (equippedConsumable == null)
        {
            Debug.LogWarning("❌ No hay consumible equipado");
            return;
        }

        Consumable consumable = gameObject.AddComponent<Consumable>();
        consumable.SetConsumableData(equippedConsumable);
        consumable.Use(playerHealth);

        inventory.Remove(equippedConsumable);
        equippedConsumable = null;

        InventoryChanged?.Invoke(equippedMelee, equippedRanged, equippedShield, equippedConsumable);
        InventoryUpdated?.Invoke();
    }

    // GETTERS
    public ItemData GetEquippedMelee() => equippedMelee;
    public ItemData GetEquippedRanged() => equippedRanged;
    public ItemData GetEquippedShield() => equippedShield;
    public ItemData GetEquippedConsumable() => equippedConsumable;
    public List<ItemData> GetInventory() => inventory;
    public int GetInventoryCount() => inventory.Count;
}