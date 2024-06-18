using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escenas : MonoBehaviour
{
    public string nombreEscena;
    public void Escena()
    {
        SceneManager.LoadScene(nombreEscena);
    }
}
