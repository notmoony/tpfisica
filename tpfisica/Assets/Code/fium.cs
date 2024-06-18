using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class fium : MonoBehaviour
{
    [Header("Render")]
    [SerializeField] private LineRenderer _izquierdaRender;
    [SerializeField] private LineRenderer _derechaRender;

    [Header("TransformERS")]
    [SerializeField] private Transform _izquierdaPosition;
    [SerializeField] private Transform _derechaPosition;
    [SerializeField] private Transform _centerPosition;
    [SerializeField] private Transform _idlePosition;

    [Header("Propiedades")]
    [SerializeField] private float _distanciaMax = 3f;
    [SerializeField] private float _shotForce = 5f;
    [SerializeField] private float _timeBetweenBirdRespawn = 2f;
    [SerializeField] private GameObject _desactivarCollider;


    [Header("Script")]
    [SerializeField] private areaDisparo _areaDisparo;

    [Header("Pajaro")]
    [SerializeField] private red _pajaro;
    [SerializeField] private float _pajaroFueraSet = 2f;

    private Vector2 _LineasPosition; //slingShotLinesPosition

    private Vector2 _LineasDirection; //direction
    private Vector2 _LineasNormalized; //directionNormalized

    private bool _clickEnElArea;
    private bool _pajaroTrichera;

    private red _spawnPajaro;
    
    private void Awake()
    {
        _izquierdaRender.enabled = false;
        _derechaRender.enabled = false;

        SpawnPajaro();
    }

    private void Update()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame && _areaDisparo.dentrodelArea())
        {
            _clickEnElArea = true;
        }

        if (Mouse.current.leftButton.isPressed && _clickEnElArea && _pajaroTrichera)
        {
            DibujarLineas();
            PositionAndRotationPajaro();
            
            Collider2D collider = _desactivarCollider.GetComponent<Collider2D>();
            if (collider != null && _desactivarCollider.layer == LayerMask.NameToLayer("ignorar"))
            {
                Physics2D.IgnoreCollision(collider, _spawnPajaro.GetComponent<Collider2D>(), true);
            }
        } 

        if (Mouse.current.leftButton.wasReleasedThisFrame && _pajaroTrichera)
        {
            if (gameManager.instance.SuficientesShots())
            {
                _clickEnElArea = false;
                _pajaroTrichera = false;
                
                Collider2D collider = _desactivarCollider.GetComponent<Collider2D>();
                if (collider != null && _desactivarCollider.layer == LayerMask.NameToLayer("ignorar"))
                {
                    Physics2D.IgnoreCollision(collider, _spawnPajaro.GetComponent<Collider2D>(), false);
                }

                _spawnPajaro.Lanzar(_LineasDirection, _shotForce);
                gameManager.instance.UsarShots();
                DibujarEnPantalla(_centerPosition.position);

                if (gameManager.instance.SuficientesShots())
                {
                    StartCoroutine(SpawnXTiempo());
                }

            }
            
        }
    }

    #region Gracias random de internet
    private void DibujarLineas()
    {
        Vector3 touchPosition = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());

        _LineasPosition = _centerPosition.position + Vector3.ClampMagnitude(touchPosition - _centerPosition.position, _distanciaMax);

        DibujarEnPantalla(_LineasPosition);

        _LineasDirection = (Vector2)_centerPosition.position - _LineasPosition;
        _LineasNormalized = _LineasDirection.normalized;
    }

    private void DibujarEnPantalla(Vector2 position)
    {
        if (!_izquierdaRender.enabled && !_derechaRender.enabled)
        {
            _izquierdaRender.enabled = true;
            _derechaRender.enabled = true;
        }

        _izquierdaRender.SetPosition(0, position);
        _izquierdaRender.SetPosition(1, _izquierdaPosition.position);
        
        _derechaRender.SetPosition(0, position);
        _derechaRender.SetPosition(1, _derechaPosition.position);

    }
    #endregion

    #region Pajaaaaaaaaaaaaaaaaaarooooooooooooooooooooooooooooooos

    private void SpawnPajaro()
    {
        DibujarEnPantalla(_idlePosition.position);

        Vector2 direction = (_centerPosition.position - _idlePosition.position).normalized;
        Vector2 spawnPosition = (Vector2)_idlePosition.position + direction * _pajaroFueraSet;

        _spawnPajaro = Instantiate(_pajaro, spawnPosition, Quaternion.identity);
        _spawnPajaro.transform.right = direction;

        _pajaroTrichera = true;
    }

    private void PositionAndRotationPajaro()
    {
        _spawnPajaro.transform.position = _LineasPosition + _LineasNormalized * _pajaroFueraSet;
        _spawnPajaro.transform.right = _LineasNormalized;
    }

    private IEnumerator SpawnXTiempo()
    {
        yield return new WaitForSeconds(_timeBetweenBirdRespawn);

        SpawnPajaro();
    }

    #endregion ayuda
}

