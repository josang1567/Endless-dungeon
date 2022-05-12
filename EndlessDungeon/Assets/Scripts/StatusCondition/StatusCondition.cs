using UnityEngine;
using System.Collections.Generic;

public abstract class StatusCondition : MonoBehaviour
{
    [Header("Base Status Condition")]
    public GameObject effectPrfb;
    public float animationDuration;
    public string estado;
    public string receptionMessage;
    public string applyMessage;
    public string expireMessage;
    
    public AudioSource soundEffect;
    public int turnDuration;

    public bool hasExpired { get { return this.turnDuration <= 0; } }

    protected Queue<string> messages;
    protected Fighter receiver;
    public Sprite efecto;

    public void Awake()
    {
        this.messages = new Queue<string>();

    }

    public void SetReceiver(Fighter recv)
    {
        this.receiver = recv;
    }

    private void Animate()
    {


        soundEffect.Play();
        var go = Instantiate(this.effectPrfb, this.receiver.transform.position, Quaternion.identity);
        Destroy(go, this.animationDuration);
    }

    public void Apply()
    {
        if (this.receiver == null)
        {
            throw new System.InvalidOperationException("StatusCondition needs a receiver");
        }

        if (this.OnApply())
        {

            this.Animate();
        }

        turnDuration--;

        if (this.hasExpired)
        {
            this.messages.Enqueue(this.expireMessage.Replace("{receiver}", this.receiver.idName));
            this.receiver.statusPanel.statuscondition.gameObject.SetActive(false);
        }
    }

    public string GetNextMessage()
    {
        if (this.messages.Count != 0)
            return this.messages.Dequeue();
        else
            return null;
    }

    public string GetReceptionMessage()
    {
        this.receiver.statusPanel.statuscondition.gameObject.SetActive(true);
        this.receiver.statusPanel.statuscondition.sprite = this.efecto;
        return this.receptionMessage.Replace("{receiver}", this.receiver.idName);
    }

    public abstract bool OnApply();
    public abstract bool BlocksTurn();
}