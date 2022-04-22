using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AyudaController : MonoBehaviour
{
    [SerializeField] private Button botonPausa;
    [SerializeField] private Button botonGlosario;
    [SerializeField] private GameObject Glosario;
    [SerializeField] private GameObject Pag1;
    [SerializeField] private GameObject Pag2;
    [SerializeField] private GameObject Pag3;
    [SerializeField] private GameObject Pag4;
    [SerializeField] private GameObject Pag5;
    [SerializeField] private GameObject Pag6;

    public void cargarGlosario()
    {

        Time.timeScale = 0f;
        botonGlosario.interactable = false;
        botonPausa.interactable = false;
        Glosario.SetActive(true);

    }
    public void cargarPag1()
    {
        Glosario.SetActive(false);
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
    public void cargarPag5()
    {

        Pag4.SetActive(false);
        Pag5.SetActive(true);
    }
    public void cargarPag6()
    {

        Pag5.SetActive(false);
        Pag6.SetActive(true);
    }
    public void Glosario1()
    {
        Glosario.SetActive(false);
        Pag1.SetActive(true);
    }
    public void Glosario2()
    {
        Glosario.SetActive(false);
        Pag2.SetActive(true);
    }
    public void Glosario3()
    {
        Glosario.SetActive(false);
        Pag3.SetActive(true);
    }

    public void Glosario4()
    {
        Glosario.SetActive(false);
        Pag4.SetActive(true);
    }

    public void Glosario5()
    {
        Glosario.SetActive(false);
        Pag5.SetActive(true);
    }
    public void Glosario6()
    {
        Glosario.SetActive(false);
        Pag6.SetActive(true);
    }
    public void volverGlosario()
    {
        Glosario.SetActive(true);
        Pag1.SetActive(false);
        Pag2.SetActive(false);
        Pag3.SetActive(false);
        Pag4.SetActive(false);
        Pag5.SetActive(false);
        Pag6.SetActive(false);
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
    public void volverPag4()
    {
        Pag5.SetActive(false);
        Pag4.SetActive(true);
    }
    public void volverPag5()
    {
        Pag6.SetActive(false);
        Pag5.SetActive(true);
    }


    public void ocultarPantallaAll()
    {

        Pag1.SetActive(false);
        Pag2.SetActive(false);
        Pag3.SetActive(false);
        Pag4.SetActive(false);
        Pag5.SetActive(false);
        Pag6.SetActive(false);
        Glosario.SetActive(false);
        Time.timeScale = 1f;
        botonGlosario.interactable = true;
        botonPausa.interactable = true;
    }

}
