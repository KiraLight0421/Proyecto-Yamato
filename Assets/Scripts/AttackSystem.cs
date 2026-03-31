using UnityEngine;
using UnityEngine.InputSystem;  // ← CAMBIA AQUÍ

public class AttackSystem : MonoBehaviour
{
    [SerializeField] private float attackDamage = 1f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private float attackCooldown = 0.5f;
    [SerializeField] private Transform attackPoint;

    private float lastAttackTime = -1f;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (attackPoint == null)
        {
            GameObject ap = new GameObject("AttackPoint");
            ap.transform.SetParent(transform);
            ap.transform.localPosition = Vector3.zero;
            attackPoint = ap.transform;
        }
    }

    void Update()
    {
        HandleAttackInput();
    }

    /// <summary>
    /// Detecta entrada de ataque (NEW INPUT SYSTEM)
    /// </summary>
    void HandleAttackInput()
    {
        if (Keyboard.current.mKey.wasPressedThisFrame)  // ← M para atacar
        {
            if (Time.time - lastAttackTime >= attackCooldown)
            {
                Attack();
                lastAttackTime = Time.time;
            }
        }
    }

    void Attack()
    {
        Debug.Log("⚔️ ¡ATAQUE!");

        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange);

        foreach (Collider2D enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();
            if (enemyScript != null)
            {
                enemyScript.TakeDamage(attackDamage, attackPoint.position);
                Debug.Log($"💥 ¡Golpeaste un enemigo!");
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }
}