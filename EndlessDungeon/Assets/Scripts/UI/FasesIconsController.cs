using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FasesIconsController : MonoBehaviour
{
    [SerializeField] private Button botonOrden;
    [SerializeField] private Button botonSeleccion;
    [SerializeField] private Button botonEjecucion;
    [SerializeField] private Button botonEliminacion;

    public GameObject[] videos;
    // Start is called before the first frame update
    public void mostrarOrden()
    {
        Time.timeScale = 1f;
        videos[0].SetActive(true);
    }
    public void cerrarOrden()
    {
        Time.timeScale = 0f;
        videos[0].SetActive(false);
    }
    public void mostrarSeleccion()
    {
        Time.timeScale = 1f;
        videos[1].SetActive(true);
    }
    public void cerrarSeleccion()
    {
        Time.timeScale = 0f;
        videos[1].SetActive(false);
    }
    public void mostrarEjecucion()
    {
        Time.timeScale = 1f;
        videos[2].SetActive(true);
    }
    public void cerrarEjecucion()
    {
        Time.timeScale = 0f;
        videos[2].SetActive(false);
    }
    public void mostrarEliminacion()
    {
        Time.timeScale = 1f;
        videos[3].SetActive(true);
    }
    public void cerrarELiminacion()
    {
        Time.timeScale = 0f;
        videos[3].SetActive(false);
    }
}
