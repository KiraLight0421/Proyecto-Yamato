using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private Image slotImage;
    private ItemData itemData;
    private string slotType; // "Melee", "Ranged", "Shield", "Consumable", "Inventory"
    private Inventory inventory;
    private Button slotButton;

    void Awake()
    {
        slotImage = GetComponent<Image>();
        slotButton = GetComponent<Button>();

        if (slotButton == null)
        {
            slotButton = gameObject.AddComponent<Button>();
        }

        slotButton.onClick.AddListener(OnSlotClicked);
    }

    /// <summary>
    /// Configura el tipo de slot
    /// </summary>
    public void SetSlotType(string type)
    {
        slotType = type;
    }

    /// <summary>
    /// Configura la referencia del inventario
    /// </summary>
    public void SetInventory(Inventory inv)
    {
        inventory = inv;
    }

    /// <summary>
    /// Asigna un item al slot
    /// </summary>
    public void SetItem(ItemData item)
    {
        itemData = item;
        if (item != null && item.itemSprite != null)
        {
            slotImage.sprite = item.itemSprite;
            slotImage.color = Color.white;
        }
    }

    /// <summary>
    /// Vacía el slot
    /// </summary>
    public void SetEmpty()
    {
        itemData = null;
        slotImage.sprite = null;
        slotImage.color = new Color(1, 1, 1, 0.5f);
    }

    /// <summary>
    /// Se ejecuta al hacer clic en el slot
    /// </summary>
    void OnSlotClicked()
    {
        if (itemData == null || inventory == null)
            return;

        Debug.Log($"🖱️ Slot clickeado: {slotType} - {itemData.itemName}");

        if (slotType == "Inventory")
        {
            // Equipar desde inventario
            EquipFromInventory(itemData);
        }
        else
        {
            // Desequipar desde hotbar
            inventory.UnequipSlot(slotType);
        }
    }

    /// <summary>
    /// Equipa un item desde el inventario
    /// </summary>
    void EquipFromInventory(ItemData item)
    {
        switch (item.itemType)
        {
            case ItemType.Weapon:
                inventory.EquipMelee(item);
                Debug.Log($"⚔️ {item.itemName} equipado como Melee");
                break;

            case ItemType.Ranged:
                inventory.EquipRanged(item);
                Debug.Log($"🪄 {item.itemName} equipado como Ranged");
                break;

            case ItemType.Shield:
                inventory.EquipShield(item);
                Debug.Log($"🛡️ {item.itemName} equipado como Shield");
                break;

            case ItemType.Consumable:
                inventory.EquipConsumable(item);
                Debug.Log($"🧪 {item.itemName} equipado como Consumable");
                break;
        }
    }

    public ItemData GetItem() => itemData;
}