using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    [SerializeField] private List<AbilitySO> _data = new List<AbilitySO>();
    [SerializeField] private List<AbilityView> _views = new List<AbilityView>();
    [SerializeField] private PlayerStatsSO _playerStats;
    [SerializeField] private Exit _exit;
    [SerializeField] private AbilitySO _restoreHealth;
    [SerializeField] private int _maxAttempts;

    private int _minRandomIndex;
    private int _maxRandomIndex;
    private int _lastIndex;

    private void Awake()
    {
        _minRandomIndex = 0;
        _maxRandomIndex = _data.Count - 1;
    }

    private void OnEnable()
    {
        for (int i = 0; i < _views.Count; i++)
        {
            AbilitySO data;
            Ability ability;
            int randomIndex;

            if (_views.Count == 2 && i == 1)
            {
                data = _restoreHealth;
            }
            else
            {
                randomIndex = GetRandomIndex();
                data = _data[randomIndex];
            }

            ability = Instantiate(data.Skill, _views[i].transform);
            ability.Init(_playerStats);

            _views[i].Init(data, ability);
            _views[i].Key.onClick.AddListener(TurnOff);
        }
    }

    private int GetRandomIndex()
    {
        int randomIndex = _lastIndex;

        for (int i = 0; randomIndex == _lastIndex && i < _maxAttempts; i++)
        {
            randomIndex = Random.Range(_minRandomIndex, _maxRandomIndex);
        }

        _lastIndex = randomIndex;
        return randomIndex;
    }

    private void TurnOff()
    {
        Time.timeScale = 1;
        _exit.gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}