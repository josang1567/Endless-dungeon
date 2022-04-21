using UnityEngine;

public class HealthModStatusCondition : StatusCondition
{
    [Header("Health mod")]
    public float percentage;

    public override bool OnApply()
    {
        Stats rStats = receiver.GetCurrentStats();

       
        if (rStats.health-Mathf.Abs(rStats.maxHealth * this.percentage) < 0)
        {
            this.messages.Enqueue(this.receiver.idName+" no puede perder mas vida");
           
        }
        else if (rStats.maxHealth-(rStats.maxHealth * this.percentage) > 0)
        {
            this.receiver.ModifyHealth(rStats.maxHealth * this.percentage);
        }


        this.messages.Enqueue(this.applyMessage.Replace("{receiver}", this.receiver.idName));

        return true;
    }

    public override bool BlocksTurn() => false;
}
