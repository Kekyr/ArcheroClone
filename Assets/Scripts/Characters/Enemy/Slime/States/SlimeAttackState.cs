using System.Collections;
using UnityEngine;

public class SlimeAttackState : State
{
    public readonly int Attack = Animator.StringToHash("Attack");

    [SerializeField] private SFXSO _sfx;
    [SerializeField] private float _speed;
    [SerializeField] private float _minRandomValue;
    [SerializeField] private float _maxRandomValue;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _maxAttempts;
    [SerializeField] private float _rayDistance;

    private Movement _movement;
    private Animator _animator;
    private Audio _audio;

    private bool _canMove;
    private Vector3 _direction;
    private Vector3 _lastDirection;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _movement = GetComponent<Movement>();
        _audio = GetComponentInChildren<Audio>();
    }

    private void OnEnable()
    {
        _direction = GetRandomDirection();
        Quaternion newRotation = Quaternion.LookRotation(_direction);
        StartCoroutine(Rotate(newRotation));
    }

    private void FixedUpdate()
    {
        if (_canMove)
            _movement.Move(_direction, _speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        _canMove = false;
    }

    private IEnumerator Rotate(Quaternion newRotation)
    {
        while (transform.rotation != newRotation)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
            yield return null;
        }

        _canMove = true;
        _audio.Play(_sfx);
        _animator.SetTrigger(Attack);
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 randomDirection = Vector3.zero;
        int i = 0;

        do
        {
            randomDirection.x = Random.Range(_minRandomValue, _maxRandomValue);
            randomDirection.z = Random.Range(_minRandomValue, _maxRandomValue);
            i++;
        }
        while (CheckDirection(randomDirection) || i < _maxAttempts);

        _lastDirection = randomDirection;
        return randomDirection;
    }

    private bool CheckDirection(Vector3 direction)
    {
        bool result = Physics.Raycast(transform.position, direction, _rayDistance);
        return result;
    }
}