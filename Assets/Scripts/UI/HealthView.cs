using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private PlayerStatsSO _playerStatsSO;
    [SerializeField] private float _speed;

    private Coroutine _changeValue;
    private Slider _slider;
    private TextMeshProUGUI _text;
    private float _filled;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
        _text = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        _slider.value = _health.Ratio;
        _filled = _slider.value;
        _text.text = _playerStatsSO.CurrentHealth.ToString();
    }

    private void OnEnable()
    {
        _health.HealthChanged += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= OnHealthChanged;
    }

    public void OnHealthChanged(float newValue)
    {
        if (_changeValue != null)
            StopCoroutine(_changeValue);

        _changeValue = StartCoroutine(ChangeValue(newValue));
    }

    private IEnumerator ChangeValue(float newValue)
    {
        while (_filled != newValue)
        {
            _slider.value = Mathf.MoveTowards(_filled, newValue, _speed * Time.deltaTime);
            _filled = _slider.value;
            _text.text = _playerStatsSO.CurrentHealth.ToString();
            yield return null;
        }
    }
}