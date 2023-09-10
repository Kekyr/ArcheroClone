using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Input : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private PlayerInput _input;

    private Vector3 _direction;
    private Vector3 _lastDirection;

    public Vector3 Direction => _direction;
    public PlayerInput InputData => _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _input.Enable();
        _input.Player.Move.performed += ctx => OnMove();
    }

    private void OnDisable()
    {
        _input.Player.Move.performed -= ctx => OnMove();
        _input.Disable();
    }

    private void OnMove()
    {
        _direction = _input.Player.Move.ReadValue<Vector3>();
    }
}