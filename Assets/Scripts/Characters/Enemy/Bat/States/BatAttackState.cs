using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(Animator))]
public class BatAttackState : State
{
    public readonly int AttackTrigger = Animator.StringToHash("Attack");

    [SerializeField] private SFXSO _sfx;
    [SerializeField] private float _speed;
    [SerializeField] private float _offsetX;
    
    private Movement _movement;
    private Animator _animator;
    private Audio _audio;

    private Vector3 _direction;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        _animator = GetComponent<Animator>();
        _audio = GetComponentInChildren<Audio>();
    }

    private void OnEnable()
    {
        float randomOffsetX = Random.Range(-_offsetX, _offsetX);
        Vector3 targetPosition = new Vector3(Target.transform.position.x + randomOffsetX, transform.position.y, Target.transform.position.z);
        _direction = (targetPosition - transform.position).normalized;
        _audio.Play(_sfx);
        _animator.SetTrigger(AttackTrigger);
    }

    private void FixedUpdate()
    {
        _movement.Move(_direction, _speed);
    }
}