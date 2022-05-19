using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogPanel : MonoBehaviour
{
    protected static LogPanel current;
 public Text logLabel;
    void Awake(){
        current=this;
    }
//Escribe el mensaje en la pantalla
    public static void Write(string message)
    {
        current.logLabel.text = message;
    }
}
