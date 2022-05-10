using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadFile : MonoBehaviour
{
    // Start is called before the first frame update


     public void onclick()
    {
        StartCoroutine(NextLevel());
    }
     IEnumerator NextLevel()
    {      
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("");               
    }
}
