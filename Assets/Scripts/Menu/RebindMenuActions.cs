using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RebindMenuActions : MonoBehaviour
{
    [SerializeField] private InputActionReference _oneMovement, _twoMovement;

    private void OnEnable()
    {
        _oneMovement.action.Disable();
        _twoMovement.action.Disable();
    }

    private void OnDisable()
    {
        _oneMovement.action.Enable();
        _twoMovement.action.Enable();
    }

}
