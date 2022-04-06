using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
public enum CombatStatus
{
    WAITING_FOR_FIGHTER,
    FIGHTER_ACTION,
    CHECK_ACTION_MESSAGES,
    CHECK_FOR_VICTORY,
    NEXT_TURN,
    CHECK_FIGHTER_STATUS_CONDITION
}

public class CombatManager : MonoBehaviour
{
    private Fighter[] playerTeam;
    private Fighter[] enemyTeam;
    private Fighter[] fighters;
    [Header("Siguiente escena")]
    public string NextScene;
    private int fighterIndex;

    private bool isCombatActive;

    private CombatStatus combatStatus;

    private Skill currentFighterSkill;

    private List<Fighter> returnBuffer;


    void onAwake()
    {
        DontDestroyOnLoad(playerTeam[0]);
        DontDestroyOnLoad(playerTeam[1]);
        DontDestroyOnLoad(playerTeam[2]);
    }
    void Start()
    {

        this.returnBuffer = new List<Fighter>();

        this.fighters = GameObject.FindObjectsOfType<Fighter>();

        this.SortFightersBySpeed();
        this.MakeTeams();
        this.MakeOrder();

        LogPanel.Write("Comienza la batalla.");

        this.combatStatus = CombatStatus.NEXT_TURN;

        this.fighterIndex = -1;
        this.isCombatActive = true;
        StartCoroutine(this.CombatLoop());
    }

    private void SortFightersBySpeed()
    {
        bool sorted = false;
        while (!sorted)
        {
            sorted = true;

            for (int i = 0; i < this.fighters.Length - 1; i++)
            {
                Fighter a = this.fighters[i];
                Fighter b = this.fighters[i + 1];

                float aSpeed = a.GetCurrentStats().speed;
                float bSpeed = b.GetCurrentStats().speed;

                if (bSpeed > aSpeed)
                {
                    this.fighters[i] = b;
                    this.fighters[i + 1] = a;

                    sorted = false;
                }
            }
        }
    }

    private void MakeOrder()
    {
        string listado = "Orden de batalla:\n";
        
        for (int i = 0; i < this.fighters.Length; i++)
        {
            if (fighters[i].isAlive)
            {
                
                if (i+1 == this.fighters.Length)
                {
                    
                    listado += fighters[i].idName;
                }
                else if (i+1 != this.fighters.Length)
                {
                  
                    listado += fighters[i].idName + "-> ";
                }

            }

        }
        TurnPanel.Write(listado);
        
    }
    private void MakeTeams()
    {
        List<Fighter> playersBuffer = new List<Fighter>();
        List<Fighter> enemiesBuffer = new List<Fighter>();

        foreach (var fgtr in this.fighters)
        {
            if (fgtr.team == Team.PLAYERS)
            {
                playersBuffer.Add(fgtr);
            }
            else if (fgtr.team == Team.ENEMIES)
            {
                enemiesBuffer.Add(fgtr);
            }

            fgtr.combatManager = this;
        }

        this.playerTeam = playersBuffer.ToArray();
        this.enemyTeam = enemiesBuffer.ToArray();
    }

    IEnumerator CombatLoop()
    {
        while (this.isCombatActive)
        {
            switch (this.combatStatus)
            {
                case CombatStatus.WAITING_FOR_FIGHTER:
                    yield return null;
                    break;

                case CombatStatus.FIGHTER_ACTION:
                    LogPanel.Write($"{this.fighters[this.fighterIndex].idName} usa {currentFighterSkill.skillName}.");

                    yield return null;

                    // Executing fighter skill
                    currentFighterSkill.Run();

                    // Wait for fighter skill animation
                    yield return new WaitForSeconds(currentFighterSkill.animationDuration);
                    this.combatStatus = CombatStatus.CHECK_ACTION_MESSAGES;

                    break;
                case CombatStatus.CHECK_ACTION_MESSAGES:
                    string nextMessage = this.currentFighterSkill.GetNextMessage();

                    if (nextMessage != null)
                    {
                        LogPanel.Write(nextMessage);
                        yield return new WaitForSeconds(2f);
                    }
                    else
                    {
                        this.currentFighterSkill = null;
                        this.combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                        yield return null;
                    }
                    break;

                case CombatStatus.CHECK_FOR_VICTORY:
                    bool arePlayersAlive = false;
                    foreach (var figther in this.playerTeam)
                    {
                        arePlayersAlive |= figther.isAlive;
                    }



                    bool areEnemiesAlive = false;
                    foreach (var figther in this.enemyTeam)
                    {
                        areEnemiesAlive |= figther.isAlive;
                    }


                    bool victory = areEnemiesAlive == false;
                    bool defeat = arePlayersAlive == false;

                    if (victory)
                    {
                        LogPanel.Write("Victoria!");
                        if (!NextScene.Equals(""))
                        {

                            StartCoroutine(NextLevel());
                            this.isCombatActive = false;
                        }
                        else if (NextScene.Equals(""))
                        {
                            this.isCombatActive = false;
                        }


                    }

                    if (defeat)
                    {
                        LogPanel.Write("Derrota!");
                        this.isCombatActive = false;
                        StartCoroutine(waitToLoad());


                    }

                    if (this.isCombatActive)
                    {
                        this.combatStatus = CombatStatus.NEXT_TURN;
                    }

                    yield return null;
                    break;
                case CombatStatus.NEXT_TURN:
                    yield return new WaitForSeconds(1f);
                    MakeOrder();
                    Fighter current = null;

                    do
                    {
                        this.fighterIndex = (this.fighterIndex + 1) % this.fighters.Length;

                        current = this.fighters[this.fighterIndex];
                    } while (current.isAlive == false);

                    this.combatStatus = CombatStatus.CHECK_FIGHTER_STATUS_CONDITION;

                    break;

                case CombatStatus.CHECK_FIGHTER_STATUS_CONDITION:
                    var currentFighter = this.fighters[this.fighterIndex];

                    var statusCondition = currentFighter.GetCurrentStatusCondition();

                    if (statusCondition != null)
                    {
                        statusCondition.Apply();

                        while (true)
                        {
                            string nextSCMessage = statusCondition.GetNextMessage();
                            if (nextSCMessage == null)
                            {
                                break;
                            }

                            LogPanel.Write(nextSCMessage);
                            yield return new WaitForSeconds(2f);
                        }

                        if (statusCondition.BlocksTurn())
                        {
                            this.combatStatus = CombatStatus.CHECK_FOR_VICTORY;
                            break;
                        }
                    }

                    LogPanel.Write($"Es el turno de {currentFighter.idName}.");
                    currentFighter.InitTurn();

                    this.combatStatus = CombatStatus.WAITING_FOR_FIGHTER;
                    break;
            }
        }
    }

    public Fighter[] FilterJustAlive(Fighter[] team)
    {
        this.returnBuffer.Clear();

        foreach (var fgtr in team)
        {
            if (fgtr.isAlive)
            {
                this.returnBuffer.Add(fgtr);
            }
        }

        return this.returnBuffer.ToArray();
    }

    public Fighter[] GetOpposingTeam()
    {
        Fighter currentFighter = this.fighters[this.fighterIndex];

        Fighter[] team = null;
        if (currentFighter.team == Team.PLAYERS)
        {
            team = this.enemyTeam;
        }
        else if (currentFighter.team == Team.ENEMIES)
        {
            team = this.playerTeam;
        }

        return this.FilterJustAlive(team);
    }

    public Fighter[] GetAllyTeam()
    {
        Fighter currentFighter = this.fighters[this.fighterIndex];

        Fighter[] team = null;
        if (currentFighter.team == Team.PLAYERS)
        {
            team = this.playerTeam;
        }
        else
        {
            team = this.enemyTeam;
        }

        return this.FilterJustAlive(team);
    }

    public void OnFighterSkill(Skill skill)
    {
        this.currentFighterSkill = skill;
        this.combatStatus = CombatStatus.FIGHTER_ACTION;
    }
    IEnumerator waitToLoad()
    {
        yield return new WaitForSeconds(1);
        LogPanel.Write("Reiniciando nivel...");
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(1);
        LogPanel.Write("El equipo se cura sus heridas");
        yield return new WaitForSeconds(1);
        LogPanel.Write("Pasando de nivel...");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(this.NextScene);
    }
}