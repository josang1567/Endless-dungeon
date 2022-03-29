using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public Fighter[] fighters;
    private int fighterIndex;

    void Start()
    {
        LogPanel.Write("Battle initiated.");

        this.fighterIndex = 0;
        
       
    }

}
