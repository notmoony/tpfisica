using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemigos : MonoBehaviour
{
    [SerializeField] private float _maxSalud = 3f;
    [SerializeField] private float _danioArea = 0.5f;

    private float _salud;

    private void Awake()
    {
        _salud = _maxSalud;
    }

    public void Danio(float cantidadDanio)
    {
        _salud -= cantidadDanio;
        
        if (_salud <= 0f)
        {
            Morir();
        }
    }

    private void Morir()
    {
        gameManager.instance.RemoverEnemigos(this); 
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        float impactVelocity = collision.relativeVelocity.magnitude;

        if(impactVelocity > _danioArea)
        {
            Danio(impactVelocity);
        }

    }
}
