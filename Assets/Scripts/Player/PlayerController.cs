using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Mediator")]
    [SerializeField] private Mediator _mediator;
    [Header("Database")]
    [SerializeField] private Database _db;
    [Header("Player InputController")]
    [SerializeField] private InputActionReference _movement;
    [Header("Rigibody")]
    [SerializeField] private Rigidbody _rb;

    private Vector3 _direction;
    private float _localTimeScale = 1f;

    #region Unity Methods
    private void Start()
    {
        _mediator.OnPauseSet += PauseSet;
    }

    private void PauseSet()
    {
        _localTimeScale = 0f;
    }

    private void Update()
    {
        //Movement();        
    }
    private void FixedUpdate()
    {
        Movement();
    }
    #endregion

    #region Private Methods
    private void Movement()
    {        
        Vector2 inputDirection = _movement.action.ReadValue<Vector2>();
        _direction.y = inputDirection.y * _db.moveSpeed * _localTimeScale;
        _rb.velocity = _direction;
    }        
    #endregion
}
