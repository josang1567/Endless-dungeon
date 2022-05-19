using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnPanel : MonoBehaviour
{
    protected static TurnPanel current;
    public List<Image> Icon;
    public List<Image> Marcs;
    void Awake()
    {
        current = this;
    }
//Funcion encargada de actualizar el panel de turnos segun la cantidad de peleadores vivos
    public static void Mostrar(List<Sprite> iconos)
    {
        switch (iconos.Count)
        {
            case 0:
              
                for (int i = 0; i < 5; i++)
                {
                    current.Icon[i].gameObject.SetActive(false);
                    current.Marcs[i].gameObject.SetActive(false);
                }
                break;
            case 1:
              
                current.Icon[1].gameObject.SetActive(false);
                current.Icon[2].gameObject.SetActive(false);
                current.Icon[3].gameObject.SetActive(false);
                current.Icon[4].gameObject.SetActive(false);
                current.Icon[5].gameObject.SetActive(false);

                for (int i = 0; i < 1; i++)
                {
                    current.Icon[i].sprite = iconos[i];
                    current.Marcs[i].gameObject.SetActive(false);
                }
                break;
            case 2:
              
                current.Marcs[2].gameObject.SetActive(false);
                current.Marcs[3].gameObject.SetActive(false);
                current.Marcs[4].gameObject.SetActive(false);

                current.Icon[2].gameObject.SetActive(false);
                current.Icon[3].gameObject.SetActive(false);
                current.Icon[4].gameObject.SetActive(false);
                current.Icon[5].gameObject.SetActive(false);
                for (int i = 0; i < 2; i++)
                {
                    current.Icon[i].sprite = iconos[i];
                }
                break;
            case 3:
           
                current.Marcs[2].gameObject.SetActive(false);
                current.Marcs[3].gameObject.SetActive(false);
                current.Marcs[4].gameObject.SetActive(false);

                current.Icon[3].gameObject.SetActive(false);
                current.Icon[4].gameObject.SetActive(false);
                current.Icon[5].gameObject.SetActive(false);
                for (int i = 0; i < 3; i++)
                {
                    current.Icon[i].sprite = iconos[i];
                }
                break;
            case 4:
             
                current.Marcs[3].gameObject.SetActive(false);
                current.Marcs[4].gameObject.SetActive(false);

                current.Icon[4].gameObject.SetActive(false);
                current.Icon[5].gameObject.SetActive(false);
                for (int i = 0; i < 4; i++)
                {
                    current.Icon[i].sprite = iconos[i];
                }

                break;
            case 5:
                
                current.Marcs[4].gameObject.SetActive(false);
                current.Icon[5].gameObject.SetActive(false);
                for (int i = 0; i < 5; i++)
                {
                    current.Icon[i].sprite = iconos[i];
                }

                break;
            case 6:
             
                for (int i = 0; i < 6; i++)
                {
                    current.Icon[i].sprite = iconos[i];
                }
                break;
        }
    }
}