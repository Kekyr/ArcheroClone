using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerIdleState : PlayerState
{
    public readonly int IsIdle = Animator.StringToHash("IsIdle");

    private Animator _animator;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.zero;
        _animator.SetBool(IsIdle, true);
    }

    private void OnDisable()
    {
        _animator.SetBool(IsIdle, false);
    }
}
