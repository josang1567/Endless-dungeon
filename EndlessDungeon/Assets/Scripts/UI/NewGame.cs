using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class NewGame : MonoBehaviour
{
    public string NextScene;
    private Text label;
    [SerializeField] private Button BotonNewGame;
    [SerializeField] private Button BotonContinue;
    [SerializeField] private Button BotonAyuda;
    [SerializeField] private Button BotonPause;
    [SerializeField] private GameObject MenuNewGame;

    //Muestra el panel de confirmacion
    public void cargarPantalla()
    {

        BotonNewGame.interactable = false;
        BotonAyuda.interactable = false;
        BotonContinue.interactable = false;
        BotonPause.interactable = false;

        MenuNewGame.SetActive(true);
    }
   
   //Oculta el panel de confirmacion
    public void CancelNewGame()
    {

        BotonNewGame.interactable = true;
        BotonAyuda.interactable = true;
        BotonContinue.interactable = true;
        BotonPause.interactable = true;
        MenuNewGame.SetActive(false);
    }
    //Carga la nueva partida
    public void onclick()
    {

        StartCoroutine(NextLevel());
    }

// funcion de cargado, se controla la animaciones y se sobrescriben los datos previos
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(this.NextScene);
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetString("NivelActual", this.NextScene);
        PlayerPrefs.SetFloat("HuntressVida", 50);
        PlayerPrefs.SetFloat("MagoVida", 50);
        PlayerPrefs.SetFloat("KnightVida", 50);
        Time.timeScale = 1f;
    }


}
