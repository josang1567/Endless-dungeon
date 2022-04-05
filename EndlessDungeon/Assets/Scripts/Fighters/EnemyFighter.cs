using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFighter : Fighter
{ [Header("stats")]
    public int level;
     public float maxHealth;
    public float attack;
    public float deffense;
    public float spirit;
    public float speed;

    public GameObject startPosition;
    void Awake()
    {
        transform.position=startPosition.transform.position;
        this.stats = new Stats(level, maxHealth, attack, deffense, spirit, speed);
    }
    public override void InitTurn()
    {
        StartCoroutine(this.IA());
    }

    IEnumerator IA()
    {
        yield return new WaitForSeconds(1f);

        Skill skill = this.skills[Random.Range(0, this.skills.Length)];
        skill.SetEmitter(this);
        
        if (skill.needsManualTargeting)
        {
            Fighter[] targets = this.GetSkillTargets(skill);

            Fighter target = targets[Random.Range(0, targets.Length)];

            skill.AddReceiver(target);
        }
        else
        {
            this.AutoConfigureSkillTargeting(skill);

        }

        this.combatManager.OnFighterSkill(skill);
    this.isAttacking = true;
    }
}
