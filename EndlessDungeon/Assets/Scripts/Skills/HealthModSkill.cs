using UnityEngine;



public enum HealthModType
{
    STAT_BASED, FIXED, PERCENTAGE
}

public class HealthModSkill : Skill
{
    [Header("Health Mod")]
    public float amount;

    public HealthModType modType;

    [Range(0f, 1f)]
    public float critChance = 0;

    protected override void OnRun(Fighter receiver)
    {
        float amount = this.GetModification(receiver);

        float dice = Random.Range(0f, 1f);
        //En caso de que el ataque sea critico se duplica el daño
        if (dice <= this.critChance)
        {
            amount *= 2f;
            this.messages.Enqueue("Golpe Critico!");
        }

        receiver.ModifyHealth(amount);
    }


    public float GetModification(Fighter receiver)
    {
        //Segun el metodo de daño se aplica de distinta forma
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