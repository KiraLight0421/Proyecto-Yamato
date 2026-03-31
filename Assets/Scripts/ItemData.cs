using UnityEngine;

public enum ItemType
{
    Weapon,      // Espada, Lanza, Mazo
    Ranged,      // Vara Mágica
    Shield,      // Escudo
    Consumable   // Pociones, comida
}

public enum WeaponType
{
    Sword,       // Espada
    Lance,       // Lanza
    Mace,        // Mazo
    MagicWand    // Vara Mágica
}

public enum ShieldType
{
    WoodShield,  // Escudo de madera
    IronShield   // Escudo de hierro
}

public enum ConsumableType
{
    HealthPotion,
    MaxHealthPotion
}

[CreateAssetMenu(fileName = "New Item", menuName = "Items/Item Data")]
public class ItemData : ScriptableObject
{
    public string itemName;
    public ItemType itemType;
    public Sprite itemSprite;
    public string description;

    // ARMAS
    public WeaponType weaponType;
    public float weaponDamage;
    public float weaponRange;
    public float weaponCooldown;
    public float attackSpeed = 1f;

    // ESCUDOS
    public ShieldType shieldType;
    public float damageReduction = 0.2f; // 20% reducción

    // CONSUMIBLES
    public ConsumableType consumableType;
    public float consumableAmount = 1f;

    // GENERAL
    public int quantity = 1;
    public int maxStackSize = 1;
    public bool isStackable = false;
}