using UnityEngine;
using System.Collections.Generic;


public abstract class Skill : MonoBehaviour
{
    [Header("Base Skill")]
    public string skillName;
    public Sprite FondoCarta;

    public AudioSource soundEffect;
    public string skillDescription;
    public float animationDuration;

    public SkillTargeting targeting;

    public GameObject effectPrfb;

    protected Fighter emitter;
    protected List<Fighter> receivers;

    protected Queue<string> messages;

    public bool needsManualTargeting
    {
        get
        {
            switch (this.targeting)
            {
                case SkillTargeting.SINGLE_ALLY:
                case SkillTargeting.SINGLE_OPPONENT:
                    return true;

                default:
                    return false;
            }
        }
    }

    void Awake()
    {
        this.messages = new Queue<string>();
        this.receivers = new List<Fighter>();
    }
    //Funcion encargada de animar el ataque 
    public void Animate(Fighter receiver)
    {
        // Camera.main.GetComponent<AudioSource>().PlayOneShot(soundEffect);
        soundEffect.Play();
        var go = Instantiate(this.effectPrfb, receiver.transform.position, Quaternion.identity);
        Destroy(go, this.animationDuration);
    }
    //funcion que se ejecuta al realizar un ataque
    public void Run()
    {
        foreach (var receiver in this.receivers)
        {
            this.Animate(receiver);
            this.OnRun(receiver);
        }

        this.receivers.Clear();
    }
    //funcion que asigna el peleador que dispara la habilidad
    public void SetEmitter(Fighter _emitter)
    {
        this.emitter = _emitter;
    }

    //funcion que asigna un objetivo a la habilidad
    public void AddReceiver(Fighter _receiver)
    {
        this.receivers.Add(_receiver);
    }
    //Funcion que obtiene el mensaje de la habilidad
    public string GetNextMessage()
    {
        if (this.messages.Count != 0)
            return this.messages.Dequeue();
        else
            return null;
    }


    protected abstract void OnRun(Fighter receiver);
}