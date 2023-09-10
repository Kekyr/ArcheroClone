using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "new PlayerStatsSO", menuName = "PlayerStatsSO/Create new PlayerStatsSO")]
public class PlayerStatsSO : ScriptableObject
{
    public event UnityAction<float> AttackSpeedChanged;
    public event UnityAction HealthRestored;
    public event UnityAction Rebirthed;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    [SerializeField] private int _levelNumber;
    [SerializeField] private float _currentXP;
    [SerializeField] private float _maxXP;

    [SerializeField] private float _damage;
    [SerializeField] private float _attackSpeed;

    [SerializeField] private int _diagonalCount;
    [SerializeField] private int _forwardCount;
    [SerializeField] private int _backwardCount;
    [SerializeField] private int _sideCount;

    [SerializeField] private int _multiShotCount;
    [SerializeField] private float _multiShotDelay;

    private int _zeroSceneIndex = 0;
    private int _deathCount = 0;

    public float CurrentHealth => _currentHealth;
    public float MaxHealth => _maxHealth;
    public int LevelNumber => _levelNumber;
    public float CurrentXP => _currentXP;
    public float MaxXP => _maxXP;
    public float Damage => _damage;
    public float AttackSpeed => _attackSpeed;
    public int DiagonalCount => _diagonalCount;
    public int ForwardCount => _forwardCount;
    public int BackwardCount => _backwardCount;
    public int SideCount => _sideCount;
    public int MultiShotCount => _multiShotCount;
    public float MultiShotDelay => _multiShotDelay;
    public int DeathCount => _deathCount;

    public void ApplyDamage(int damage)
    {
        _currentHealth -= damage;
    }

    public void IncreaseLevel()
    {
        _levelNumber++;
    }

    public void AddXP()
    {
        _currentXP++;
    }

    public void ResetXP()
    {
        _currentXP = 0;
    }

    public void AddForwardShuriken()
    {
        int damagePercent = -25;
        _forwardCount++;
        ChangeStat(ref _damage, damagePercent);
    }

    public void AddBackwardShuriken()
    {
        _backwardCount++;
    }

    public void AddDiagonalShuriken()
    {
        _diagonalCount++;
    }

    public void AddSideShuriken()
    {
        _sideCount++;
    }

    public void AddMultiShot()
    {
        int damagePercent = -10;
        int speedPercent = -15;
        _multiShotCount++;
        ChangeAttackSpeed(speedPercent);
        ChangeStat(ref _damage, damagePercent);
    }

    public void MinorAttackSpeedBoost()
    {
        int boostPercent = 12;
        ChangeAttackSpeed(boostPercent);
    }

    public void MajorAttackSpeedBoost()
    {
        int boostPercent = 25;
        ChangeAttackSpeed(boostPercent);
    }

    public void MinorDamageBoost()
    {
        int boostPercent = 15;
        ChangeStat(ref _damage, boostPercent);
    }

    public void MajorDamageBoost()
    {
        int boostPercent = 30;
        ChangeStat(ref _damage, boostPercent);
    }

    public void RestoreHealth()
    {
        float maxPercent = 100;
        float restorePercent = 40;
        float possiblePercent = maxPercent - ((_currentHealth / _maxHealth) * maxPercent);
        float onePercent = _maxHealth / maxPercent;

        restorePercent = possiblePercent >= restorePercent ? restorePercent : possiblePercent;
        _currentHealth += onePercent * restorePercent;
        HealthRestored?.Invoke();
    }

    public void MaxHealthBoost()
    {
        int boostPercent = 20;
        ChangeStat(ref _maxHealth, boostPercent);
    }

    private void ChangeStat(ref float stat, float percent)
    {
        float maxPercent = 100;
        float onePercent = stat / maxPercent;
        stat += onePercent * percent;
    }

    private void ChangeAttackSpeed(int percent)
    {
        ChangeStat(ref _attackSpeed, percent);
        AttackSpeedChanged?.Invoke(_attackSpeed);
    }

    public void Reset()
    {
        _maxHealth = 100;
        _currentHealth = _maxHealth;
        _levelNumber = 0;
        _currentXP = 0;
        _maxXP = 20;
        _damage = 10;
        _attackSpeed = 2;
        _diagonalCount = 0;
        _backwardCount = 0;
        _forwardCount = 1;
        _sideCount = 0;
        _multiShotCount = 0;
        _multiShotDelay = 0.2f;
        _deathCount = 0;
    }

    public void Rebirth()
    {
        _deathCount++;
        _currentHealth = _maxHealth;
        Rebirthed?.Invoke();
        HealthRestored?.Invoke();
    }

    public void Restart()
    {
        Reset();
        SceneManager.LoadScene(_zeroSceneIndex);
    }
}