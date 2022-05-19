using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Fighter : MonoBehaviour
{
    public Team team;

    public string idName;
    public StatusPanel statusPanel;

    public CombatManager combatManager;

    public List<StatusMod> statusMods;
    protected Stats stats;

    protected Skill[] skills;
    public StatusCondition statusCondition;
    public bool isAttacking;
    public bool isHit;
    public bool isDead;
    private Animator anim;
    [Header("FavIcon")]
    public Sprite Foto;

    [Header("Dejar Cadaver")]
    public bool Cadaver;
    public bool isAlive
    {
        get => this.stats.health > 0;
    }

    protected virtual void Start()
    {
        isAttacking = false;

        isDead = false;
        anim = GetComponent<Animator>();
        this.statusPanel.SetStats(this.idName, this.stats);
        this.skills = this.GetComponentsInChildren<Skill>();

        this.statusMods = new List<StatusMod>();
    }
    void FixedUpdate()
    {
        anim.SetBool("isAttacking", isAttacking);

        anim.SetBool("isDead", isDead);
        anim.SetBool("isHit", isHit);
    }
    //funcion encargada de asignar los objetivos en caso de que no se necesite seleccionar un objetivo
    protected void AutoConfigureSkillTargeting(Skill skill)
    {
        skill.SetEmitter(this);

        switch (skill.targeting)
        {
            case SkillTargeting.AUTO:
                skill.AddReceiver(this);
                break;
            case SkillTargeting.ALL_ALLIES:
                Fighter[] allies = this.combatManager.GetAllyTeam();
                foreach (var receiver in allies)
                {
                    skill.AddReceiver(receiver);
                }

                break;
            case SkillTargeting.ALL_OPPONENTS:
                Fighter[] enemies = this.combatManager.GetOpposingTeam();
                foreach (var receiver in enemies)
                {
                    skill.AddReceiver(receiver);
                }
                break;

            case SkillTargeting.SINGLE_ALLY:
            case SkillTargeting.SINGLE_OPPONENT:
                throw new System.InvalidOperationException("Unimplemented! This skill needs manual targeting.");
        }
    }
    //Funcion encargada de mostrar los objetivos disponibles en caso de que la habilidad solo pueda usar un solo objetivo
    protected Fighter[] GetSkillTargets(Skill skill)
    {
        switch (skill.targeting)
        {
            case SkillTargeting.AUTO:
            case SkillTargeting.ALL_ALLIES:
            case SkillTargeting.ALL_OPPONENTS:
                throw new System.InvalidOperationException("Unimplemented! This skill doesn't need manual targeting.");

            case SkillTargeting.SINGLE_ALLY:
                return this.combatManager.GetAllyTeam();
            case SkillTargeting.SINGLE_OPPONENT:
                return this.combatManager.GetOpposingTeam();
        }

        // Esto no debería ejecutarse nunca pero hay que ponerlo para hacer al compilador feliz.
        throw new System.InvalidOperationException("Fighter::GetSkillTargets. Unreachable!");
    }
    //funcion encargada de eliminar los paneles y peleadores muertos
    protected void Die()
    {
        this.statusPanel.gameObject.SetActive(false);
        if (Cadaver == false)
        {
            this.gameObject.SetActive(false);
        }


    }
    //Funcion encargada de actualizar la vida cuando un peleador recibe  daño o se cura
    public void ModifyHealth(float amount)
    {
        this.stats.health = Mathf.Clamp(this.stats.health + amount, 0f, this.stats.maxHealth);

        this.stats.health = Mathf.Round(this.stats.health);
        this.statusPanel.SetHealth(this.stats.health, this.stats.maxHealth);

        if (this.isAlive == false)
        {
            isDead = true;
            StartCoroutine(this.death());
            Invoke("Die", 0.75f);
        }

    }
    //funcion que devuelve los stats actuales
    public Stats GetCurrentStats()
    {
        Stats modedStats = this.stats;

        foreach (var mod in this.statusMods)
        {
            if (modedStats.deffense >= 10)
            {

                if (modedStats.deffense == 10)
                { }
                else
                {
                    modedStats = mod.Apply(modedStats);

                }

            }
            if (modedStats.deffense >= 30)
            {

                if (modedStats.deffense == 140)
                { }
                else
                {
                    modedStats = mod.Apply(modedStats);

                }

            }

        }

        return modedStats;
    }

    //Funcion  que  devuelve el efecto de estado en caso de que no se haya agotado y exista
    public StatusCondition GetCurrentStatusCondition()
    {
        if (this.statusCondition != null && this.statusCondition.hasExpired)
        {
            Destroy(this.statusCondition.gameObject);
            this.statusCondition = null;
        }

        return this.statusCondition;
    }
    //Funcion encargada de detener la animacion de ataque
    public void EndAttack()
    {
        isAttacking = false;
    }
    //Funcion que devuelve los stats iniciales
    public Stats GetStats()
    {
        return this.stats;
    }

    public abstract void InitTurn();

    IEnumerator death()
    {

        yield return new WaitForSeconds(3f);
    }
}
