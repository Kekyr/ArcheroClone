using UnityEngine;

public class ShurikenMovementData : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Transform _player;
    private Vector3 _moveDirection;
    private bool _isReturning;
    private Movement _movement;

    public bool IsReturning => _isReturning;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    public void Init(Vector3 moveDirection, Transform player)
    {
        _moveDirection = moveDirection;
        _player = player;
    }

    public void ChangeReturnStatus()
    {
        _isReturning = !_isReturning;
    }

    private void FixedUpdate()
    {
        if (_isReturning)
        {
            Vector3 targetPosition = new Vector3(_player.position.x, transform.position.y, _player.position.z);
            _moveDirection = (targetPosition - transform.position).normalized;
        }

        _movement.Move(_moveDirection, _speed);
    }
}