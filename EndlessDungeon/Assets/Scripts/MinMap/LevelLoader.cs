using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class LevelLoader : MonoBehaviour
{
    public string NextScene;
    private Text label;


    void Start()
    {
       
        Time.timeScale = 1f;
    }


    public void onclick()
    {

        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(this.NextScene);
        PlayerPrefs.SetString("NivelActual", this.NextScene);

    }
}
