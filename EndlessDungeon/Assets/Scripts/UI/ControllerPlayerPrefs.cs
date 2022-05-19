using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ControllerPlayerPrefs : MonoBehaviour
{
//Se encarga de controlar el guardado del progreso
    void Start()
    {
        if ((PlayerPrefs.GetString("NivelActual", "Nivel") != "Nueva"))
        {

        }
        else if ((PlayerPrefs.GetString("NivelActual", "Nivel") != "Nivel 3 fase 3"))
        {
            PlayerPrefs.DeleteAll();
        }
        else
        {
            PlayerPrefs.SetString("NivelActual", "Nueva");
        }

    }



}
