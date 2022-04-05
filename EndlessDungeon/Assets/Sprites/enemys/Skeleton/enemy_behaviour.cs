using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_behaviour : MonoBehaviour
{
#region  public variables
   public Transform rayCast;
   public LayerMask rayCastMask;
   public float rayCastLength;
   public float attackDistance;
   public float moveSpeed;
   public float timer;
#endregion

#region private variables
    private RaycastHit2D hit;
    private GameObject target;
    private Animator anim;
    private float distance;//distancia entre enemigo y jugador

    private bool attackMode,inRange,cooling;
    private float intTimer;

#endregion
   
   void Awake(){
       intTimer=timer;
       anim=GetComponent<Animator>();
   }
   
       void Update()
    {
        if(inRange ==true){
            hit=Physics2D.Raycast(rayCast.position, Vector2.left,rayCastLength,rayCastMask);
            RaycastDebugger();
        }
        if(hit.collider!=null){
            EnemyLogic();
        }else if(hit.collider==null){
            inRange=false;
        }
        if(inRange==false){
            anim.SetBool("canWalk",false);
            StopAttack();
        }
    }
    void OnTriggerEnter2D(Collider2D trig){
        if(trig.gameObject.tag=="Player"){
            target=trig.gameObject;
            inRange=true;
        }
    }

    void EnemyLogic(){
        distance=Vector2.Distance(transform.position,target.transform.position);
        if(distance>attackDistance){
            Move();
            StopAttack();
        }else if(attackDistance>=distance && cooling== false){
            Attack();
        }
        if(cooling==true){
            Cooldown();
            anim.SetBool("Attack",false);
        }
    }

    void RaycastDebugger(){
        if(distance > attackDistance){
            Debug.DrawRay(rayCast.position,Vector2.left*rayCastLength,Color.red);
        }
        else if(distance < attackDistance){
            Debug.DrawRay(rayCast.position,Vector2.left*rayCastLength,Color.green);
        }
    }
    void Move(){
        anim.SetBool("canWalk",true);

        if(!anim.GetCurrentAnimatorStateInfo(0).IsName("skelettonattack")){
            Vector2 targetPosition=new Vector2(target.transform.position.x,transform.position.y);
            transform.position=Vector2.MoveTowards(transform.position, targetPosition, moveSpeed*Time.deltaTime);
        }
    }
    void Attack(){
        timer=intTimer;
        attackMode=true;
        anim.SetBool("canWalk",false);
        anim.SetBool("Attack",true);
    }
    void StopAttack(){
        cooling=false;
        anim.SetBool("Attack",false);
    }
    public void TriggerCooling(){
        cooling=true;
    }
    void Cooldown(){
        timer-=Time.deltaTime;
        if(timer<=0 && cooling==true&&attackMode==true){
            cooling=false;
            timer=intTimer;
        }
    }
}
