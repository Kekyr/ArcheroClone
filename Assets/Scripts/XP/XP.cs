using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(CapsuleCollider))]
public class XP : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Rigidbody _rigidbody;

    private bool _moveToPlayer;
    private Transform _player;
    private Vector3 _direction;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (_moveToPlayer)
        {
            _direction = (_player.position - transform.position).normalized;
            _rigidbody.MovePosition(_rigidbody.position + (_direction * _speed * Time.fixedDeltaTime));
        }
    }

    public void Init(Transform player)
    {
        _player = player;
    }

    public void MoveToPlayer()
    {
        _moveToPlayer = true;
        GetComponent<Collider>().enabled = true;
    }
}