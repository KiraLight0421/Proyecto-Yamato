using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private Button slotButton;
    [SerializeField] private Image selectedHighlight;

    private ItemData itemData;
    private int slotIndex;
    private Inventory inventory;
    private bool isSelected = false;

    void Awake()
    {
        itemImage = GetComponent<Image>();

        if (slotButton == null)
        {
            slotButton = GetComponent<Button>();
        }

        if (slotButton != null)
        {
            slotButton.onClick.AddListener(OnSlotClicked);
        }
    }

    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    /// <summary>
    /// Asigna el índice del slot
    /// </summary>
    public void SetSlotIndex(int index)
    {
        slotIndex = index;
    }

    /// <summary>
    /// Asigna un item al slot
    /// </summary>
    public void SetItem(ItemData item)
    {
        itemData = item;

        if (item != null && item.itemSprite != null)
        {
            itemImage.sprite = item.itemSprite;
            itemImage.color = Color.white;

            if (itemNameText != null)
            {
                itemNameText.text = item.itemName;
            }

            if (quantityText != null)
            {
                quantityText.text = "1";
            }
        }
        else
        {
            Clear();
        }
    }

    /// <summary>
    /// Vacía el slot
    /// </summary>
    public void Clear()
    {
        itemData = null;
        itemImage.sprite = null;
        itemImage.color = new Color(1, 1, 1, 0.3f);

        if (itemNameText != null)
        {
            itemNameText.text = "Vacío";
        }

        if (quantityText != null)
        {
            quantityText.text = "";
        }
    }

    /// <summary>
    /// Se ejecuta al clickear el slot
    /// </summary>
    void OnSlotClicked()
    {
        if (itemData == null || inventory == null)
            return;

        Debug.Log($"🖱️ Slot {slotIndex} clickeado: {itemData.itemName}");

        // Equipar según el tipo
        switch (itemData.itemType)
        {
            case ItemType.Weapon:
                inventory.EquipMelee(itemData);
                Debug.Log($"⚔️ {itemData.itemName} equipado como Melee");
                break;

            case ItemType.Ranged:
                inventory.EquipRanged(itemData);
                Debug.Log($"🪄 {itemData.itemName} equipado como Ranged");
                break;

            case ItemType.Shield:
                inventory.EquipShield(itemData);
                Debug.Log($"🛡️ {itemData.itemName} equipado como Shield");
                break;

            case ItemType.Consumable:
                inventory.EquipConsumable(itemData);
                Debug.Log($"🧪 {itemData.itemName} equipado como Consumable");
                break;
        }

        Select();
    }

    /// <summary>
    /// Marca el slot como seleccionado
    /// </summary>
    public void Select()
    {
        isSelected = true;

        if (selectedHighlight != null)
        {
            selectedHighlight.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// Deselecciona el slot
    /// </summary>
    public void Deselect()
    {
        isSelected = false;

        if (selectedHighlight != null)
        {
            selectedHighlight.gameObject.SetActive(false);
        }
    }

    public ItemData GetItem() => itemData;
    public bool IsSelected() => isSelected;
    public int GetSlotIndex() => slotIndex;
}