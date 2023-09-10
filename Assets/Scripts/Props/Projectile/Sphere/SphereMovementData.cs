using UnityEngine;

public class SphereMovementData : MonoBehaviour
{
    [SerializeField] private float _speed;

    private Vector3 _moveDirection;
    private Movement _movement;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
    }

    public void Init(Vector3 moveDirection)
    {
        _moveDirection = moveDirection;
    }

    private void FixedUpdate()
    {
        _movement.Move(_moveDirection, _speed);
    }
}
