using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class LevelView : MonoBehaviour
{
    public event UnityAction BarFilled;

    [SerializeField] private LevelingUp _leveling;
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private float _speed;
    [SerializeField] private TextMeshProUGUI _text;

    private Coroutine _changeValue;
    private Slider _slider;
    private float _filled;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void Start()
    {
        _slider.value = _playerStatsSO.CurrentXP/_playerStatsSO.MaxXP;
        _filled = _slider.value;
        _text.text = _playerStatsSO.LevelNumber.ToString();
    }

    private void OnEnable()
    {
        _leveling.XPChanged += OnXPChanged;
    }

    private void OnDisable()
    {
        _leveling.XPChanged -= OnXPChanged;
    }

    public void OnXPChanged(float currentXP, float levelNumber)
    {
        if (_changeValue != null)
            StopCoroutine(_changeValue);

        _changeValue = StartCoroutine(ChangeValue(currentXP, levelNumber));
    }

    private IEnumerator ChangeValue(float currentXP, float levelNumber)
    {
        while (_filled != currentXP)
        {
            _slider.value = Mathf.MoveTowards(_filled, currentXP, _speed * Time.deltaTime);
            _filled = _slider.value;
            yield return null;
        }

        if (_slider.value == _slider.maxValue)
        {
            _slider.value = _slider.minValue;
            _filled = _slider.value;
            _text.text = levelNumber.ToString();
            BarFilled?.Invoke();
        }
    }
}