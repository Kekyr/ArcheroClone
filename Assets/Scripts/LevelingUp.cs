using UnityEngine;
using UnityEngine.Events;

public class LevelingUp : MonoBehaviour
{
    public event UnityAction<float, float> XPChanged;

    [SerializeField] private LevelView _levelView;
    [SerializeField] private AbilityManager _abilityManager;
    [SerializeField] private PlayerStatsSO _playerStats;
    [SerializeField] private SFXSO _sfx;

    private bool _canChange = true;
    private Audio _audio;

    public float Ratio => _playerStats.CurrentXP / _playerStats.MaxXP;

    private void Awake()
    {
        _audio = GetComponentInChildren<Audio>();
    }

    private void OnEnable()
    {
        _levelView.BarFilled += OnBarFilled;
    }

    private void OnDisable()
    {
        _levelView.BarFilled -= OnBarFilled;
    }

    public void AddXp()
    {
        _playerStats.AddXP();

        if (_playerStats.CurrentXP == _playerStats.MaxXP)
        {
            _playerStats.IncreaseLevel();
            _audio.Play(_sfx);
            _canChange = false;
            XPChanged?.Invoke(Ratio, _playerStats.LevelNumber);
            _playerStats.ResetXP();
        }

        if (_canChange)
            XPChanged?.Invoke(Ratio, _playerStats.LevelNumber);
    }

    private void OnBarFilled()
    {
        XPChanged?.Invoke(Ratio, _playerStats.LevelNumber);
        _canChange = true;
        Time.timeScale = 0;
        _abilityManager.gameObject.SetActive(true);
    }
}