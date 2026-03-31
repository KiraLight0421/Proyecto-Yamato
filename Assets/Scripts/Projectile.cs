using UnityEngine;

public class Projectile : MonoBehaviour
{
    private float damage = 5f;
    private bool hasHit = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit)
            return;

        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(damage, transform.position);
            hasHit = true;

            Debug.Log($"🎯 Proyectil golpeó a {enemy.gameObject.name}");

            // Efecto de impacto
            ParticleEffectManager particleManager = FindFirstObjectByType<ParticleEffectManager>();
            if (particleManager != null)
            {
                particleManager.PlayHitEffect(transform.position);
            }

            Destroy(gameObject);
        }
    }

    public void SetDamage(float dmg)
    {
        damage = dmg;
    }

    public float GetDamage() => damage;
}