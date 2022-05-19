using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Script encargado de pasar la pantalla & guardar la partida en los mini mapas
public class ContinueController : MonoBehaviour
{
    private string nivel;
   
    void Start()
    {
        if(PlayerPrefs.GetString("NivelActual","Nivel")=="Nueva" || PlayerPrefs.GetString("NivelActual","Nivel")=="Inicio"){
            this.nivel="0"; 
        }else if(PlayerPrefs.GetString("NivelActual","Nivel")!="Nueva"|| PlayerPrefs.GetString("NivelActual","Nivel")!="Inicio"){
            this.nivel=PlayerPrefs.GetString("NivelActual","Nivel");
        }
    }
    //Funcion encargada de pasar de pantalla e iniciar las transiciones
  public void onclick()
    {
        //En caso de que nivel ==0 significa que no hay datos que cargar o que se ha terminado todo el juego
       
        if(this.nivel=="0"){
            return;
        }
     
        StartCoroutine(Continue());
    }
   
    IEnumerator Continue()
    {      
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(this.nivel); 
           Time.timeScale = 1f;              
    }
}
