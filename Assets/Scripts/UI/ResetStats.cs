using UnityEngine;
using UnityEngine.UI;

public class ResetStats : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStatsSO;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        _button.onClick.AddListener(_playerStatsSO.Reset);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(_playerStatsSO.Reset);
    }
}