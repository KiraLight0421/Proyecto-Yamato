using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 5f;
    [SerializeField] private float projectileDamage = 5f;

    private Inventory inventory;

    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();
    }

    /// <summary>
    /// Dispara un proyectil
    /// </summary>
    public void FireProjectile(Vector3 startPosition, Vector3 direction)
    {
        if (projectilePrefab == null)
        {
            Debug.LogWarning("⚠️ Projectile prefab no asignado");
            return;
        }

        GameObject projectileGO = Instantiate(projectilePrefab, startPosition, Quaternion.identity);
        Rigidbody2D rb = projectileGO.GetComponent<Rigidbody2D>();

        if (rb != null)
        {
            rb.linearVelocity = direction.normalized * projectileSpeed;
        }

        // Asigna daño al proyectil
        Projectile projectile = projectileGO.GetComponent<Projectile>();
        if (projectile != null)
        {
            projectile.SetDamage(GetRangedDamage());
        }

        Destroy(projectileGO, projectileLifetime);
        Debug.Log($"🎯 Proyectil disparado en dirección {direction}");
    }

    /// <summary>
    /// Obtiene daño del arma equipada
    /// </summary>
    public float GetRangedDamage()
    {
        if (inventory == null)
            return projectileDamage;

        ItemData ranged = inventory.GetEquippedRanged();
        if (ranged != null)
        {
            return ranged.weaponDamage;
        }

        return projectileDamage;
    }

    public static ProjectileManager GetInstance()
    {
        return FindFirstObjectByType<ProjectileManager>();
    }
}