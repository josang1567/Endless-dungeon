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
    [SerializeField] private GameObject GlosarioPag2;
    [SerializeField] private GameObject Pag1;
    [SerializeField] private GameObject Pag2;
    [SerializeField] private GameObject Pag3;
    [SerializeField] private GameObject Pag4;
    [SerializeField] private GameObject Pag5;
    [SerializeField] private GameObject Pag6;
    [SerializeField] private GameObject Pag7;
    [SerializeField] private GameObject Pag8;
    [SerializeField] private GameObject Pag9;

    [SerializeField] private List<GameObject> Paginas;


    void Update()
    {
        if (Input.GetKeyDown("escape") )
        {
            ocultarPantallaAll();
        }
        //pag1
        if (Input.GetKeyDown(KeyCode.RightArrow)||Input.GetKeyDown("d"))
        {
      
            cargarSiguiente();
        }
        //pag 2
        if (Input.GetKeyDown(KeyCode.LeftArrow)||Input.GetKeyDown("a"))
        {
           
            cargarAnterior();
        }



    }

    public void cargarSiguiente()
    {
        bool encontrado = false;
        for (int i = 0; i < Paginas.Count && encontrado == false; i++)
        {
            if (Paginas[i].activeSelf == true)
            {
                

                switch (Paginas[i].name)
                {
                    case "AyudaPanel, pagina 1, info Basica":
                        encontrado = true;
                        cargarPag2();
                        break;

                    case "AyudaPanel, pagina 2, Efectos de estado":
                        encontrado = true;
                        cargarPag3();
                        break;

                    case "AyudaPanel, pagina 3, fases de combate":
                        encontrado = true;
                        cargarPag4();
                        break;

                    case "AyudaPanel, pagina 4, heroes":
                        encontrado = true;
                        cargarPag5();
                        break;

                    case "AyudaPanel, pagina 5, Enemigos":
                        encontrado = true;
                        cargarPag6();
                        break;

                    case "AyudaPanel, pagina 6, Enemigos 2":
                        encontrado = true;
                        cargarPag7();
                        break;
                    case "AyudaPanel, pagina 7, Enemigos 3":
                        encontrado = true;
                        cargarPag8();
                        break;
                    case "AyudaPanel, pagina 8, Enemigos 4":
                        encontrado = true;
                        cargarPag9();
                        break;
                    case "AyudaPanel, pagina 9, Enemigos 5":
                        encontrado = true;
                        
                        break;
                    default:
                        break;
                }
            }
        }
    }

public void cargarAnterior()
    {
        bool encontrado = false;
        for (int i = 0; i < Paginas.Count && encontrado == false; i++)
        {
            if (Paginas[i].activeSelf == true)
            {
                

                switch (Paginas[i].name)
                {
                    case "AyudaPanel, pagina 1, info Basica":
                        encontrado = true;
                      
                        break;

                    case "AyudaPanel, pagina 2, Efectos de estado":
                        encontrado = true;
                       volverPag1();
                        break;

                    case "AyudaPanel, pagina 3, fases de combate":
                        encontrado = true;
                        volverPag2();
                        break;

                    case "AyudaPanel, pagina 4, heroes":
                        encontrado = true;
                        volverPag3();
                        break;

                    case "AyudaPanel, pagina 5, Enemigos":
                        encontrado = true;
                        volverPag4();
                        break;

                    case "AyudaPanel, pagina 6, Enemigos 2":
                        encontrado = true;
                      volverPag5();
                        break;
                    case "AyudaPanel, pagina 7, Enemigos 3":
                        encontrado = true;
                       volverPag6();
                        break;
                    case "AyudaPanel, pagina 8, Enemigos 4":
                        encontrado = true;
                       volverPag7();
                        break;
                    case "AyudaPanel, pagina 9, Enemigos 5":
                        encontrado = true;
                        volverPag8();
                        break;
                    default:
                        break;
                }
            }
        }
    }


    private void cargarGlosario()
    {

        Time.timeScale = 0f;
        botonGlosario.interactable = false;
        botonPausa.interactable = false;
        Glosario.SetActive(true);

    }
    private void cargarGlosario2()
    {
        Glosario.SetActive(false);
        GlosarioPag2.SetActive(true);

    }
    private void cargarPag1()
    {
        Glosario.SetActive(false);
        Pag1.SetActive(true);

    }
    private void cargarPag2()
    {
        Pag1.SetActive(false);
        Pag2.SetActive(true);
    }

    private void cargarPag3()
    {
        Pag2.SetActive(false);
        Pag3.SetActive(true);
    }

    private void cargarPag4()
    {

        Pag3.SetActive(false);
        Pag4.SetActive(true);
    }
    private void cargarPag5()
    {

        Pag4.SetActive(false);
        Pag5.SetActive(true);
    }
    private void cargarPag6()
    {

        Pag5.SetActive(false);
        Pag6.SetActive(true);
    }
    private void cargarPag7()
    {

        Pag6.SetActive(false);
        Pag7.SetActive(true);
    }
    private void cargarPag8()
    {

        Pag7.SetActive(false);
        Pag8.SetActive(true);
    }
    private void cargarPag9()
    {

        Pag8.SetActive(false);
        Pag9.SetActive(true);
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
    public void Glosario7()
    {
        Glosario.SetActive(false);
        Pag7.SetActive(true);
    }
    public void Glosario8()
    {
        Glosario.SetActive(false);
        Pag8.SetActive(true);
    }
    public void Glosario9()
    {
        Glosario.SetActive(false);
        Pag9.SetActive(true);
    }
    public void volverGlosario()
    {
        Glosario.SetActive(true);
        GlosarioPag2.SetActive(false);
        Pag1.SetActive(false);
        Pag2.SetActive(false);
        Pag3.SetActive(false);
        Pag4.SetActive(false);
        Pag5.SetActive(false);
        Pag6.SetActive(false);
        Pag7.SetActive(false);
        Pag8.SetActive(false);
        Pag9.SetActive(false);
    }
    private void volverPag1()
    {
        Pag2.SetActive(false);
        Pag1.SetActive(true);

    }
    private void volverPag2()
    {
        Pag3.SetActive(false);
        Pag2.SetActive(true);
    }

    private void volverPag3()
    {

        Pag4.SetActive(false);
        Pag3.SetActive(true);
    }
    private void volverPag4()
    {
        Pag5.SetActive(false);
        Pag4.SetActive(true);
    }
    private void volverPag5()
    {
        Pag6.SetActive(false);
        Pag5.SetActive(true);
    }
    private void volverPag6()
    {
        Pag7.SetActive(false);
        Pag6.SetActive(true);
    }
    private void volverPag7()
    {
        Pag8.SetActive(false);
        Pag7.SetActive(true);
    }
    private void volverPag8()
    {
        Pag9.SetActive(false);
        Pag8.SetActive(true);
    }


    public void ocultarPantallaAll()
    {

        Pag1.SetActive(false);
        Pag2.SetActive(false);
        Pag3.SetActive(false);
        Pag4.SetActive(false);
        Pag5.SetActive(false);
        Pag6.SetActive(false);
        Pag7.SetActive(false);
        Pag8.SetActive(false);
        Pag9.SetActive(false);
        Glosario.SetActive(false);
        GlosarioPag2.SetActive(false);
        Time.timeScale = 1f;
        botonGlosario.interactable = true;
        botonPausa.interactable = true;
    }

}
