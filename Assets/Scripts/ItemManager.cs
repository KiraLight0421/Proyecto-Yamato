using UnityEngine;
using System.Collections.Generic;

public class ItemManager : MonoBehaviour
{
    private static ItemManager instance;

    [SerializeField] private ItemData[] allItems;

    private Dictionary<string, ItemData> itemDatabase = new Dictionary<string, ItemData>();

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
        LoadItemDatabase();
    }

    /// <summary>
    /// Carga la base de datos de items
    /// </summary>
    void LoadItemDatabase()
    {
        if (allItems == null || allItems.Length == 0)
        {
            Debug.LogWarning("⚠️ No hay items en la base de datos");
            return;
        }

        itemDatabase.Clear();

        foreach (ItemData item in allItems)
        {
            if (!itemDatabase.ContainsKey(item.itemName))
            {
                itemDatabase.Add(item.itemName, item);
                Debug.Log($"✅ {item.itemName} cargado en la BD");
            }
        }

        Debug.Log($"✅ Base de datos de items cargada: {itemDatabase.Count} items");
    }

    /// <summary>
    /// Obtiene un item por nombre
    /// </summary>
    public ItemData GetItem(string itemName)
    {
        if (itemDatabase.ContainsKey(itemName))
        {
            return itemDatabase[itemName];
        }

        Debug.LogWarning($"❌ Item '{itemName}' no encontrado en la BD");
        return null;
    }

    /// <summary>
    /// Obtiene todos los items de un tipo
    /// </summary>
    public ItemData[] GetItemsByType(ItemType type)
    {
        List<ItemData> items = new List<ItemData>();

        foreach (ItemData item in allItems)
        {
            if (item.itemType == type)
            {
                items.Add(item);
            }
        }

        return items.ToArray();
    }

    /// <summary>
    /// Obtiene todas las armas
    /// </summary>
    public ItemData[] GetWeapons()
    {
        List<ItemData> weapons = new List<ItemData>();

        foreach (ItemData item in allItems)
        {
            if (item.itemType == ItemType.Weapon || item.itemType == ItemType.Ranged)
            {
                weapons.Add(item);
            }
        }

        return weapons.ToArray();
    }

    /// <summary>
    /// Obtiene todos los escudos
    /// </summary>
    public ItemData[] GetShields()
    {
        return GetItemsByType(ItemType.Shield);
    }

    /// <summary>
    /// Obtiene todos los consumibles
    /// </summary>
    public ItemData[] GetConsumables()
    {
        return GetItemsByType(ItemType.Consumable);
    }

    public ItemData[] GetAllItems() => allItems;
    public static ItemManager GetInstance() => instance;
}