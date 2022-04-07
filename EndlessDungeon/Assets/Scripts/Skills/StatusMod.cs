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
                modedStats.attack += this.amount;
                break;
            case StatusModType.DEFFENSE_MOD:
                modedStats.deffense += this.amount;
                if(modedStats.deffense<modedStats.Mindeffense){
                    modedStats.deffense-=this.amount;
                }
                break;
        }
      
      
      
       /* switch (this.type)
        {
            case StatusModType.ATTACK_MOD:
               if((modedStats.attack+=this.amount)<modedStats.Minattack){
                   break;
               }else{
                   modedStats.attack += this.amount;
                break;
               }
                
            case StatusModType.DEFFENSE_MOD:
            //Debug.Log("Defensa a quitar"+modedStats.deffense+=this.amount);
            float modificado=modedStats.deffense;
            modificado+=this.amount;
             if(modificado<modedStats.Mindeffense){
                 Debug.Log("no se puede bajar mas la defensa");
                   
               }else{
                   Debug.Log("Se baja defensa");
                   modedStats.deffense += this.amount;
                
               }
               break;
                
        }*/

        return modedStats;
    }
}
