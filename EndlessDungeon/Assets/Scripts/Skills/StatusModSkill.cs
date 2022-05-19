using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusModSkill : Skill
{

    [Header("Status mod skill")]
    public string message;
    public string message2;
    protected StatusMod mod;
//Esta funcion se encarga de añadir efectos de estado a los objetivos
    protected override void OnRun(Fighter receiver)
    {
        if (this.mod == null)
        {
            this.mod = this.GetComponent<StatusMod>();
        }


        if (receiver.GetCurrentStats().deffense == 10 || receiver.GetCurrentStats().attack > 130)
        {
            this.messages.Enqueue(this.message2.Replace("{receiver}", receiver.idName));
        }
        else
        {
            this.messages.Enqueue(this.message.Replace("{receiver}", receiver.idName));
        }

        receiver.statusMods.Add(this.mod);


    }
}
