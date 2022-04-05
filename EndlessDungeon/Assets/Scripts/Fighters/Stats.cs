using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float health;
    public float maxHealth;
    public int level;
    public float attack;
    public float deffense;

    public float Mindeffense;
    public float Minattack;
    public float spirit;
    public float speed;
    public Stats(int level, float maxHealth, float attack, float deffense, float spirit, float _speed)
    {

        this.level = level;

        this.maxHealth = maxHealth;
        this.health = maxHealth;
        this.Minattack=30;
        this.Mindeffense=30;
        this.attack = attack;
        this.deffense = deffense;
        this.spirit = spirit;
         this.speed = _speed;
    }
    public Stats Clone()
    {
        return new Stats(this.level, this.maxHealth, this.attack, this.deffense, this.spirit, this.speed);
    }

}
