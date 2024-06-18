using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;
    
    public int MaxShots = 3;
    [SerializeField] private float _segundosMuerteCheck = 3f;
    [SerializeField] private fium _fium;
    
    [Header("Canvas")]
    [SerializeField] private GameObject _ganaste;
    [SerializeField] private GameObject _perdiste;

    private int _usarNumerosShots;

    private iconosPersonaje _iconosPersonaje;

    private List<Enemigos> _enemigos = new List<Enemigos>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        _iconosPersonaje = FindObjectOfType<iconosPersonaje>();

        Enemigos[] enemigos = FindObjectsOfType<Enemigos>();
        for (int i = 0; i < enemigos.Length; i++)
        {
            _enemigos.Add(enemigos[i]);
        }
    }

    public void UsarShots()
    {
        _usarNumerosShots++;
        _iconosPersonaje.UsarShots(_usarNumerosShots);

        CheckUltimoShot();
    }

    public bool SuficientesShots()
    {
        if (_usarNumerosShots < MaxShots)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void CheckUltimoShot()
    {
        if (_usarNumerosShots == MaxShots)
        {
            StartCoroutine(CheckTiempoPasado());
        }
    }

    private IEnumerator CheckTiempoPasado()
    {
        yield return new WaitForSeconds(_segundosMuerteCheck);

        if (_enemigos.Count == 0)
        {
            Ganar();
        }

        else
        {
            Perder();
        }
    
    }

    public void RemoverEnemigos(Enemigos enemigos)
    {
        _enemigos.Remove(enemigos);
        CheckMuertesBrutalesXD();
    }

    private void CheckMuertesBrutalesXD()
    {
        if (_enemigos.Count == 0)
        {
            Ganar();
        }
    }

    #region Ganar o perder

    private void Ganar()
    {
        _ganaste.SetActive(true);
        _fium.enabled = false;
    }

    private void Perder()
    {
        _perdiste.SetActive(true);
        _fium.enabled = false;
    }


    #endregion Tu decides 
}
