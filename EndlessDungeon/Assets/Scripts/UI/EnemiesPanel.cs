using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesPanel : MonoBehaviour
{
 public GameObject[] btns;
    public Text[] btnLabels;

    private PlayerFighter targetFigther;

    private Fighter[] targets;

    void Awake()
    {
        this.Hide();

        this.targets = new Fighter[this.btns.Length];
    }
//Configura los botones visibles del panel de objetivos 
    public void ConfigureButton(Fighter fighter, int index)
    {
        this.btns[index].SetActive(true);
        this.btnLabels[index].text = fighter.idName;

        this.targets[index] = fighter;
    }
//Funcion encargada de ejecutar la habilidad clicada
    public void OnTargetButtonClick(int index)
    {
        Fighter enemy = this.targets[index];

        this.targetFigther.SetTargetAndAttack(enemy);
    }
//Funcion encargada de mostrar el panel de objetivos
    public void Show(PlayerFighter playerTarget, Fighter[] enemies)
    {
        this.gameObject.SetActive(true);

        this.targetFigther = playerTarget;

        int btnIndex = 0;
       foreach (var enemy in enemies)
        {
            if (enemy.isAlive)
            {
                this.ConfigureButton(enemy, btnIndex);
                btnIndex ++;
            }
        }
        
      
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);

        foreach (var btn in this.btns)
        {
            btn.SetActive(false);
        }
        
    }
     public void HidePlayer(Fighter lanzador)
    {
        lanzador.isAttacking=true;
        this.gameObject.SetActive(false);

        foreach (var btn in this.btns)
        {
            btn.SetActive(false);
        }
        
    }
}
