using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFighter : Fighter
{
    [Header("UI")]
    public PlayerSkillPanel skillPanel;
    public EnemiesPanel enemiesPanel;
    private Skill[] baraja = new Skill[4];
    private Skill skillToBeExecuted;

    [Header("stats")]
    public int level;
    public float maxHealth;
    public float attack;
    public float deffense;
    public float spirit;
    public float speed;
    void Awake()
    {
        this.stats = new Stats(level, maxHealth, attack, deffense, spirit, speed);
    }

    public override async void InitTurn()
    {
        this.skillPanel.ShowForPlayer(this);
        ;

        if (skills.Length <= 4)
        {
            for (int i = 0; i < this.skills.Length; i++)
            {
                this.skillPanel.ConfigureButton(i, this.skills[i].skillName, this.skills[i].skillDescription, this.skills[i].FondoCarta, this.skills[i].objetivos);
            }
            baraja = skills;
        }
        else if (skills.Length >= 5)
        {
            int Rand;
            int[] LastRand;
            int Max = this.skills.Length;

            LastRand = new int[Max];

            for (int u = 1; u < 4; u++)
            {
                Rand = Random.Range(0, this.skills.Length);

                for (int j = 1; j < u; j++)
                {
                    while (Rand == LastRand[j])
                    {
                        Rand = Random.Range(0, this.skills.Length);
                    }
                }

                LastRand[u] = Rand;

            }
            for (int i = 0; i < 4; i++)
            {
                this.skillPanel.ConfigureButton(i, this.skills[LastRand[i]].skillName, this.skills[LastRand[i]].skillDescription, this.skills[LastRand[i]].FondoCarta, this.skills[LastRand[i]].objetivos);
            }
            for (int i = 0; i < 4; i++)
            {
                baraja[i] = skills[LastRand[i]];

            }
        }
    }


    public void ExecuteSkill(int index)
    {

        this.skillToBeExecuted = this.baraja[index];



        this.skillToBeExecuted.SetEmitter(this);

        if (this.skillToBeExecuted.needsManualTargeting)
        {
            Fighter[] receivers = this.GetSkillTargets(this.skillToBeExecuted);
            this.enemiesPanel.Show(this, receivers);
        }
        else
        {
            this.AutoConfigureSkillTargeting(this.skillToBeExecuted);

            this.combatManager.OnFighterSkill(this.skillToBeExecuted);
            this.skillPanel.Hide();
        }
    }

    public void SetTargetAndAttack(Fighter enemyFigther)
    {
        this.skillToBeExecuted.AddReceiver(enemyFigther);

        this.combatManager.OnFighterSkill(this.skillToBeExecuted);
        this.isAttacking = true;
        this.skillPanel.Hide();
        this.enemiesPanel.Hide();
    }
}
