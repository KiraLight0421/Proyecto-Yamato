using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] protected ItemData shieldData;

    protected float lastDefenseTime = -1f;
    protected bool isDefending = false;

    protected virtual void Start()
    {
        if (shieldData == null)
        {
            Debug.LogWarning("⚠️ Shield sin ItemData asignado");
        }
    }

    /// <summary>
    /// Activa la defensa
    /// </summary>
    public virtual void StartDefense()
    {
        isDefending = true;
        Debug.Log($"🛡️ ¡DEFENSA CON {shieldData.itemName}!");
    }

    /// <summary>
    /// Desactiva la defensa
    /// </summary>
    public virtual void StopDefense()
    {
        isDefending = false;
    }

    /// <summary>
    /// Calcula daño reducido
    /// </summary>
    public float ReduceDamage(float incomingDamage)
    {
        if (!isDefending || shieldData == null)
            return incomingDamage;

        float reducedDamage = incomingDamage * (1f - shieldData.damageReduction);
        Debug.Log($"🛡️ Daño reducido: {incomingDamage} → {reducedDamage}");
        return reducedDamage;
    }

    /// <summary>
    /// Asigna datos del escudo
    /// </summary>
    public void SetShieldData(ItemData data)
    {
        shieldData = data;
    }

    public ItemData GetShieldData() => shieldData;
    public float GetDamageReduction() => shieldData != null ? shieldData.damageReduction : 0f;
    public bool IsDefending() => isDefending;
}