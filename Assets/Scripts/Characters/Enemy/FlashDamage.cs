using System.Collections;
using UnityEngine;

public class FlashDamage : MonoBehaviour
{
    private readonly string _colorProperty = "_BaseColor";

    [SerializeField] private Color _flashColor;

    private SkinnedMeshRenderer _skinnedMeshRenderer;
    private Coroutine _flash;

    private float _duration;
    private float _timeElapsed;
    private float _speed;

    private Color _startColor;
    private Color _currentColor;

    private void Awake()
    {
        _skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
    }

    private void Start()
    {
        _startColor = _skinnedMeshRenderer.material.GetColor(_colorProperty);
    }

    public void Show(float duration)
    {
        if (_flash != null)
            StopCoroutine(_flash);

        _flash = StartCoroutine(Flash(_startColor, _flashColor, duration));
    }

    private IEnumerator Flash(Color startColor, Color endColor, float duration)
    {
        _currentColor = startColor;

        while (_timeElapsed < duration)
        {
            ChangeColor(endColor, duration);
            yield return null;
        }

        _timeElapsed = 0;
        _currentColor = endColor;

        while (_timeElapsed < duration)
        {
            ChangeColor(startColor, duration);
            yield return null;
        }

        _timeElapsed = 0;
    }

    private void ChangeColor(Color endColor, float duration)
    {
        _timeElapsed += Time.deltaTime;

        _speed = duration / _timeElapsed;

        _currentColor = Color.Lerp(_currentColor, endColor, _speed);

        foreach (Material material in _skinnedMeshRenderer.materials)
        {
            material.SetColor(_colorProperty, _currentColor);
        }
    }
}