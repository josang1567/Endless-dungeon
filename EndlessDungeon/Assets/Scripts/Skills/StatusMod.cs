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
            case StatusModType.ATTACK_MOD:
               if((modedStats.attack+=this.amount)<modedStats.Minattack){
                   break;
               }else{
                   modedStats.attack += this.amount;
                break;
               }
                
            case StatusModType.DEFFENSE_MOD:
             if((modedStats.deffense+=this.amount)<modedStats.Mindeffense){
                   break;
               }else{
                   modedStats.deffense += this.amount;
                break;
               }
                
        }

        return modedStats;
    }
}
