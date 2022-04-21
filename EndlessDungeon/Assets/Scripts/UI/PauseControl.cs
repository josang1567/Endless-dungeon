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
    public void pause()
    {
        Time.timeScale = 0f;
        botonPausa.interactable =false;
        menuPausa.SetActive(true);
        botonGlosario.interactable =false;
    }
    public void Continue()
    {
        Time.timeScale = 1f;
        botonPausa.interactable =true;
        menuPausa.SetActive(false);
         botonGlosario.interactable =true;
    }
    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Cerrar()
    {
        Application.Quit();
    }
    public void Menu()
    {
        SceneManager.LoadScene("Inicio");
    }
}
