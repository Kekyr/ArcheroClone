using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoButton : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStats;

    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(OnButtonClick);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(OnButtonClick);
    }

    private void OnButtonClick()
    {
        _playerStats.Restart();
    }
}