using UnityEngine;

[RequireComponent(typeof(Movement))]
public class BatMoveState : State
{
    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _minDistance;

    private Movement _movement;

    private Vector3 _direction;
    private float _distance;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    private void Update()
    {
        Vector3 modifiedTargetPosition = new Vector3(Target.transform.position.x, transform.position.y, Target.transform.transform.position.z);
        Quaternion newRotation = Quaternion.LookRotation(_direction);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, _rotationSpeed * Time.deltaTime);
        _direction = (modifiedTargetPosition - transform.position).normalized;
        _distance = (modifiedTargetPosition - transform.position).magnitude;
    }

    private void FixedUpdate()
    {
        if (_distance > _minDistance)
            _movement.Move(_direction, _speed);
    }
}