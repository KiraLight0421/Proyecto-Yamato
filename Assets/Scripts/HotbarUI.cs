using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarUI : MonoBehaviour
{
    [SerializeField] private Image meleSlot;
    [SerializeField] private Image rangedSlot;
    [SerializeField] private Image shieldSlot;
    [SerializeField] private Image consumableSlot;

    [SerializeField] private TextMeshProUGUI meleeName;
    [SerializeField] private TextMeshProUGUI rangedName;
    [SerializeField] private TextMeshProUGUI shieldName;
    [SerializeField] private TextMeshProUGUI consumableName;

    private Inventory inventory;
    private GameManager gameManager;

    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
        gameManager = GameManager.GetInstance();

        if (inventory == null)
        {
            Debug.LogWarning("❌ Inventory no encontrado");
            return;
        }

        RefreshHotbar();
    }

    /// <summary>
    /// Actualiza la hotbar
    /// </summary>
    public void RefreshHotbar()
    {
        if (inventory == null)
            return;

        // Melee
        ItemData melee = inventory.GetEquippedMelee();
        UpdateSlot(meleSlot, meleeName, melee, "⚔️");

        // Ranged
        ItemData ranged = inventory.GetEquippedRanged();
        UpdateSlot(rangedSlot, rangedName, ranged, "🪄");

        // Shield
        ItemData shield = inventory.GetEquippedShield();
        UpdateSlot(shieldSlot, shieldName, shield, "🛡️");

        // Consumable
        ItemData consumable = inventory.GetEquippedConsumable();
        UpdateSlot(consumableSlot, consumableName, consumable, "🧪");
    }

    /// <summary>
    /// Actualiza un slot de la hotbar
    /// </summary>
    void UpdateSlot(Image slotImage, TextMeshProUGUI slotText, ItemData item, string emoji)
    {
        if (item != null && item.itemSprite != null)
        {
            slotImage.sprite = item.itemSprite;
            slotImage.color = Color.white;

            if (slotText != null)
            {
                slotText.text = $"{emoji} {item.itemName}";
            }
        }
        else
        {
            slotImage.sprite = null;
            slotImage.color = new Color(1, 1, 1, 0.3f);

            if (slotText != null)
            {
                slotText.text = $"{emoji} Vacío";
            }
        }
    }

    public void OnHotbarChanged()
    {
        RefreshHotbar();
    }
}