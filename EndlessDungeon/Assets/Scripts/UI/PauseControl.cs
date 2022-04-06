using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseControl : MonoBehaviour
{
[SerializeField] private GameObject botonPausa;
[SerializeField] private GameObject menuPausa;

  public void pause(){
      Time.timeScale=0f;
      botonPausa.SetActive(false);
      menuPausa.SetActive(true);
  }
   public void Restart(){
      Time.timeScale=1f;
      botonPausa.SetActive(true);
      menuPausa.SetActive(false);
  }
}
