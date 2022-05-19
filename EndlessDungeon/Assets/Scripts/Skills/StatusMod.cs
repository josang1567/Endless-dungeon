using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusModType
{
    ATTACK_MOD,
    DEFFENSE_MOD
}
public class StatusMod : MonoBehaviour
{
    public StatusModType type;
    public float amount;

    public Stats Apply(Stats stats)
    {
        Stats modedStats = stats.Clone();

        switch (this.type)
        {
            //aumenta o baja el ataque del peleador objetivo
            case StatusModType.ATTACK_MOD:
                modedStats.attack += this.amount;
                break;
            //aumenta o baja la defensa del peleador objetivo
            case StatusModType.DEFFENSE_MOD:

                modedStats.deffense += this.amount;
                break;
        }

        return modedStats;
    }
}
