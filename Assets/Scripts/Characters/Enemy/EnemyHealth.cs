using UnityEngine;
using UnityEngine.Events;

public class EnemyHealth : Health
{
    public readonly int Damage = Animator.StringToHash("Damage");
    public readonly int Die = Animator.StringToHash("Die");
    public readonly int LayerIndex = 0;

    public event UnityAction<Enemy> Dying;

    [SerializeField] private SFXSO _damageSfx;
    [SerializeField] private SFXSO _deathSfx;
    [SerializeField] private EnemyHealthView _enemyHealthView;
    [SerializeField] private float _flashDuration;

    private Animator _animator;
    private Enemy _enemy;
    private EnemyStateMachine _enemyStateMachine;
    private Rigidbody _rigidbody;
    private FlashDamage _flashDamage;
    private Audio _audio;

    private bool _isDead;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
        _animator = GetComponent<Animator>();
        _enemyStateMachine = GetComponent<EnemyStateMachine>();
        _rigidbody = GetComponent<Rigidbody>();
        _flashDamage = GetComponentInChildren<FlashDamage>();
        _audio = GetComponentInChildren<Audio>();
    }

    private void Start()
    {
        CurrentHealth = MaxHealth;
    }

    public override void ApplyDamage(float damage)
    {
        CurrentHealth -= damage;

        OnHealthChanged(Ratio);

        if (CurrentHealth <= 0 && !_isDead)
        {
            _audio.Play(_deathSfx);
            _flashDamage.Show(_flashDuration);
            _isDead = true;

            if (_rigidbody != null)
                _rigidbody.velocity = Vector3.zero;

            _enemyStateMachine.Stop();
            _animator.SetTrigger(Die);
            _enemyHealthView.gameObject.SetActive(false);
            Dying?.Invoke(_enemy);
        }
        else if (!_isDead && _animator.HasState(LayerIndex, Damage))
        {
            _audio.Play(_damageSfx);
            _flashDamage.Show(_flashDuration);
            _animator.Play(Damage, LayerIndex, 0);
        }
        else if (!_isDead)
        {
            _audio.Play(_damageSfx);
            _flashDamage.Show(_flashDuration);
        }
    }

    private void OnEnemyDied()
    {
        Destroy(gameObject);
    }
}