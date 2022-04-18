using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ContinueController : MonoBehaviour
{
     [Header("Boton")]
    public GameObject ContinueButton;
    private string nivel;
   
    void Start()
    {
        if(PlayerPrefs.GetString("NivelActual","Nivel")=="Nueva"){
            this.nivel="0"; 
        }else{
            this.nivel=PlayerPrefs.GetString("NivelActual","Nivel");
        }
    }
  public void onclick()
    {
        Debug.Log("Cargando nivel guardado "+PlayerPrefs.GetString("NivelActual","Nivel"));
        StartCoroutine(Continue());
    }
   
    IEnumerator Continue()
    {      
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(this.nivel); 
           Time.timeScale = 1f;              
    }
}
