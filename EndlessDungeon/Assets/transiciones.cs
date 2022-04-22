using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class transiciones : MonoBehaviour
{
    private Animator anim;

    void Start()
    {

        anim = GetComponent<Animator>();

    }
    public void StartAnim()
    {
        StartCoroutine(NextLevel());
    }
    IEnumerator NextLevel()
    {
        yield return new WaitForSeconds(0.2f);
        anim.SetTrigger("StartTransition");
        yield return new WaitForSeconds(1f);



    }
}
