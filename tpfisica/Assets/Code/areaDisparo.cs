using System.Runtime.CompilerServices;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class areaDisparo : MonoBehaviour
{
    [SerializeField] private LayerMask _areaDisparoMask;

    public bool dentrodelArea()
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        if (Physics2D.OverlapPoint(worldPosition, _areaDisparoMask))
        {
            return true;
        }

        else
        {
            return false;
        }
    }
}
