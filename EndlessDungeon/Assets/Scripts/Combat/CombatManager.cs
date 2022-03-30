using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Fighter[] fighters;
    private int fighterIndex;
 private bool isCombatActive;
   void Start()
    {
        LogPanel.Write("Comienza la batalla.");

        this.fighterIndex = 0;
        this.isCombatActive = true;
        StartCoroutine(this.CombatLoop());
    }

    IEnumerator CombatLoop()
    {
        while (this.isCombatActive)
        {
            yield return new WaitForSeconds(2f);

            var currentTurn = this.fighters[this.fighterIndex];

            LogPanel.Write($"Es el turno de {currentTurn.idName}.");
            currentTurn.InitTurn();

            this.fighterIndex = (this.fighterIndex + 1) % this.fighters.Length;
        }
    }

}
