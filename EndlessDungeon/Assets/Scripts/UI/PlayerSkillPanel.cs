using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerSkillPanel : MonoBehaviour
{
    public GameObject[] skillButtons;
    public Text[] SkillTitleLabels;
    public Text[] SkillDescriptionLabels;
    public Text[] SkillObjetivosLabels;
    public Button[] Fondos;
    private PlayerFighter targetFigther;

    void Awake()
    {
        this.Hide();
    }

    public void ConfigureButton(int index, string skillName, string skillDescription, Sprite fondoCarta, string objetivos)
    {
        this.skillButtons[index].SetActive(true);
        this.SkillTitleLabels[index].text = skillName;
        this.SkillDescriptionLabels[index].text=skillDescription;
        this.Fondos[index].image.sprite=fondoCarta;
        this.SkillObjetivosLabels[index].text=objetivos;
    }

    public void OnSkillButtonClick(int index)
    {
        this.targetFigther.ExecuteSkill(index);
    }

    public void ShowForPlayer(PlayerFighter newTarget)
    {
        this.gameObject.SetActive(true);

        this.targetFigther = newTarget;
    }

    public void Hide()
    {
        this.gameObject.SetActive(false);

        foreach (var btn in this.skillButtons)
        {
            btn.SetActive(false);
        }
    }

}
