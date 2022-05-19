using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseControl : MonoBehaviour
{
    [SerializeField] private Button botonPausa;
    [SerializeField] private GameObject menuPausa;
    [SerializeField] private Button botonGlosario;

    void Update()
    {
        //si se pulsa p se muestra el menu de pausa

        if (Input.GetKeyDown("p"))
        {
            pause();
        }
        else
        // si se pulsa escape se cierra el menu de pausa
       if (Input.GetKeyDown("escape"))
        {
            Continue();
        }
    }
    // se pausa el juego y se muestra el menu de pausa
    public void pause()
    {

        Time.timeScale = 0f;
        botonPausa.interactable = false;
        menuPausa.SetActive(true);
        botonGlosario.interactable = false;
    }
    // se continua el juego
    public void Continue()
    {

        Time.timeScale = 1f;
        botonPausa.interactable = true;
        menuPausa.SetActive(false);
        botonGlosario.interactable = true;
    }
    // se reinicia el nivel
    public void Restart()
    {

        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    // se cierra el juegi
    public void Cerrar()
    {
        Application.Quit();
    }
    //Se vuelve al menu de inicio
    public void Menu()
    {

        SceneManager.LoadScene("Inicio");
    }
}
