using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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
  public void onclick()
    {
        
       
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
