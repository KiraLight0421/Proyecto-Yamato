using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private Weapon equippedWeapon;
    private Shield equippedShield;
    private Inventory inventory;
    private HealthSystem healthSystem;
    private GameManager gameManager;

    void Start()
    {
        inventory = GetComponent<Inventory>();
        healthSystem = GetComponent<HealthSystem>();
        gameManager = GameManager.GetInstance();
    }

    void Update()
    {
        // Left Click para atacar
        if (Input.GetMouseButtonDown(0))
        {
            TryAttack();
        }

        // Right Click para defender
        if (Input.GetMouseButtonDown(1))
        {
            StartDefense();
        }

        if (Input.GetMouseButtonUp(1))
        {
            StopDefense();
        }

        // E para usar consumible
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseConsumable();
        }
    }

    /// <summary>
    /// Intenta atacar con arma equipada
    /// </summary>
    void TryAttack()
    {
        if (inventory == null)
            return;

        ItemData meleeWeapon = inventory.GetEquippedMelee();

        if (meleeWeapon == null)
        {
            Debug.LogWarning("❌ No hay arma equipada");
            return;
        }

        // Crea instancia temporal del arma
        GameObject weaponGO = new GameObject("TempWeapon");
        weaponGO.transform.SetParent(transform);

        Weapon weapon = weaponGO.AddComponent<Weapon>();
        weapon.SetWeaponData(meleeWeapon);

        if (weapon.TryAttack())
        {
            Debug.Log($"⚔️ ¡Atacaste con {meleeWeapon.itemName}!");
            if (gameManager != null)
                gameManager.AddScore(1);
        }

        Destroy(weaponGO);
    }

    /// <summary>
    /// Inicia defensa
    /// </summary>
    void StartDefense()
    {
        if (inventory == null)
            return;

        ItemData shield = inventory.GetEquippedShield();

        if (shield == null)
        {
            Debug.LogWarning("❌ No hay escudo equipado");
            return;
        }

        GameObject shieldGO = new GameObject("TempShield");
        shieldGO.transform.SetParent(transform);

        Shield shieldComponent = shieldGO.AddComponent<Shield>();
        shieldComponent.SetShieldData(shield);
        shieldComponent.StartDefense();

        Debug.Log($"🛡️ ¡Defensa activada con {shield.itemName}!");
    }

    /// <summary>
    /// Termina defensa
    /// </summary>
    void StopDefense()
    {
        Shield shield = GetComponentInChildren<Shield>();
        if (shield != null)
        {
            shield.StopDefense();
            Debug.Log("🛡️ Defensa desactivada");
            Destroy(shield.gameObject);
        }
    }

    /// <summary>
    /// Usa consumible equipado
    /// </summary>
    void UseConsumable()
    {
        if (inventory == null || healthSystem == null)
            return;

        ItemData consumable = inventory.GetEquippedConsumable();

        if (consumable == null)
        {
            Debug.LogWarning("❌ No hay consumible equipado");
            return;
        }

        inventory.UseConsumable(healthSystem);

        if (gameManager != null)
            gameManager.AddScore(2);
    }

    public void TakeDamage(float damage, Vector3 damageSource)
    {
        if (healthSystem != null)
        {
            healthSystem.TakeDamage(damage, damageSource);
            healthSystem.SetInvulnerable(1f);
        }
    }
}