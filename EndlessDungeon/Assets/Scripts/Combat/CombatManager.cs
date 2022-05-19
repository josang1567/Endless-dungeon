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

    public transiciones panel;





    void Start()
    {



        this.returnBuffer = new List<Fighter>();

        this.fighters = GameObject.FindObjectsOfType<Fighter>();

        this.SortFightersBySpeed();
        this.MakeTeams();

        this.initStats();
        this.MakeOrder();

        LogPanel.Write("Comienza la batalla.");

        this.combatStatus = CombatStatus.NEXT_TURN;

        this.fighterIndex = -1;
        this.isCombatActive = true;
        StartCoroutine(this.CombatLoop());
    }
    //Funcion que se encarga de generar el orden de activacion de los peleadores
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
    // funcion encargada de asignar los iconos al orden de batalla
    private void MakeOrder()
    {
        List<Sprite> listado = new List<Sprite>();

        for (int i = 0; i < this.fighters.Length; i++)
        {
            if (fighters[i].isAlive)
            {
                listado.Add(fighters[i].Foto);
            }

        }
        TurnPanel.Mostrar(listado);

    }
    //funcion encargada de agrupar los peleadores segun sean jugadores o enemigos
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

    //funcion encargada de administrar el combate
    IEnumerator CombatLoop()
    {
        while (this.isCombatActive)
        {
            switch (this.combatStatus)
            {

                case CombatStatus.WAITING_FOR_FIGHTER:
                    yield return null;
                    break;
                //muestra por pantalla el ataque que esta realizando el peleador
                case CombatStatus.FIGHTER_ACTION:
                    LogPanel.Write($"{this.fighters[this.fighterIndex].idName} usa {currentFighterSkill.skillName}.");

                    yield return null;

                    // Executing fighter skill
                    //currentFighterSkill.Run();

                    // Espera tantos segundos como dure la animacion de ataque antes de pasar al siguiente turno
                    yield return new WaitForSeconds(currentFighterSkill.animationDuration);
                    this.combatStatus = CombatStatus.CHECK_ACTION_MESSAGES;

                    break;
                //Muestra por pantalla el mensaje de la habilidad usada
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
                //Comprueba si se cumplen los requisitos para la victoria o derrota del jugador y pone el mensaje adecuado para el momento
                case CombatStatus.CHECK_FOR_VICTORY:

                    bool arePlayersAlive = false;
                    //En caso de que todos los jugadores esten muertos se declara derrota
                    foreach (var figther in this.playerTeam)
                    {
                        arePlayersAlive |= figther.isAlive;
                    }



                    bool areEnemiesAlive = false;
                    //En caso de que todos los enemigos esten muertos se declara victoria
                    foreach (var figther in this.enemyTeam)
                    {
                        areEnemiesAlive |= figther.isAlive;
                    }


                    bool victory = areEnemiesAlive == false;
                    bool defeat = arePlayersAlive == false;
                    //en caso de victoria se pasa a la siguiente pantalla
                    if (victory)
                    {
                        LogPanel.Write("Victoria!");

                        if (!NextScene.Equals(""))
                        {

                            this.isCombatActive = false;
                            StartCoroutine(NextLevel());

                        }
                        else if (NextScene.Equals(""))
                        {
                            this.isCombatActive = false;
                        }


                    }
                    // en caso de derrota se reinicia el nivel
                    if (defeat)
                    {
                        LogPanel.Write("Derrota!");

                        this.isCombatActive = false;
                        StartCoroutine(waitToLoad());


                    }
                    //en caso de que no se cumplan los requisitos de victoria o derrota se pasa al siguiente turno
                    if (this.isCombatActive)
                    {
                        this.combatStatus = CombatStatus.NEXT_TURN;
                    }

                    yield return null;
                    break;
                //Se inicia el siguiente turno
                case CombatStatus.NEXT_TURN:
                    yield return new WaitForSeconds(1f);
                    //se llama a la funcion de MakeOrder para actualizar el orden de batalla
                    MakeOrder();
                    Fighter current = null;

                    do
                    {
                        this.fighterIndex = (this.fighterIndex + 1) % this.fighters.Length;

                        current = this.fighters[this.fighterIndex];
                    } while (current.isAlive == false);

                    this.combatStatus = CombatStatus.CHECK_FIGHTER_STATUS_CONDITION;

                    break;

                //Se comprueba si el peleador activo tiene algun efecto de estado, en caso de tener se aplican sus efectos
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

                    string mensaje = "";
                    //se muestra en pantalla las estadisticas del jugador y si tiene algun efecto de estado durante de un jugador
                    if (currentFighter.team.ToString().Equals("PLAYERS"))
                    {
                        if (currentFighter.statusCondition == null)
                        {
                            mensaje += $"Es el turno de {currentFighter.idName}.\n"
                               + $"Vida:{currentFighter.GetStats().health}\n"
                       + $"Ataque:{currentFighter.GetStats().attack}\n"
                       + $"Defensa:{currentFighter.GetStats().deffense}\n"
                       + $"Velocidad:{currentFighter.GetStats().speed}\n"
                       + "Estado:";
                        }
                        else if (currentFighter.statusCondition != null)
                        {
                            mensaje += $"Es el turno de {currentFighter.idName}.\n"
                           + $"Ataque:{currentFighter.GetStats().attack}\n"
                           + $"Defensa:{currentFighter.GetStats().deffense}\n"
                           + $"Velocidad:{currentFighter.GetStats().speed}\n"
                          + $"Estado:{currentFighter.statusCondition.estado}\n";
                        }






                    }
                    //Durante del turno de un enemigo no se muestran sus estadisticas
                    else
                    if (currentFighter.team.ToString().Equals("ENEMIES"))
                    {

                        mensaje += $"Es el turno de {currentFighter.idName}.";
                    }

                    LogPanel.Write(mensaje);
                    currentFighter.InitTurn();

                    this.combatStatus = CombatStatus.WAITING_FOR_FIGHTER;
                    break;
            }
        }
    }
    //funcion encargada en filtrar solo los peleadores vivos que pueden ser objetivo de ataques o beneficios
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

    //Funcion encargada de mostrar solo los peleadores del equipo contrario para aplicar las habilidades agresivas
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

    //Funcion encargada de mostrar los aliados objetivos de beneficios
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
    //funcion que modifica el estado de combate cuanndo un peleador usa una habilidad
    public void OnFighterSkill(Skill skill)
    {

        this.currentFighterSkill = skill;
        this.combatStatus = CombatStatus.FIGHTER_ACTION;
    }
    //Funcion encarcagada de manejar las transiciones de reinicio de nivel y reiniciar el nivel
    IEnumerator waitToLoad()
    {
        yield return new WaitForSeconds(1);
        LogPanel.Write("Reiniciando nivel...");
        yield return new WaitForSeconds(2);
        this.panel.StartAnim();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    //Funcion encargada de manejar las animaciones de cambio de nivel, pasar de nivel y guardar el progreso del jugador
    IEnumerator NextLevel()
    {

        yield return new WaitForSeconds(1);
        LogPanel.Write("Adentrandose en la mazmorra...");
        yield return new WaitForSeconds(1);
        this.panel.StartAnim();
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(this.NextScene);

        PlayerPrefs.SetString("NivelActual", this.NextScene);


        for (int i = 0; i < playerTeam.Length; i++)
        {
            if (playerTeam[i].idName == "Cazadora")
            {
                PlayerPrefs.SetFloat("HuntressVida", this.playerTeam[i].GetStats().health);
            }
            if (playerTeam[i].idName == "Mago")
            {
                PlayerPrefs.SetFloat("MagoVida", this.playerTeam[i].GetStats().health);
            }
            if (playerTeam[i].idName == "Caballero")
            {
                PlayerPrefs.SetFloat("KnightVida", this.playerTeam[i].GetStats().health);
            }
        }
        //En caso de enfrentarse a un enemigo con la etiquieta Boss y salir victorioso se guarda la vida segun la caracteristica maxima de vida que tiene en ese momento cada heroe
        //Ejemplo: Si el Caballero tiene 80 de vida maxima se cura su vida para que se ajueste a 80 y se guarda esa cantidad para que al empezar el siguiente nivel tenga toda la vida
        if (this.gameObject.tag == "Boss")
        {

            for (int i = 0; i < playerTeam.Length; i++)
            {
                if (playerTeam[i].idName == "Cazadora")
                {
                    PlayerPrefs.SetFloat("HuntressVida", this.playerTeam[i].GetStats().maxHealth);
                }
                if (playerTeam[i].idName == "Mago")
                {
                    PlayerPrefs.SetFloat("MagoVida", this.playerTeam[i].GetStats().maxHealth);
                }
                if (playerTeam[i].idName == "Caballero")
                {
                    PlayerPrefs.SetFloat("KnightVida", this.playerTeam[i].GetStats().maxHealth);
                }
            }
        }

    }
// funcion encargada de poner las estadisticas de los jugadores al pasar de nivel
    public void initStats()
    {
        // en caso de haber datos se comprueban quienes tienen vida mayor que 0

        //En caso de que algun heroe tenga vida igual a 0 se le da una decima parte de su vida maxima al inicio de ronda
        if (PlayerPrefs.GetFloat("HuntressVida", 0) <= 0)
        {
            for (int i = 0; i < playerTeam.Length; i++)
            {
                if (playerTeam[i].idName == "Cazadora")
                {

                    playerTeam[i].GetStats().setHealth(playerTeam[i].GetStats().maxHealth / 10);
                    playerTeam[i].statusPanel.SetHealth(playerTeam[i].GetStats().maxHealth / 10, playerTeam[i].GetStats().maxHealth);
                }
            }


        }
        if (PlayerPrefs.GetFloat("MagoVida", 0) <= 0)
        {
            for (int i = 0; i < playerTeam.Length; i++)
            {
                if (playerTeam[i].idName == "Mago")
                {

                    playerTeam[i].GetStats().setHealth(playerTeam[i].GetStats().maxHealth / 10);
                    playerTeam[i].statusPanel.SetHealth(playerTeam[i].GetStats().maxHealth / 10, playerTeam[i].GetStats().maxHealth);
                }
            }

        }
        if (PlayerPrefs.GetFloat("KnightVida", 0) <= 0)
        {
            for (int i = 0; i < playerTeam.Length; i++)
            {
                if (playerTeam[i].idName == "Caballero")
                {

                    playerTeam[i].GetStats().setHealth(playerTeam[i].GetStats().maxHealth / 10);
                    playerTeam[i].statusPanel.SetHealth(playerTeam[i].GetStats().maxHealth / 10, playerTeam[i].GetStats().maxHealth);
                }
            }

        }


        // en caso de que algun peleador jugable tenga mas de 1 de vida se actualiza la tablas de vida con la vida anteriormente guardada

        if (PlayerPrefs.GetFloat("HuntressVida", 0) >= 1)
        {
            for (int i = 0; i < playerTeam.Length; i++)
            {
                if (playerTeam[i].idName == "Cazadora")
                {
                    playerTeam[i].GetStats().setHealth(PlayerPrefs.GetFloat("HuntressVida", 0));
                    playerTeam[i].statusPanel.SetHealth(PlayerPrefs.GetFloat("HuntressVida", 0), playerTeam[i].GetStats().maxHealth);
                }
            }
        }
        if (PlayerPrefs.GetFloat("MagoVida", 0) >= 1)
        {
            for (int i = 0; i < playerTeam.Length; i++)
            {
                if (playerTeam[i].idName == "Mago")
                {
                    playerTeam[i].GetStats().setHealth(PlayerPrefs.GetFloat("MagoVida", 0));
                    playerTeam[i].statusPanel.SetHealth(PlayerPrefs.GetFloat("MagoVida", 0), playerTeam[i].GetStats().maxHealth);
                }
            }
        }
        if (PlayerPrefs.GetFloat("KnightVida", 0) >= 1)
        {
            for (int i = 0; i < playerTeam.Length; i++)
            {
                if (playerTeam[i].idName == "Caballero")
                {

                    playerTeam[i].GetStats().setHealth(PlayerPrefs.GetFloat("KnightVida", 0));
                    playerTeam[i].statusPanel.SetHealth(PlayerPrefs.GetFloat("KnightVida", 0), playerTeam[i].GetStats().maxHealth);

                }
            }
        }



    }



}