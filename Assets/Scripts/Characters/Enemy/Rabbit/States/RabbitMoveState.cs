using System.Collections;
using UnityEngine;

public class RabbitMoveState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _minRandomValue;
    [SerializeField] private float _maxRandomValue;
    [SerializeField] private float _rotationSpeed;

    private Movement _movement;
    private bool _canMove;
    private Vector3 _direction;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void OnEnable()
    {
        _direction = new Vector3(Random.Range(_minRandomValue, _maxRandomValue), transform.position.y, Random.Range(_minRandomValue, _maxRandomValue));
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
    }
}
