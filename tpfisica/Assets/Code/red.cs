using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class red : MonoBehaviour
{
   private Rigidbody2D _rb;
   private CircleCollider2D _circleCollider;

   private bool _yaLanzado;
   private bool _faceDirection;

   private void Awake()
   {
        _rb = GetComponent<Rigidbody2D>();
        _circleCollider = GetComponent<CircleCollider2D>();
   }

   private void Start()
   {
        _rb.isKinematic = true;
        _circleCollider.enabled = false;
   }

   private void FixedUpdate()
   {
        if (_yaLanzado && _faceDirection)
        {
            transform.right = _rb.velocity;
        }
   }

   public void Lanzar(Vector2 direction, float force)
   {
        _rb.isKinematic = false;
        _circleCollider.enabled = true;

        _rb.AddForce(direction * force, ForceMode2D.Impulse);

        _yaLanzado = true;
        _faceDirection = true;
   }

   private void OnCollisionEnter2D(Collision2D collision)
   {
        _faceDirection = false; 
   }
}
