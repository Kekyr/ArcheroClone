using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    public readonly int Die = Animator.StringToHash("Die");

    public event UnityAction<float> HealthChanged;

    [SerializeField] private SFXSO _damageSfx;
    [SerializeField] private SFXSO _deathSfx;
    [SerializeField] private PlayerStatsSO _playerStats;
    [SerializeField] private float _flashDuration;
    [SerializeField] private RebirthScreen _rebirthScreen;

    private Animator _animator;
    private FlashDamage _flashDamage;
    private PlayerStateMachine _stateMachine;
    private HealthView _healthView;
    private Audio _audio;

    private bool _isDead;

    public float Ratio => _playerStats.CurrentHealth / _playerStats.MaxHealth;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _flashDamage = GetComponentInChildren<FlashDamage>();
        _stateMachine = GetComponent<PlayerStateMachine>();
        _healthView = GetComponentInChildren<HealthView>();
        _audio = GetComponentInChildren<Audio>();
    }

    private void OnEnable()
    {
        _playerStats.HealthRestored += OnHealthRestored;
        _playerStats.Rebirthed += OnPlayerRebirthed;
    }

    private void OnDisable()
    {
        _playerStats.HealthRestored -= OnHealthRestored;
        _playerStats.Rebirthed -= OnPlayerRebirthed;
    }

    public void ApplyDamage(int damage)
    {
        _playerStats.ApplyDamage(damage);

        HealthChanged?.Invoke(Ratio);

        if (_playerStats.CurrentHealth <= 0 && !_isDead)
        {
            _isDead = true;
            _flashDamage.Show(_flashDuration);
            _stateMachine.Stop();
            _animator.SetTrigger(Die);
            _audio.Play(_deathSfx);
            _healthView.gameObject.SetActive(false);
        }
        else if(!_isDead)
        {
            _audio.Play(_damageSfx);
            _flashDamage.Show(_flashDuration);
        }
    }

    private void OnPlayerDied()
    {
        if (_playerStats.DeathCount == 0)
            _rebirthScreen.gameObject.SetActive(true);
        else
            _playerStats.Restart();

        gameObject.SetActive(false);
    }

    private void OnHealthRestored()
    {
        HealthChanged?.Invoke(Ratio);
    }

    private void OnPlayerRebirthed()
    {
        _healthView.gameObject.SetActive(true);
        _stateMachine.enabled = true;
        _stateMachine.Launch();
    }
}