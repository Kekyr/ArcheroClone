using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class PlayerMoveState : PlayerState
{
    public readonly int IsMoving = Animator.StringToHash("IsMoving");

    [SerializeField] private float _speed;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private SFXSO _sfx;

    private Rigidbody _rigidbody;
    private Animator _animator;
    private Quaternion _newRotation;
    private Audio _audio;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
        _audio = GetComponentInChildren<Audio>();
    }

    private void OnEnable()
    {
        _animator.SetBool(IsMoving, true);
    }

    private void OnDisable()
    {
        _animator.SetBool(IsMoving, false);
        _audio.Stop();
    }

    private void FixedUpdate()
    {
        if (InputManager.Direction != Vector3.zero)
        {
            _newRotation = Quaternion.LookRotation(InputManager.Direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _newRotation, _rotationSpeed * Time.fixedDeltaTime);
            _rigidbody.MovePosition(_rigidbody.position + (InputManager.Direction * _speed * Time.fixedDeltaTime));
        }
    }

    public void OnStep()
    {
        _audio.Play(_sfx);
    }
}