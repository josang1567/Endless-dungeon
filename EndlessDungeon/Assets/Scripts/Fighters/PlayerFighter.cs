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
    private Fighter[] objetivos;
    void Awake()
    {//Asignacion de los stats del jugador
        this.stats = new Stats(level, maxHealth, attack, deffense, spirit, speed);
    }

    //funcion encargada de barajar el mazo de habilidades del jugador
    public override async void InitTurn()
    {
        this.skillPanel.ShowForPlayer(this);
        ;

        if (skills.Length <= 4)
        {
            for (int i = 0; i < this.skills.Length; i++)
            {
                this.skillPanel.ConfigureButton(i, this.skills[i].skillName, this.skills[i].skillDescription, this.skills[i].FondoCarta);
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
                this.skillPanel.ConfigureButton(i, this.skills[LastRand[i]].skillName, this.skills[LastRand[i]].skillDescription, this.skills[LastRand[i]].FondoCarta);
            }
            for (int i = 0; i < 4; i++)
            {
                baraja[i] = skills[LastRand[i]];

            }
        }
    }

    //Funcion que se encarga de ejecutar la habilidad seleccionada
    public void ExecuteSkill(int index)
    {

        this.skillToBeExecuted = this.baraja[index];



        this.skillToBeExecuted.SetEmitter(this);

        if (this.skillToBeExecuted.needsManualTargeting)
        {
            Fighter[] receivers = this.GetSkillTargets(this.skillToBeExecuted);
            objetivos = receivers;
            this.enemiesPanel.Show(this, receivers);
        }
        else
        {
            objetivos = this.combatManager.GetOpposingTeam();
            this.AutoConfigureSkillTargeting(this.skillToBeExecuted);

            this.combatManager.OnFighterSkill(this.skillToBeExecuted);
            this.skillPanel.Hide();
            this.enemiesPanel.HidePlayer(this);
            this.isAttacking = true;
        }
    }
    //Funcion que ejecuta el sonido y animacion del ataque
    public void ExecuteSound()
    {
        this.skillToBeExecuted.Run();
    }

    //Funcion encargada de asignar el objetivo del ataque 
    public void SetTargetAndAttack(Fighter enemyFigther)
    {
        this.skillToBeExecuted.AddReceiver(enemyFigther);

        this.combatManager.OnFighterSkill(this.skillToBeExecuted);
        this.skillPanel.Hide();
        this.enemiesPanel.HidePlayer(this);
    }
}
