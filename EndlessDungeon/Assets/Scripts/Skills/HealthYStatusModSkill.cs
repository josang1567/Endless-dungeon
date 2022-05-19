using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HealthModTypeB
{
    STAT_BASED, FIXED, PERCENTAGE
}

public class HealthYStatusModSkill : Skill
{
    [Header("Health Mod")]

    public float amount;

    public HealthModType modType;

    [Range(0f, 1f)]
    public float critChance = 0;

    [Header("status Mod")]
    public string message;
    public string message2;
    protected StatusMod mod;

//Funciona igual que los scripts HealthModSkill y StatusMod pero combinando sus efectos
    protected override void OnRun(Fighter receiver)
    {
        float amount = this.GetModification(receiver);

        float dice = Random.Range(0f, 1f);

        if (dice <= this.critChance)
        {
            amount *= 2f;
            this.messages.Enqueue("Golpe Critico!");
        }

        receiver.ModifyHealth(amount);

        if (this.mod == null)
        {
            this.mod = this.GetComponent<StatusMod>();
        }

        if (receiver.GetCurrentStats().deffense == 10)
        {
            this.messages.Enqueue(this.message2.Replace("{receiver}", receiver.idName));
        }
        else
        {
            this.messages.Enqueue(this.message.Replace("{receiver}", receiver.idName));
        }


        receiver.statusMods.Add(this.mod);

    }

    public float GetModification(Fighter receiver)
    {
        switch (this.modType)
        {
            case HealthModType.STAT_BASED:
                Stats emitterStats = this.emitter.GetCurrentStats();
                Stats receiverStats = receiver.GetCurrentStats();

                float rawDamage = (((2 * emitterStats.level) / 5) + 2) * this.amount * (emitterStats.attack / receiverStats.deffense);

                return (rawDamage / 50) + 2;
            case HealthModType.FIXED:
                return this.amount;
            case HealthModType.PERCENTAGE:
                Stats rStats = receiver.GetCurrentStats();

                return rStats.maxHealth * this.amount;
        }

        throw new System.InvalidOperationException("HealthModSkill::GetDamage. Unreachable!");
    }
}
