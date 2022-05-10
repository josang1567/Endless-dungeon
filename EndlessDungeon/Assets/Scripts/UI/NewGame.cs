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

    public void cargarPantalla()
    {

        BotonNewGame.interactable = false;
        BotonAyuda.interactable = false;
        BotonContinue.interactable = false;
        BotonPause.interactable = false;

        MenuNewGame.SetActive(true);
    }
    public void CancelNewGame()
    {

        BotonNewGame.interactable = true;
        BotonAyuda.interactable = true;
        BotonContinue.interactable = true;
        BotonPause.interactable = true;
        MenuNewGame.SetActive(false);
    }
    public void onclick()
    {

        StartCoroutine(NextLevel());
    }

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
