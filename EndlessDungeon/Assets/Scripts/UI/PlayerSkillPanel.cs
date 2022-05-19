using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSkillPanel : MonoBehaviour
{
    public GameObject[] skillButtons;
    public Text[] SkillTitleLabels;
    public Text[] SkillDescriptionLabels;
    public Button[] Fondos;
    private PlayerFighter targetFigther;

    void Awake()
    {
        this.Hide();
    }
    //Configura los botones de carta para que adquieran las carasteristicas del objeto
    public void ConfigureButton(int index, string skillName, string skillDescription, Sprite fondoCarta)
    {
        this.skillButtons[index].SetActive(true);
        this.SkillTitleLabels[index].text = skillName;
        this.SkillDescriptionLabels[index].text = skillDescription;
        this.Fondos[index].image.sprite = fondoCarta;

    }
    //Ejecuta la habilidad clickada
    public void OnSkillButtonClick(int index)
    {
        this.targetFigther.ExecuteSkill(index);
    }
    // se muestan los jugadores objetivo
    public void ShowForPlayer(PlayerFighter newTarget)
    {
        this.gameObject.SetActive(true);

        this.targetFigther = newTarget;
    }
    //esconde la baraja de cartas
    public void Hide()
    {
        this.gameObject.SetActive(false);

        foreach (var btn in this.skillButtons)
        {
            btn.SetActive(false);
        }
    }

}
