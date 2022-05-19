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
    private Skill skillToBeExecuted;

    public GameObject startPosition;
    void Awake()
    {
        //Actualiza la posicion de los enemigos para que se ajusten a una posicion en el mapa
        transform.position=startPosition.transform.position;
        //Asigna las estadisticas al enemigo
        this.stats = new Stats(level, maxHealth, attack, deffense, spirit, speed);
    }
    public override void InitTurn()
    {
        StartCoroutine(this.IA());
    }
    //Controlador de la inteligencia artificial encargada de controlar los enemigos
    IEnumerator IA()
    {
        yield return new WaitForSeconds(1f);

        Skill skill = this.skills[Random.Range(0, this.skills.Length)];//Se selecciona una habilidad aleatoria cada turno
        skillToBeExecuted=skill;
        skill.SetEmitter(this);
        //Se selecciona un objetivo aleatorio en caso de que se necesite un objetivo
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
    //Funcion que ejecuta el efecto de sonido de la habilidad ejecutada
       public void ExecuteSound(){
            
           this.skillToBeExecuted.Run();
        }

}
