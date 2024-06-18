using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class iconosPersonaje : MonoBehaviour
{
    [SerializeField] private Image[] _icons;
    [SerializeField] private Color _colores;

    public void UsarShots(int shotNum)
    {
        for (int i = 0; i < _icons.Length; i++)
        {
            if (shotNum == i + 1)
            {
                _icons[i].color = _colores;
                return;
            }
        }
    }

}
