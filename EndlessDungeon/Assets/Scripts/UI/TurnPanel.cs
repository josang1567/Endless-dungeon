using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{
    protected static TurnPanel current;
 public Text turnLabel;
    void Awake(){
        current=this;
    }

    public static void Write(string message)
    {
        current.turnLabel.text = message;
    }
}