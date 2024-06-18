using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class niveles : MonoBehaviour
{
    public GameObject canva;
    public GameObject esconder;

    void Start()
    {
        canva.SetActive(false);
    }

    public void cambiarCanva()
    {
        canva.SetActive(true);        
    }

    public void jueraBoton()
    {
        esconder.SetActive(false);
    }
}
