using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusPanel : MonoBehaviour
{  public Text nameLabel;
    public Text levelLabel;

    public Slider healthSlider;
    public Image healthSliderBar;
    public Text healthLabel;

    public void SetStats(string name, Stats stats)
    {
         this.nameLabel.text = name;

        this.levelLabel.text = $"N. {stats.level}";
        Debug.Log("Vida1: "+stats.health);
        this.SetHealth(stats.health, stats.maxHealth);
    }

     public void SetHealth(float health, float maxHealth)
    {
        this.healthLabel.text = $"{Mathf.RoundToInt(health)} / {Mathf.RoundToInt(maxHealth)}";
        float percentage = health / maxHealth;
        Debug.Log("Vida: "+health);
        Debug.Log("Vida maxima: "+maxHealth);
        Debug.Log(health / maxHealth);
        this.healthSlider.value = percentage;

        if (percentage < 0.33f)
        {
            this.healthSliderBar.color = Color.red;
        }
    }

}
