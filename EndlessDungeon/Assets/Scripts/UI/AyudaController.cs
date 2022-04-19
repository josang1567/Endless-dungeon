using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AyudaController : MonoBehaviour
{
    [SerializeField] private Button BotonNewGame;
    [SerializeField] private Button BotonContinue;
    [SerializeField] private Button BotonAyuda;
    [SerializeField] private Button BotonPause;
    [SerializeField] private GameObject Pag1;
    [SerializeField] private GameObject Pag2;
    [SerializeField] private GameObject Pag3;
    [SerializeField] private GameObject Pag4;

    public void cargarPag1()
    {

        BotonNewGame.interactable = false;
        BotonAyuda.interactable = false;
        BotonContinue.interactable = false;
        BotonPause.interactable = false;

        Pag1.SetActive(true);
    }
    public void cargarPag2()
    {
        Pag1.SetActive(false);
        Pag2.SetActive(true);
    }

     public void cargarPag3()
    {
       Pag2.SetActive(false);
        Pag3.SetActive(true);
    }

     public void cargarPag4()
    {
       
        Pag3.SetActive(false);
        Pag4.SetActive(true);
    }


public void volverPag1()
    {
        Pag2.SetActive(false);
        Pag1.SetActive(true);
      
    }
    public void volverPag2()
    {
        Pag3.SetActive(false);
        Pag2.SetActive(true);
    }

     public void volverPag3()
    {
       Pag4.SetActive(false);
        Pag3.SetActive(true);
    }

  

    
    public void ocultarPantallaAll()
    {

        BotonNewGame.interactable = true;
        BotonAyuda.interactable = true;
        BotonContinue.interactable = true;
        BotonPause.interactable = true;
        Pag1.SetActive(false);
        Pag2.SetActive(false);
        Pag3.SetActive(false);
        Pag4.SetActive(false);
    }
    public void ocultarPantalla()
    {

        BotonNewGame.interactable = true;
        BotonAyuda.interactable = true;
        BotonContinue.interactable = true;
        BotonPause.interactable = true;
        Pag4.SetActive(false);
    }
}
