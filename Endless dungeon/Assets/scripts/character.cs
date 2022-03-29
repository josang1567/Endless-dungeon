using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class character : MonoBehaviour
{
    GameObject targets;
    CombatCtrl combatCtrl;
    public GameObject barlife;
    public SpriteRenderer sr;
    public GameObject select;
    float ScaleI;
    int maxlife;
    public int life;
    public int atack;
    int target;
    public bool type;

    public void Start()
    {
        ScaleI= barlife.transform.localScale.x;
        maxlife=life;
        combatCtrl = GameObject.Find("CombatCtrl").GetComponent<CombatCtrl>();
        if (type)
        {
            targets = GameObject.Find("Enemies");

        }
        else targets = GameObject.Find("Players");
    }


    public void Atack()
    {
        StartCoroutine(AnimAtack());
        if (type) target = combatCtrl.EnemySelect;
        else target = combatCtrl.PlayerSelect;
        if (combatCtrl.EnemyN >= 0 && combatCtrl.PlayersN >= 0)
            targets.transform.GetChild(target).GetComponent<character>().Damage(atack);
    }

    public void Damage(int attack){
        life-= attack;
        StartCoroutine(AnimDamage(atack));
        if(life<=0){
            if(type) combatCtrl.PlayersN--;else combatCtrl.EnemyN--;
            Destroy(gameObject);
        }
    }
    public void Select(bool select)
    {
        this.select.SetActive(select);
    }
    IEnumerator AnimAtack()
    {
        float mov = 0.3f;
        if (!type) mov *= -1;
        transform.position = new Vector3(transform.position.x + mov, transform.position.y, transform.position.z);
        yield return new WaitForSecondsRealtime(0.2f);
        transform.position = new Vector3(transform.position.x - mov, transform.position.y, transform.position.z);
    }

    IEnumerator AnimDamage(float damage){

            barlife.transform.localScale= new Vector3(barlife.transform.localScale.x-(damage/maxlife)*ScaleI,
            barlife.transform.localScale.y,barlife.transform.localScale.z);

        for (int i = 0; i < 10; i++)
        {
            sr.enabled=!sr.enabled;
            yield return new WaitForSecondsRealtime(0.5f);
        }
    }
}
