using System.Transactions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombColision : MonoBehaviour
{
    [SerializeField] private float radio; 
    
    [SerializeField] private float fuerzaExplosion;

    [SerializeField] private GameObject estructura;

    [SerializeField] private GameObject efectoExplosion;
    
    private void OnCollisionEnter2D(Collision2D estructura)
    {
        Explosion();
    }

    public void Explosion()
    {
        Instantiate(efectoExplosion, transform.position, Quaternion.identity);

        Collider2D[] estructura = Physics2D.OverlapCircleAll(transform.position, radio);

        foreach (Collider2D colisionador in estructura)
        {
            Rigidbody2D rb2D = colisionador.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                Vector2 direccion = colisionador.transform.position - transform.position;
                float distancia = 1 + direccion.magnitude;
                float fuerza = fuerzaExplosion / distancia;
                rb2D.AddForce(direccion * fuerza);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radio);
    }

}
