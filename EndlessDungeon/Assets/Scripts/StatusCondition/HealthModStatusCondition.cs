using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class HealthModStatusCondition : StatusCondition
{
    [Header("Health mod")]
    public float percentage;


     IEnumerator pausa()
    {
        yield return new WaitForSeconds(1.2f);
    }
    //Cuando se aplica reduce o aumenta la vida del afectado
    public override bool OnApply()
    {
        Stats rStats = receiver.GetCurrentStats();

            //Se evita que el objetivo pierda toda la vida porque sino se buggea el juego al intentar ejecutar el turno del peleador
        if (rStats.health - Mathf.Abs(rStats.maxHealth * this.percentage) <= 0.9f)
        {
            
            this.messages.Enqueue(this.receiver.idName + " no puede perder mas vida");
            StartCoroutine(pausa());

        }
        else if (rStats.health - (rStats.maxHealth * this.percentage) != 0 || rStats.maxHealth - (rStats.maxHealth * this.percentage) > 0)
        {
           
            this.receiver.ModifyHealth(rStats.maxHealth * this.percentage);
        }


        this.messages.Enqueue(this.applyMessage.Replace("{receiver}", this.receiver.idName));

        return true;
    }



    public override bool BlocksTurn() => false;
}
