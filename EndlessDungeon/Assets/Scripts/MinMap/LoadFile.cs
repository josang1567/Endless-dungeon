using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFile : MonoBehaviour
{
    // Start is called before the first frame update


     public void onclick()
    {
        Debug.Log("Clicado");
        StartCoroutine(NextLevel());
    }
     IEnumerator NextLevel()
    {      
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("");               
    }
}
