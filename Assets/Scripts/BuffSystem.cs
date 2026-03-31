using UnityEngine;
using System.Collections.Generic;

public class BuffSystem : MonoBehaviour
{
    [System.Serializable]
    public class Buff
    {
        public string buffName;
        public float duration;
        public float damageMultiplier = 1f;
        public float speedMultiplier = 1f;
        public float defenseMultiplier = 1f;

        private float remainingTime;

        public Buff(string name, float dur, float dmg = 1f, float spd = 1f, float def = 1f)
        {
            buffName = name;
            duration = dur;
            damageMultiplier = dmg;
            speedMultiplier = spd;
            defenseMultiplier = def;
            remainingTime = dur;
        }

        public void Update()
        {
            remainingTime -= Time.deltaTime;
        }

        public bool IsActive() => remainingTime > 0f;
        public float GetRemainingTime() => remainingTime;
    }

    private List<Buff> activeBuffs = new List<Buff>();
    private float currentDamageMultiplier = 1f;
    private float currentSpeedMultiplier = 1f;
    private float currentDefenseMultiplier = 1f;

    void Update()
    {
        UpdateBuffs();
    }

    /// <summary>
    /// Actualiza los buffs activos
    /// </summary>
    void UpdateBuffs()
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--)
        {
            activeBuffs[i].Update();

            if (!activeBuffs[i].IsActive())
            {
                Debug.Log($"⏰ Buff '{activeBuffs[i].buffName}' expiró");
                activeBuffs.RemoveAt(i);
                RecalculateMultipliers();
            }
        }
    }

    /// <summary>
    /// Añade un buff
    /// </summary>
    public void AddBuff(string name, float duration, float dmgMult = 1f, float spdMult = 1f, float defMult = 1f)
    {
        Buff newBuff = new Buff(name, duration, dmgMult, spdMult, defMult);
        activeBuffs.Add(newBuff);
        RecalculateMultipliers();

        Debug.Log($"✨ Buff '{name}' aplicado por {duration}s");
    }

    /// <summary>
    /// Recalcula los multiplicadores
    /// </summary>
    void RecalculateMultipliers()
    {
        currentDamageMultiplier = 1f;
        currentSpeedMultiplier = 1f;
        currentDefenseMultiplier = 1f;

        foreach (Buff buff in activeBuffs)
        {
            currentDamageMultiplier *= buff.damageMultiplier;
            currentSpeedMultiplier *= buff.speedMultiplier;
            currentDefenseMultiplier *= buff.defenseMultiplier;
        }
    }

    public float GetDamageMultiplier() => currentDamageMultiplier;
    public float GetSpeedMultiplier() => currentSpeedMultiplier;
    public float GetDefenseMultiplier() => currentDefenseMultiplier;
    public List<Buff> GetActiveBuffs() => activeBuffs;
}