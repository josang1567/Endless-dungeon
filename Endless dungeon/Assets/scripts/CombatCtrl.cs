using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCtrl : MonoBehaviour
{
     public int EnemyN=2, PlayersN=1; // 0,1,2

    public int EnemySelect, PlayerSelect;
    public GameObject enemies,players;
    bool turn=true;

    

    private void Start(){
        character stats= players.transform.GetChild(PlayerSelect).GetComponent<character>();
        stats.Select(true);
    }
   private void Update()
    {
       if(EnemyN>=0){
           enemies.transform.GetChild(EnemySelect).GetComponent<character>().Select(false);
           if(Input.GetKeyDown(KeyCode.DownArrow)){
               EnemySelect++;
           }
            if(Input.GetKeyDown(KeyCode.UpArrow)){
               EnemySelect--;
           }
           EnemySelect=Mathf.Clamp(EnemySelect,0,EnemyN);
            enemies.transform.GetChild(EnemySelect).GetComponent<character>().Select(true);

       } 
    }

    public void Atack(){

            if(turn && PlayersN>=0){
                character ch= players.transform.GetChild(PlayerSelect).GetComponent<character>();
                ch.Atack();

                if(PlayerSelect==PlayersN){
                    PlayerSelect=0;
                    turn = false;
                    StartCoroutine(AtackE());
                }
                else PlayerSelect++;

                ch.Select(false);
                ch= players.transform.GetChild(PlayerSelect).GetComponent<character>();
                ch.Select(true);
            }
    }

    IEnumerator AtackE(){
        if(EnemyN>=0){
            yield return new WaitForSecondsRealtime(1f);
            for (int i = 0; i < EnemyN; i++)
            {
                enemies.transform.GetChild(i).GetComponent<character>().Atack();
                yield return new WaitForSecondsRealtime(1f);
            }
            turn=true;
        }
    }
}
