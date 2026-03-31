using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected ItemData weaponData;
    [SerializeField] protected Transform attackPoint;

    protected float lastAttackTime = -1f;
    protected Animator animator;
    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        if (attackPoint == null)
        {
            GameObject ap = new GameObject("AttackPoint");
            ap.transform.SetParent(transform);
            ap.transform.localPosition = Vector3.zero;
            attackPoint = ap.transform;
        }

        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// Intenta atacar
    /// </summary>
    public virtual bool TryAttack()
    {
        if (weaponData == null) return false;

        if (Time.time - lastAttackTime >= weaponData.weaponCooldown)
        {
            Attack();
            lastAttackTime = Time.time;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Realiza el ataque
    /// </summary>
    protected virtual void Attack()
    {
        if (weaponData == null) return;

        Debug.Log($"⚔️ ¡ATAQUE CON {weaponData.itemName}!");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, weaponData.weaponRange);

        foreach (Collider2D enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(weaponData.weaponDamage, attackPoint.position);
                Debug.Log($"💥 ¡{weaponData.itemName} golpeó un enemigo por {weaponData.weaponDamage} de daño!");
            }
        }
    }

    /// <summary>
    /// Asigna datos del arma
    /// </summary>
    public void SetWeaponData(ItemData data)
    {
        weaponData = data;
    }

    public ItemData GetWeaponData() => weaponData;
    public float GetDamage() => weaponData != null ? weaponData.weaponDamage : 0f;
    public float GetRange() => weaponData != null ? weaponData.weaponRange : 0f;
    public float GetCooldown() => weaponData != null ? weaponData.weaponCooldown : 0f;

    protected virtual void OnDrawGizmosSelected()
    {
        if (attackPoint != null && weaponData != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, weaponData.weaponRange);
        }
    }
}